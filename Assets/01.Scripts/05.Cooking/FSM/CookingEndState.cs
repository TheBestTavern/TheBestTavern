using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingEndState : ICookingState
{
    private CookingStateMachine stateMachine;

    public CookingEndState(CookingStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
    }

    public void Update()
    {
    }
    public void Exit()
    {
    }
}
