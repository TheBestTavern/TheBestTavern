using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 미니게임 진입 시 상태
/// </summary>
public class CookingReadyState : ICookingState
{
    private CookingStateMachine stateMachine;
    private CookingMiniGameController controller;

    public CookingReadyState(CookingStateMachine stateMachine, CookingMiniGameController controller)
    {
        this.stateMachine = stateMachine;
        this.controller = controller;
    }

    public void Enter()
    {
        controller.handler.StartGame();

    }

    public void Update()
    {
        controller.handler.UpdateGame();
    }
    public void Exit()
    {
        controller.handler.StopGame();

        // UI 비활성화?
    }
}
