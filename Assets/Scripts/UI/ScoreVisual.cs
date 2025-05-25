using System;
using DG.Tweening;
using TMPro;
using UnityEngine;


public class ScoreVisual : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text scoreDeltaText;
    [SerializeField] private TMP_Text scoreBonusText;
    

    private ScoreManagerService _scoreManagerService;
    
    private int _currentScore;
    private string _formattedScore;
    private int _scoreDelta;
    private void Awake()
    {
        _scoreManagerService = ServiceLocator.instance.GetService<ScoreManagerService>();
        scoreBonusText.text = "";
    }
    
    private void OnEnable()
    {
        _scoreManagerService.OnScoreUpdated += OnScoreUpdated;
        _scoreManagerService.OnBonusAdded += OnBonusAdded;
    }

    private void OnBonusAdded(int obj)
    {
        scoreBonusText.text = $"Bonus! +{obj}";
        
        scoreBonusText.transform.DOPunchScale(0.5f * Vector3.one, 0.5f, 10, 1f).OnComplete(
            () =>
            {
                scoreBonusText.text = "";
            });
    }

    private void OnDisable()
    {
        _scoreManagerService.OnScoreUpdated -= OnScoreUpdated;
        _scoreManagerService.OnBonusAdded -= OnBonusAdded;
    }   
    
    private void OnScoreUpdated(int score)
    {
        _scoreDelta = score - _currentScore;
        DOTween.To(() => _currentScore, x => _currentScore = x, score, 0.5f).SetEase(Ease.OutCubic).OnUpdate(
            () => SetFormattedNumber(_currentScore));
    }

    private void SetFormattedNumber(int value)
    {
        _formattedScore = value.ToString("N0"); // Adds commas, e.g., 111,111,111
        scoreText.text = _formattedScore;
    }
}
