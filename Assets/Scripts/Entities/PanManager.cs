using System;
using System.Collections.Generic;
using UnityEngine;

public class PanManager : MonoBehaviour
{
    [SerializeField] public Pan panPrefab;

    public event Action<Pan> OnPanAdded;
    public event Action<Pan> OnPanSelected;
    
    private Pan _currentPan;
    private int _currentPanIndex = 0;
    
    private List<Pan> pans = new List<Pan>();

    public void StartGame()
    {
        foreach (var pan in pans)
        {
            Destroy(pan.gameObject);
        }
        
        pans.Clear();
        // add a single pan
        _currentPan = AddPan();
    }
    
    public Pan AddPan()
    {
        var newPan = Instantiate(panPrefab, transform);
        pans.Add(newPan);
        OnPanAdded?.Invoke(newPan);

        return newPan;
    }

    public void NextPan()
    {
        _currentPanIndex++;
        if (_currentPanIndex >= pans.Count)
        {
            _currentPanIndex = 0;
        }
        _currentPan = pans[_currentPanIndex];
        OnPanSelected?.Invoke(_currentPan);
    }
    
    public void PreviousPan()
    {
        _currentPanIndex--;
        if (_currentPanIndex < 0)
        {
            _currentPanIndex = pans.Count - 1;
        }
        _currentPan = pans[_currentPanIndex];
        OnPanSelected?.Invoke(_currentPan);
    }
    
    public Pan GetCurrentPan()
    {
        return _currentPan;
    }
}
