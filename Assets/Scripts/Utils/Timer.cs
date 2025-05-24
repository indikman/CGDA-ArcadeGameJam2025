using UnityEngine;

public class Timer
{
    private float _timer = 0;
    private float _maxTime = 0;
    private bool _isTimerRunning = false;

    public event System.Action OnTimerExpired;
    public event System.Action OnTimerStopped;
    public event System.Action<float> OnTimerUpdate;

    /// <summary>
    /// Starts the timer
    /// </summary>
    /// <param name="timeValue"></param>
    public void StartTimer(float timeValue)
    {
        _isTimerRunning = true;
        _maxTime = timeValue;
        _timer = timeValue;
    }

    /// <summary>
    /// Stops the current Timer
    /// </summary>
    public void StopTimer()
    {
        _isTimerRunning = false;
        OnTimerStopped?.Invoke();
    }

    public void UpdateTimer()
    {
        if (!_isTimerRunning) return;

        _timer -= Time.deltaTime;

        OnTimerUpdate?.Invoke(_timer / _maxTime);

        if (!(_timer < 0)) return;
        _isTimerRunning = false;
        OnTimerExpired?.Invoke();
    }

    public float GetRatioOfTimeLeft()
    {
        return _timer / _maxTime;
    }
        
    public void SetTimer(float timeValue)
    {
        _timer = Mathf.Clamp(timeValue,0, _maxTime);
    }
}
