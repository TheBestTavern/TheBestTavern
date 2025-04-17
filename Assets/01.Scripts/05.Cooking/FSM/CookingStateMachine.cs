using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingStateMachine 
{
    protected ICookingState currentState;

    public void ChangeState(ICookingState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }
}
