
using System;
using UnityEngine;

public class DayTimerService : MonoBehaviour
{
    [SerializeField] private int secondsForDay = 180;
    
    private CustomTimer _customTimer;
    
    public event Action OnDayStarted;
    public event Action OnDayEnded;
    public event Action<int> OnDayUpdate;
    public event Action<int> OnPenalty;
    
    private ScoreManagerService _scoreManagerService;

    private void Awake()
    {
        _scoreManagerService = ServiceLocator.instance.GetService<ScoreManagerService>();
    }
    
    private void OnEnable()
    {
        _scoreManagerService.OnPenalty += SetPenalty;
    }

    private void OnDisable()
    {
        _scoreManagerService.OnPenalty -= SetPenalty;
    }


    private void DayEnd()
    {
        OnDayEnded?.Invoke();
    }
    
    private void Update()
    {
        _customTimer?.UpdateTimer();

        if (_customTimer != null) OnDayUpdate?.Invoke((int)_customTimer.GetRatioOfTimeLeft() * 100);
    }

    public void UpdateDay()
    {
        
    }
    
    public void StartDay()
    {
        _customTimer?.StopTimer();
        if(_customTimer!=null) _customTimer.OnTimerExpired -= DayEnd;
        
        _customTimer = new CustomTimer();
        _customTimer.OnTimerExpired += DayEnd;
        
        _customTimer.StartTimer(secondsForDay);
        
        OnDayStarted?.Invoke();
    }
    
    private void SetPenalty(int penalty)
    {
        _customTimer.SetTimer(_customTimer.GetTimeLeft() - penalty);
        OnPenalty?.Invoke(penalty);
    }
}
