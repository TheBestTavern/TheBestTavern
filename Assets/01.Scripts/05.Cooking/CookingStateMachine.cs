using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CookingStateMachine : ICookingState
{
    protected ICookingState currentState;

    public void ChangeState(ICookingState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    public void Enter()
    {
    }

    public void Exit()
    {
    }


    public void Update()
    {
        currentState?.Update();
    }
}
