using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    InputSystem_Actions _input;


    void Awake()
    {
        _input = new InputSystem_Actions();
        
    }

    void OnEnable()
    {
        _input.Player.Start.performed += StartGame;
        _input.Player.Quit.performed += ctx =>
        {
            Application.Quit();
        };

        _input.Player.Enable();
    }

    void OnDisable()
    {
        _input.Player.Start.performed += StartGame;
    }

    private void StartGame(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("Gameplay");
    }

    
}
