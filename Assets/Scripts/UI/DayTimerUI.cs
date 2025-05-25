using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DayTimerUI : MonoBehaviour
{
    private DayTimerService _dayTimerService;
    
    [Header("UI Refs")]
    [SerializeField] private TMP_Text _timerPaneltyText;
    [SerializeField] private Canvas _penaltyValueCanvas;
    [SerializeField] private Image _timerBar;

    private void Awake()
    {
        _dayTimerService = ServiceLocator.instance.GetService<DayTimerService>();
    }
    
    private void OnEnable()
    {
        _dayTimerService.OnDayStarted += OnDayStarted;
        _dayTimerService.OnDayUpdate += OnDayUpdate;
        _dayTimerService.OnPenalty += OnPenalty;
    }

    private void OnPenalty(int penalty)
    {
        _timerPaneltyText.text = $"- {penalty} Secs.";
        _penaltyValueCanvas.enabled = true;
        _timerPaneltyText.transform.DOPunchScale(1.5f * Vector3.one, 0.5f, 10, 1f);
        Invoke(nameof(HidePenaltyText), 1.5f);
    }

    private void OnDayUpdate(int ratio)
    {
        _timerBar.fillAmount = ratio;
    }

    private void OnDayStarted()
    {
        
    }

    private void HidePenaltyText()
    {
        _penaltyValueCanvas.enabled = false;
    }

    private void OnDisable()
    {
        _dayTimerService.OnDayStarted -= OnDayStarted;
        _dayTimerService.OnDayUpdate -= OnDayUpdate;
    }   

    
}
