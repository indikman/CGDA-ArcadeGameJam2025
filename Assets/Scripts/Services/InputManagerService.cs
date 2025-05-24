using System;
using UnityEngine.InputSystem;

public class InputManagerService : Singleton<InputManagerService>
{
    private InputSystem_Actions _inputSystemActions;
    public event Action OnPrevious;
    public event Action OnNext;
    public event Action OnPlaceDeliverFood;
    public event Action OnFlipFood;
    public event Action OnAddPan;
    public event Action OnRemovePan;
    public event Action OnStartGame;
    public event Action OnQuitGame;
    
    private void OnEnable()
    {
        _inputSystemActions = new InputSystem_Actions();
        _inputSystemActions.Player.Enable();

        _inputSystemActions.Player.AddPan.performed += AddPanPerformed;

        _inputSystemActions.Player.Previous.performed += PreviousPerformed;
        _inputSystemActions.Player.Next.performed += NextPerformed; 

        _inputSystemActions.Player.PlaceDeliverFood.performed += PlaceDeliverFoodPerformed;
        _inputSystemActions.Player.FlipFood.performed += FlipFoodPerformed;
        _inputSystemActions.Player.RemovePan.performed += RemovePanPerformed;
        _inputSystemActions.Player.Start.performed += StartGamePerformed;
        _inputSystemActions.Player.Quit.performed += QuitGamePerformed;
    }
    
    private void OnDisable()
    {
        _inputSystemActions.Player.AddPan.performed -= AddPanPerformed;
        _inputSystemActions.Player.Previous.performed -= PreviousPerformed;
        _inputSystemActions.Player.Next.performed -= NextPerformed;
        _inputSystemActions.Player.PlaceDeliverFood.performed -= PlaceDeliverFoodPerformed;
        _inputSystemActions.Player.FlipFood.performed -= FlipFoodPerformed;
        _inputSystemActions.Player.RemovePan.performed -= RemovePanPerformed;
        _inputSystemActions.Player.Start.performed -= StartGamePerformed;
        _inputSystemActions.Player.Quit.performed -= QuitGamePerformed;
        
        _inputSystemActions.Player.Disable();
    }

    private void AddPanPerformed(InputAction.CallbackContext ctx)
    {
        OnAddPan?.Invoke();
    }
    
    private void PreviousPerformed(InputAction.CallbackContext ctx)
    {
        OnPrevious?.Invoke();
    }   
    
    private void NextPerformed(InputAction.CallbackContext ctx)
    {
        OnNext?.Invoke();
    }
    
    private void PlaceDeliverFoodPerformed(InputAction.CallbackContext ctx)
    {
        OnPlaceDeliverFood?.Invoke();
    }
    
    private void FlipFoodPerformed(InputAction.CallbackContext ctx)
    {
        OnFlipFood?.Invoke();
    }
    
    private void RemovePanPerformed(InputAction.CallbackContext ctx)
    {
        OnRemovePan?.Invoke();
    }
    
    private void StartGamePerformed(InputAction.CallbackContext ctx)
    {
        OnStartGame?.Invoke();
    }
    
    private void QuitGamePerformed(InputAction.CallbackContext ctx)
    {
        OnQuitGame?.Invoke();
    }
}
