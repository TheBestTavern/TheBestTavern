using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GrillResultGrade
{
    legendary,
    rare,
    common,
    failed
}
/// <summary>
/// 카드 숫자 맞추기 게임
/// </summary>
public class CookingGrillMiniGame : CookingMiniGameBase
{
    
    public TextMeshProUGUI timerText;

    public List<Card> cards;
    // public GameObject cardPrefab;
    // public GameObject parentTransform; // UI_Cards

    public Card firstCard = null;
    public Card secondCard = null;

    public int matchCount = 0;

    public bool isFlipLocked; // 한번에 카드 두개만 열 수 있게 & 2초간 카드 뒤집기 불가 
   

    private Coroutine coroutine;

    public Action OnMatch;

    protected override float GetTimer()
    {
        return data.GrillTimer;
    }

    private IEnumerator ShowAllCard()
    {
        foreach (var card in cards) 
        {
            card.front.SetActive(true);
            card.back.SetActive(false);
        }
        
        yield return new WaitForSeconds(2f);
        isFlipLocked = false;

        foreach (var card in cards)
        {
            //card.front.SetActive(false);
            //card.back.SetActive(true);
            card.animator.SetTrigger("Flip");
        }

        coroutine = null;
    }

    private void Awake()
    {
        CookingMiniGameManager.Instance.GetCurrentMiniGame(this);
    }

    public void OpenCard(Card selectedCard)
    {
        if (isFlipLocked) return;

        if (firstCard == null)
        {
            firstCard = selectedCard;
            selectedCard.clickedCard.interactable = false;
        }
        else if (secondCard == null && selectedCard != firstCard)
        {
            secondCard = selectedCard;
            isFlipLocked = true; // 두번째 카드까지 열었으면 더이상 카드 열 수 없음
            StartCoroutine(CardMatch());
        }
    }
    private IEnumerator CardMatch()
    {
       
        yield return new WaitForSeconds(1f); 

        if (firstCard.idx == secondCard.idx)
        {
            // 매치
            OnMatch?.Invoke();

            matchCount++; // 맞춘 횟수 +1

            // 1. 고정, 선택 불가
            firstCard.clickedCard.interactable = false;
            //firstCard.transform.GetChild(0).GetComponent<Image>().enabled = false;
            //firstCard.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            //firstCard.transform.GetChild(1).GetComponent<Image>().enabled = false;

            secondCard.clickedCard.interactable = false;
            //secondCard.transform.GetChild(0).GetComponent<Image>().enabled = false;
            //secondCard.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            //secondCard.transform.GetChild(1).GetComponent<Image>().enabled = false;

            // 2. 고기가 익어가는 연출 필요 

            // 3. 사운드 연출
        }
        else
        {
            // 언매치

            // 열었던 카드가 다시 닫힘
            firstCard.ToggleCard();
            secondCard.ToggleCard();
            // 카드 다시 클릭 가능해짐
            firstCard.clickedCard.interactable = true;

            // 부드럽게 닫히는 애니메이션
        }

        firstCard = null;
        secondCard = null;
        isFlipLocked = false;
    }

    /// <summary>
    /// 결과 최종 판정
    /// </summary>
    public GrillResultGrade JudgeGrade()
    {
        // 7~8쌍 상 : 완벽한 구이

        // 4~6쌍 중 : 무난한 구이

        // 1~3쌍 하 : 덜 익음

        // 0쌍 : 실패

        // 총 개수를 확인한다 : 맞췄을 때마다 count +1 씩해서 count별로 등급 매기면 될듯

       if (matchCount >= 7)
       {
            Debug.Log("완벽한 구이");
            return GrillResultGrade.legendary;
       }
       else if (matchCount >=4)
       {
            Debug.Log("무난한 구이");
            return GrillResultGrade.rare;
       }
       else if (matchCount >= 1)
       {
            Debug.Log("망가진 구이");
            return GrillResultGrade.common;
       }

       Debug.Log("실패");
       return GrillResultGrade.failed;

       // UI 팝업 필요
    }



    protected override void UpdateGamePlay()
    {
        // 나중에 UI.cs로 옮길 예정
        timerText.text = timer.ToString("N2");

        if (timer <= 0f)
        {
            timerText.text = "Time Over"; 
        }
    }


    public override void StartGame()
    {
        isGameOver = false;
        elapsedTimer = 0f;
        playTime = 0f;
        timer = 15f;

        isFlipLocked = true;
        // 카드 배열 4x4 세팅

        int[] arr = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
        arr = arr.OrderBy(x => UnityEngine.Random.value).ToArray();

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].Setting(arr[i]);
        }

        // 2초 동안 모든 카드 앞면 공개
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(ShowAllCard());
    }

    public override void StopGame()
    {
        isFlipLocked = true;

        JudgeGrade();

        // 게임 종료 시 맞춘 쌍 수에 따라 요리 연출 분기

        // 1. 성공 : 황금 연기 이펙트

        // 2. 실패 : 타는 냄새 이펙트
    }


}
