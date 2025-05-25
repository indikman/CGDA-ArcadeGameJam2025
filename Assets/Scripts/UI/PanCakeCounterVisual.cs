using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class PanCakeCounterVisual : MonoBehaviour
{
    [SerializeField] private TMP_Text panCakeCounterText;
    [SerializeField] private GameObject panCakePrefab;
    [SerializeField] private Transform panCakeHolder;
    
    private ScoreManagerService _scoreManagerService;
    

    private void Awake()
    {
        _scoreManagerService = ServiceLocator.instance.GetService<ScoreManagerService>();
    }

    private void Start()
    {
        ResetPanCakes();
    }


    private void OnEnable()
    {
        _scoreManagerService.OnPanCakeAdded += OnPanCakeAdded;
        _scoreManagerService.OnDayStarted += ResetPanCakes;
    }

    private void OnDisable()    
    {
        _scoreManagerService.OnPanCakeAdded -= OnPanCakeAdded;
    }
    
    private void OnPanCakeAdded(int current, int total)
    {
        //update text
        panCakeCounterText.text = $"{current}/{total}";
        panCakeCounterText.transform.DOPunchScale(1.5f * Vector3.one, 0.5f, 10, 1f);
        
        //create new pancake
        var newPanCake = Instantiate(panCakePrefab, panCakeHolder);
        newPanCake.transform.eulerAngles = new Vector3(0,0, Random.Range(-4.0f, 4.0f));
        newPanCake.transform.DOPunchScale(new Vector2(1.5f, 1.2f), 0.5f, 10, 1f);
        
    }

    private void ResetPanCakes()
    {
        panCakeCounterText.text = "";

        foreach (Transform child in panCakeHolder)
        {
            Destroy(child.gameObject);
        }   
    }
}
