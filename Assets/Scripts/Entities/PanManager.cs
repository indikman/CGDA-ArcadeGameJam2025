using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PanManager : MonoBehaviour
{
    [SerializeField] public Pan panPrefab;
    [SerializeField] private Vector2 originPanPosition;
    [SerializeField] private float panSpacing = 1.0f;

    public event Action<Pan> OnPanAdded;
    public event Action<Pan> OnPanSelected;
    
    private Pan _currentPan;
    private int _currentPanIndex = 0;
    
    private List<Pan> pans = new List<Pan>();

    private InputManagerService _inputManagerService;

    private void Awake()
    {
        _inputManagerService = ServiceLocator.instance.GetService<InputManagerService>();
    }

    private void OnEnable()
    {
        _inputManagerService.OnAddPan += AddPan;
        _inputManagerService.OnRemovePan += RemovePan;
        _inputManagerService.OnNext += NextPan;
        _inputManagerService.OnPrevious += PreviousPan;
        
        
    }

    private void OnDisable()
    {
        _inputManagerService.OnAddPan -= AddPan;
        _inputManagerService.OnRemovePan -= RemovePan;
        _inputManagerService.OnNext -= NextPan;
        _inputManagerService.OnPrevious -= PreviousPan;
    }

    private void Start()
    {
        StartGame();
    }


    public void StartGame()
    {
        foreach (var pan in pans)
        {
            pan.ThrowFood();
            Destroy(pan.gameObject);
        }
        
        pans.Clear();
        
        AddPan();

        _currentPan = pans[0];
        OnPanSelected?.Invoke(_currentPan);
    }
    
    public void AddPan()
    {
        var newPan = Instantiate(panPrefab);

        newPan.transform.localPosition = new Vector3(originPanPosition.x + panSpacing * pans.Count, originPanPosition.y, 0);

        newPan.transform.DOMoveY(-10, .5f).From().SetEase(Ease.OutElastic, 1f);
        
        pans.Add(newPan);
        OnPanAdded?.Invoke(newPan);

    }
    
    public void RemovePan()
    {
        if (pans.Count <= 1) return;
        
        if(_currentPan._currentFood) Destroy(_currentPan._currentFood.gameObject);

        if (_currentPanIndex == pans.Count - 1)
        {
            _currentPanIndex = pans.Count - 2;
            _currentPan = pans[_currentPanIndex];
            
            OnPanSelected?.Invoke(_currentPan);
        }
        
        Destroy(pans[^1].gameObject);
        
        pans.RemoveAt(pans.Count - 1);
        
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
