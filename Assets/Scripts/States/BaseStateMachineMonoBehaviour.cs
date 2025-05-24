using UnityEngine;
public abstract class BaseStateMachineMonoBehaviour : MonoBehaviour
{
    public IState currentState { get; private set; }

    private string _currentStateName;

    public void SetState(IState newState)
    {
        currentState?.ExitState();

        currentState = newState;

        _currentStateName = newState.GetType().Name;

        currentState?.EnterState();
    }

    public virtual void Update()
    {
        currentState?.UpdateState();
    }
}
