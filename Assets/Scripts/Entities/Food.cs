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
    public event Action<FoodState> OnFoodDelivered;
    public event Action<float> OnFoodBeingCooked;

    private bool _isCooking = false;
    private bool _isFlipping = false;
    private bool _sideAIsCooked;
    private bool _sideBIsCooked;
    private bool _sideAIsBurned;
    private bool _sideBIsBurned;

    public FoodState currentState { get; private set; } = FoodState.NotCooked;

    public enum FoodSide
    {
        SideA,
        SideB
    }

    public void SetFoodAndStartCooking(float mTimeToBurn, float mcookThresholdMin, float mcookThresholdMax)
    {
        timeToBurn = mTimeToBurn;
        cookThresholdMin = mcookThresholdMin;
        cookThresholdMax = mcookThresholdMax;

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
        OnFoodDelivered?.Invoke(currentState);
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
        
        _sideAIsCooked = sideACookValue >= cookThresholdMin && sideACookValue <= cookThresholdMax;
        _sideBIsCooked = sideBCookValue >= cookThresholdMin && sideBCookValue <= cookThresholdMax;

        _sideAIsBurned = sideACookValue > cookThresholdMax && sideACookValue < timeToBurn;
        _sideBIsBurned = sideBCookValue > cookThresholdMax && sideBCookValue < timeToBurn;

        if (sideACookValue < cookThresholdMin && sideBCookValue < cookThresholdMin)
        {
            currentState = FoodState.NotCooked;
        }
        else if ((_sideAIsCooked && sideBCookValue < cookThresholdMin) || 
                 (_sideBIsCooked && sideACookValue < cookThresholdMin))
        {
            currentState = FoodState.CookedOneSideOnly;
        }
        else if (_sideAIsCooked && _sideBIsCooked)
        {
            currentState = FoodState.CookedBothSides;
        }
        else if (_sideAIsBurned && _sideBIsBurned)
        {
            currentState = FoodState.Burned;
        }
        else if (_sideAIsBurned || _sideBIsBurned)
        {
            currentState = FoodState.OneSideBurned;
        }
    }
}
