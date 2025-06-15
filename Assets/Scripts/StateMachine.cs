using UnityEngine;

public class StateMachine 
{
    public EntityState CurrentState {  get; private set; }
    public bool canChangeState =true;

    public void Initialize(EntityState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(EntityState newState)
    {
        if(!canChangeState) return;
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void UpdateActiveState()
    {
        CurrentState.Update();
    }

    public void SwitchOffStateMachine() => canChangeState = false;
}
