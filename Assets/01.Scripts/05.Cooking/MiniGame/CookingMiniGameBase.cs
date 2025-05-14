using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 미니게임 공통 로직
/// </summary>
public abstract class CookingMiniGameBase : MonoBehaviour, ICookingMiniGameHandler
{
    [SerializeField] protected CookingMiniGameSO data;
    [SerializeField] protected CookingEffectController effectController;

    protected float timer;

    protected float playTime = 0f; // 미니게임 플레이 타임
    protected float elapsedTimer = 0f; // 이 씬에서의 경과시간
    protected bool isGameOver = false;

    private void Start()
    {
        timer = GetTimer();
        isGameOver = false;
    }
    protected abstract float GetTimer();


    protected virtual void Update()
    {
        if (isGameOver) return;
        elapsedTimer += Time.deltaTime;

        if (elapsedTimer >= 2f)
        {
            timer -= Time.deltaTime;
            playTime += Time.deltaTime;

            // 타이머 이미지 업데이트
            //CookingMiniGameManager.Instance.miniGameUI.UpdateTimer(playTime);

            // 게임 로직 구현부 실행
            UpdateGamePlay();
        }

        if (timer <= 0f || isGameOver)
        {
            isGameOver = true;
            StopGame();
            Debug.Log("게임종료");
            //RecipeManager.Instance.EndCooking();
            CookingMiniGameManager.Instance.GetCookingResultData(); // 완성된 것 인벤토리에 넣어주기
            PopUpManager.Instance.ShowPopUp(PopUpType.CookingResult); // 결과 팝업 띄우기

        }

        // FSM 상태 전환 실행 (구현시 이곳에서 실행)
    }

    public void InstantGameOver()
    {
        isGameOver = true;
        UIManager.Instance.ShowPopUp(PopUpType.CookingResult);
        CookingMiniGameManager.Instance.GetCookingResultData();
    }

    /// <summary>
    /// 게임 로직이 담겨있는 메서드
    /// </summary>
    protected abstract void UpdateGamePlay();


    // 인터페이스
    public abstract void StartGame();

    public abstract void StopGame();

}
