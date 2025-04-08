using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 각 미니게임마다의 로직을 구현하게 할 인터페이스
/// </summary>
public interface ICookingMiniGameHandler 
{
    void StartGame(); // 초기화
    void UpdateGame();  // 게임 중 로직

    //bool isGameOver(); // 게임 오버 판단 
    void StopGame(); // 게임 종료
}
