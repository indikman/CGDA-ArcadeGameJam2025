using UnityEngine;

public class GameManagerService : MonoBehaviour
{
    public float _timeToBurn { get; private set; }
    public float _timeToCookMin { get; private set; }
    public float _timeToCookMax { get; private set; }
    
    private ScoreManagerService _scoreManagerService;
    private DayTimerService _dayTimerService;
}
