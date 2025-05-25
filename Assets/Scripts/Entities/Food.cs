using System;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float sideACookValue { get; private set; } = 0f; 
    public float sideBCookValue { get; private set; } = 0f;
    public FoodSide currentFoodSide { get; private set; } = FoodSide.SideA;
    public float timeToBurn { get; private set; } = 0f;
    public float cookThresholdMin { get; private set; }
    public float cookThresholdMax { get; private set; }

    public event Action OnFoodFlipped;
    public event Action OnFoodDelivered;
    public event Action<float> OnFoodBeingCooked;

    private bool _isCooking = false;
    
    private bool _isFlipping = false;
    
    public enum FoodSide
    {
        SideA,
        SideB
    }

    public void SetFoodAndStartCooking(float timeToBurn, float cookThresholdMin, float cookThresholdMax)
    {
        this.timeToBurn = timeToBurn;
        this.cookThresholdMin = cookThresholdMin;
        this.cookThresholdMax = cookThresholdMax;

        sideACookValue = 0;
        sideBCookValue = 0;

        _isCooking = true;
    }

    public void FlipFood()
    {
        currentFoodSide = currentFoodSide == FoodSide.SideA ? FoodSide.SideB : FoodSide.SideA;
        OnFoodFlipped?.Invoke();
    }

    public void DeliverFood()
    {
        OnFoodDelivered?.Invoke();
    }

    public void SetFlipping(bool isFlipping)
    {
        _isFlipping = isFlipping;
    }

    private void Update()
    {
        if (!_isCooking) return;
        if(_isFlipping) return;
        
        switch (currentFoodSide)
        {
            case FoodSide.SideA:
                sideACookValue += Time.deltaTime;
            
                sideACookValue = Mathf.Clamp(sideACookValue, 0, timeToBurn);
            
                OnFoodBeingCooked?.Invoke(sideACookValue / timeToBurn);
                break;
            case FoodSide.SideB:
                sideBCookValue += Time.deltaTime;
            
                sideBCookValue = Mathf.Clamp(sideBCookValue, 0, timeToBurn);
            
                OnFoodBeingCooked?.Invoke(sideBCookValue / timeToBurn);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
