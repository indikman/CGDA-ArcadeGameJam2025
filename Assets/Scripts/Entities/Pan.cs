using System;
using DG.Tweening;
using UnityEngine;

public class Pan : MonoBehaviour
{
    [SerializeField] private GameObject panSelectionIndicator;
    [SerializeField] private Food foodPrefab;
    [SerializeField] private Transform foodSpawnPoint;
    
    public Food _currentFood { get; private set; }
    
    public event Action<Food> OnFoodAdded;
    public event Action<Food> OnFoodDelivered;
    
    public event Action<bool> OnFlipping;

    private PanManager _panManager;
    private bool _panSelected;
    private InputManagerService _inputManagerService;
    private GameManagerService _gameManagerService;
    private bool _isFlipping = false;

    private void Awake()
    {
        _panManager = ServiceLocator.instance.GetService<PanManager>();
        _inputManagerService = ServiceLocator.instance.GetService<InputManagerService>();
        _gameManagerService = ServiceLocator.instance.GetService<GameManagerService>();
    }
    

    private void OnEnable()
    {
        _panManager.OnPanSelected += OnPanSelected;
        
        _inputManagerService.OnPlaceDeliverFood += AddOrDeliverFood;
        _inputManagerService.OnFlipFood += FlipPan;
    }
    
    private void OnDisable()
    {
        _panManager.OnPanSelected -= OnPanSelected;
        
        _inputManagerService.OnPlaceDeliverFood -= AddOrDeliverFood;
        _inputManagerService.OnFlipFood -= FlipPan;
    }

    public void FlipPan()
    {
        if (!_panSelected) return;

        if (_isFlipping) return;
        
        
        _isFlipping = true;

        OnFlipping?.Invoke(_isFlipping);
        
        Invoke(nameof(ResumeFlipPermissions), Globals.FlipTime);
        
        //play flip animation
        if (_currentFood)
        {
            _currentFood.SetFlipping(true);
            _currentFood.FlipFood();
        }
    }

    private void AddOrDeliverFood()
    {
        if (!_panSelected) return;
        if(_isFlipping) return;
        
        if (_currentFood)
            DeliverFood();
        else
            AddFood();
    }

    public void AddFood()
    {
        _currentFood = Instantiate(foodPrefab);
        _currentFood.transform.position = foodSpawnPoint.position;

        _currentFood.transform.DOScale(0, 0.5f).From().SetEase(Ease.OutElastic, .8f);
        
        //test line only
        _currentFood.SetFoodAndStartCooking(_gameManagerService._timeToBurn,_gameManagerService._timeToCookMin,_gameManagerService._timeToCookMax);
        OnFoodAdded?.Invoke(_currentFood);
    }

    public void DeliverFood()
    {
        _currentFood.DeliverFood();
        
        OnFoodDelivered?.Invoke(_currentFood);
        
        //test
        //Destroy(_currentFood.gameObject);
        _currentFood = null;
        
    }
    
    public void ThrowFood()
    {
        if (!_currentFood) return;
        Destroy(_currentFood.gameObject);
        _currentFood = null;
    }

    private void OnPanSelected(Pan pan)
    {
        _panSelected = pan == this;
        panSelectionIndicator.SetActive(_panSelected);
    }

    private void ResumeFlipPermissions()
    {
        _isFlipping = false;
        _currentFood?.SetFlipping(false);

        OnFlipping?.Invoke(_isFlipping);
    }

}
