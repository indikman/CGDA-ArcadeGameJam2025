using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FoodStatusUI : MonoBehaviour
{
    [SerializeField] private Pan pan;
    
    [Header("UI refs")]
    [SerializeField] private Canvas progressCanvas;

    [SerializeField] private Image cookingProgressImage;
    [SerializeField] private Image cookThresholdMaxImage;
    [SerializeField] private Image cookThresholdMinImage;

    private Food _currentFood;

    private void Start()
    {
        progressCanvas.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        pan.OnFoodAdded += OnFoodAdded;
        pan.OnFoodDelivered += OnFoodDelivered;
        pan.OnFlipping += OnFlipping;
    }

    private void OnFlipping(bool flip)
    {
        progressCanvas.gameObject.SetActive(flip);
    }

    private void OnDisable()
    {
        pan.OnFoodAdded -= OnFoodAdded;
        pan.OnFoodDelivered -= OnFoodDelivered;
        pan.OnFlipping -= OnFlipping;
    }
    
    private void OnFoodAdded(Food food)
    {
        progressCanvas.gameObject.SetActive(true);
        progressCanvas.transform.DOScaleY(0, .5f).SetEase(Ease.OutBack).From();
        
        _currentFood = food;
        
        //update the progress values
        cookThresholdMaxImage.fillAmount = food.cookThresholdMax / food.timeToBurn;
        cookThresholdMinImage.fillAmount = food.cookThresholdMin / food.timeToBurn;
        cookingProgressImage.fillAmount = 0;
        
        //listen to the food being cooked
        _currentFood.OnFoodBeingCooked += OnFoodBeingCooked;
    }
    
    private void OnFoodDelivered(Food food)
    {
        _currentFood.OnFoodBeingCooked -= OnFoodBeingCooked;
        progressCanvas.gameObject.SetActive(false);
    }
    
    private void OnFoodBeingCooked(float foodCookRatio)
    {
        cookingProgressImage.fillAmount = foodCookRatio;
    }
}

