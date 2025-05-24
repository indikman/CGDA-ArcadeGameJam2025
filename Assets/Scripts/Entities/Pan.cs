using System;
using UnityEngine;

public class Pan : MonoBehaviour
{
    [SerializeField] private GameObject panSelectionIndicator;
    
    public Food _currentFood { get; private set; }

    public event Action<Food> OnFoodCompleted;

    private PanManager _panManager;

    private void Awake()
    {
        _panManager = ServiceLocator.instance.GetService<PanManager>();
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void OnEnable()
    {
        _panManager.OnPanSelected += OnPanSelected;
    }
    
    private void OnDisable()
    {
        _panManager.OnPanSelected -= OnPanSelected;
    }

    public void FlipPan()
    {
        
    }

    public void AddFood(Food food)
    {
        _currentFood = food;
    }

    public void CompleteFood()
    {
        if (!_currentFood)
        {
            //notify there is no food in the pan
            return;
        }
        
        OnFoodCompleted?.Invoke(_currentFood);
    }

    private void OnPanSelected(Pan pan)
    {
        panSelectionIndicator.SetActive(pan == this);
    }

}
