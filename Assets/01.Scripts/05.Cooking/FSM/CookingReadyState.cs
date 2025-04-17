using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 미니게임 진입 시 상태
/// </summary>
public class CookingReadyState : ICookingState
{
    private CookingStateMachine stateMachine;

    public CookingReadyState(CookingStateMachine stateMachine)
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
