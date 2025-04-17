using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 미니게임 공통 로직
/// </summary>
public abstract class CookingMiniGameBase : MonoBehaviour, ICookingMiniGameHandler
{
    protected float timer = 15f;
    protected float elapsedTimer = 0f;
    protected bool isGameOver = false;

    protected virtual void Update()
    {
        if (isGameOver) return;
        
        elapsedTimer += Time.deltaTime;

        if (elapsedTimer >= 2f)
        {
            timer -= Time.deltaTime;

            // 게임 로직 구현부 실행
            UpdateGamePlay();
        }
        
        if(timer <= 0f)
        {
            isGameOver = true;
            StopGame();
            Time.timeScale = 0f;
        }

        // FSM 상태 전환 실행 (구현시 이곳에서 실행)
    }
    
    /// <summary>
    /// 게임 로직이 담겨있는 메서드
    /// </summary>
    protected abstract void UpdateGamePlay();


    // 인터페이스
    public abstract void StartGame();

    public abstract void StopGame();

}
