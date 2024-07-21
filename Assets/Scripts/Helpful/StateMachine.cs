using UnityEngine;

public class StateMachine
{
    public State CurrentState { get; private set; }

    public void Initialize(State startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(State newState)
    {
        if(CurrentState != null)
        {
            CurrentState.Exit();
        }

        CurrentState = newState;
        CurrentState.Enter();
    }

    public void ExitCurrentState()
    {
        CurrentState.Exit();
        CurrentState = null;
    }

    public void EnterState(State newState)
    {
        CurrentState = newState;
        CurrentState.Enter();
    }
}
