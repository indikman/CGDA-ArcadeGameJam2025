using System;
using UnityEngine;

public class ScoreManagerService : Singleton<ScoreManagerService>
{
    public int score { get; private set; }  = 0;
    public int day { get; private set; } = 0;

    private int _requiredPanCakes;
    private int _currentPanCakes;
    
    [SerializeField]private int ScoreCookedOneSide = 2;
    [SerializeField]private int ScoreCookedBothSides = 25;
    [SerializeField]private int ScoreBurned = -50;
    [SerializeField]private int ScoreBurnedOneSide = -25;
    [SerializeField]private int ScoreNotCooked = -10;
    [SerializeField] private int bonusMultiplier = 4;
    
    [Header("Penalties")]
    [SerializeField] private int penaltyNotCooked = 1;
    [SerializeField] private int penaltyBurned = 5;
    [SerializeField] private int penaltyBurnedOneSide = 2;
    
    public event Action<int> OnScoreUpdated;
    public event Action<int> OnBonusAdded;
    public event Action<int> OnPenalty;
    public event Action<int, int> OnPanCakeAdded;
    public event Action OnDayStarted;

    public void AddScoreForFood(FoodState stateOfFood)
    {
        switch (stateOfFood)
        {
            case FoodState.NotCooked:
                score += ScoreNotCooked;
                OnPenalty?.Invoke(penaltyNotCooked);
                break;
            case FoodState.CookedOneSideOnly:
                score += ScoreCookedOneSide;
                score += day;
                _currentPanCakes++;
                OnPanCakeAdded?.Invoke(_currentPanCakes, _requiredPanCakes);
                break;
            case FoodState.CookedBothSides:
                score += ScoreCookedBothSides;
                score += day;
                _currentPanCakes++;
                OnPanCakeAdded?.Invoke(_currentPanCakes, _requiredPanCakes);
                break;
            case FoodState.OneSideBurned:
                score += ScoreBurnedOneSide;
                OnPenalty?.Invoke(penaltyBurnedOneSide);
                break;
            case FoodState.Burned:
                score += ScoreBurned;
                OnPenalty?.Invoke(penaltyBurned);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(stateOfFood), stateOfFood, null);
        }

        if (_currentPanCakes > _requiredPanCakes)
        {
            //bonus
            var bonus = (_currentPanCakes - _requiredPanCakes) * bonusMultiplier; 
            score += bonus;
            OnBonusAdded?.Invoke(bonus);
        }

        score = Math.Max(0, score);
        OnScoreUpdated?.Invoke(score);
    }

    public void ResetScore()
    {
        score = 0;
        _requiredPanCakes = 0;
        _currentPanCakes = 0;
    }

    public void AddDay()
    {
        day++;
        OnDayStarted?.Invoke();
    }

    public void ResetDays()
    {
        day = 0;
    }
    
    public void SetRequriedPanCakes(int requiredPanCakes)
    {
        _requiredPanCakes = requiredPanCakes;
    }
}
