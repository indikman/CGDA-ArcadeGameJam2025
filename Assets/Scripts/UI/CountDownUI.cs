using System;
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    public GameManagerService gameManager;
    public GameObject countDownObject;
    public TMP_Text countDownText;

    void OnEnable()
    {
        gameManager.OnCountDown += OnCountDown;
        gameManager.OnGameStart += HidePage;
    }

    void OnDisable()
    {
        gameManager.OnCountDown -= OnCountDown;
        gameManager.OnGameStart -= HidePage;
    }

    public void OnCountDown(String time)
    {
        countDownObject.SetActive(true);
        countDownText.text = time;
    }

    public void HidePage()
    {
        countDownObject.SetActive(false);
    }

}
