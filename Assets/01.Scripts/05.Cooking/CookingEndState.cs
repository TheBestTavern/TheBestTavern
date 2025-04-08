using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingEndState : ICookingState
{
    private CookingStateMachine stateMachine;
    private CookingMiniGameController controller;

    public CookingEndState(CookingStateMachine stateMachine, CookingMiniGameController controller)
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
    }
}
