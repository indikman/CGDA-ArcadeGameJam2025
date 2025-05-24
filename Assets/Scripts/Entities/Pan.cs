using System;
using UnityEngine;

public class Pan : MonoBehaviour
{
    public Food _currentFood { get; private set; }

    public event Action<Food> OnFoodCompleted; 

    void Start()
    {
        
    }

    
    void Update()
    {
        
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
}
