using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 요리 미니게임 내 FSM
/// </summary>
public interface ICookingState 
{
    void Enter();
    void Update();
    void Exit();
}
