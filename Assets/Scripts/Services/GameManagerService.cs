using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerService : MonoBehaviour
{
    public float _timeToBurn { get; private set; } = 5f;
    public float _timeToCookMin { get; private set; } = 3.5f;
    public float _timeToCookMax { get; private set; } = 4.1f;
   // public int requiredPanCakes { get; private set; } = 10;
    
    private ScoreManagerService _scoreManagerService;
    private DayTimerService _dayTimerService;
    private InputManagerService _inputManagerService;

    public event Action OnDayStart;
    public event Action<string> OnCountDown;
    public event Action OnGameStart;
    public event Action<int> OnGameOver;
    
    public bool isGameRunning = false;

    public GameObject GameOverUI;
    public TMP_Text GameOverText;

    private void Awake()
    {
        _scoreManagerService = ServiceLocator.instance.GetService<ScoreManagerService>();
        _inputManagerService = ServiceLocator.instance.GetService<InputManagerService>();
        //_dayTimerService = ServiceLocator.instance.GetService<DayTimerService>();
    }

    private void OnEnable()
    {
        _scoreManagerService.OnFoodAdded += OnFoodAdded;
        _inputManagerService.OnStartGame += RestartGame;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnFoodAdded(FoodState obj)
    {
        if (obj == FoodState.Burned || obj == FoodState.OneSideBurned)
        {
            _inputManagerService.DisableInput(true);
            GameOverUI.SetActive(true);
            GameOverText.text = "Your score: " + _scoreManagerService.GetScore();
        }
    }

    private void OnDisable()
    {
        _scoreManagerService.OnFoodAdded -= OnFoodAdded;
        _inputManagerService.OnStartGame -= RestartGame;
    }

    private void Start()
    {
        //_scoreManagerService.ResetDays();
        _scoreManagerService.ResetScore();

        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop() 
    {
        //_scoreManagerService?.AddDay();
        //_scoreManagerService?.SetRequriedPanCakes(requiredPanCakes+1);
        
        _inputManagerService.DisableInput(true);

        //startcountdown
        OnCountDown?.Invoke("3");
        
        yield return new WaitForSeconds(1);
        OnCountDown?.Invoke("2");
        
        yield return new WaitForSeconds(1);
        OnCountDown?.Invoke("1");
        
        yield return new WaitForSeconds(1);        
        OnCountDown?.Invoke("Go!");
        
        _inputManagerService.DisableInput(false);

        yield return null;

        //_dayTimerService.StartDay();
        //OnDayStart?.Invoke();

        //_dayTimerService.OnDayEnded += DayEnd;

    }

    private void DayEnd()
    {
        // start next day
    }


}
