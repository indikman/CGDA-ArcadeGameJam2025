
public class EmptyPanState : IState
{
    private PanStateMachine _stateMachine;
    
    public EmptyPanState(PanStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    public void EnterState()
    {
        
    }

    public void ExitState()
    {
        
    }

    public void UpdateState()
    {
        
    }
}
