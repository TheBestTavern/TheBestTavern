using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MiniGameType 
{ 
    Cut, //도마
    Mill, // 맷돌
    Grind, // 절구
    Boil, // 끓이기
    Grill // 굽기
}

/// <summary>
/// 미니게임 플레이 상태
/// </summary>
public class CookingPlayingState : ICookingState
{
    private CookingStateMachine stateMachine;
    public CookingPlayingState(CookingStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        // 애니메이션 시작
    }

    public void Update()
    {

        stateMachine.ChangeState(new CookingEndState(stateMachine));
    }
    public void Exit()
    {
    }
}
