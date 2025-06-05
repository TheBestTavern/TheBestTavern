using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingSceneUI : MonoBehaviour
{
    [SerializeField] private Image paper;

    [SerializeField] private Image homeEnding1;
    [SerializeField] private Image homeEnding2;
    [SerializeField] private Image homeEnding3;

    [SerializeField] private Image stayEnding1;
    [SerializeField] private Image stayEnding2;
    [SerializeField] private Image stayEnding3;
    [SerializeField] private Image stayEnding4;
    [SerializeField] private Image originalJumak;

    [SerializeField] private Button btn1;
    [SerializeField] private Button btn2;


    //private readonly string[] introtexts = {
    //    "여기 환자 깨어났어요!", "나는 주말마다 부모님을 위해 손을 거든다.",
    //    "오늘도 여느때와 다를 것이 없었고", "평소처럼 논두렁으로 향하던 그때",
    //    "눈을 떠보니 이곳은 내가 있던 곳이 아니었다.",
    //    "눈앞에 펼쳐진 건 고요하고 낯선 이세계 조선.",
    //    "그 옆에는 허름한 주막과 함께", "이런 쪽지가 떨어져 있었다.",
    //    "옥황상제 특명", "지상 요리 도감 완성"
    //};
    //

    void ShowHomeEnding()
    {
        // 문 여는 애니메이션
        // 블랙아웃
        // 눈깜빡이는 애니메이션
        homeEnding1.gameObject.SetActive(true);
        // 이미지 흐림처리
        // 여기 000환자 깨어났어요! 
        // 정신을 차리고 나니 손에는 이세계 조선 요리 도감이 있었고, 그 안에는 이런 메모가 남겨져 있다.
        // 페이드인
        homeEnding2.gameObject.SetActive(true);
        // 페이드아웃
        homeEnding3.gameObject.SetActive(true);
    }

    void ShowStayEnding()
    {
        // 문 닫는 애니메이션
        stayEnding1.gameObject.SetActive(true);
        stayEnding2.gameObject.SetActive(true);
        // 콩쥐, 호랑이, 허생, 선녀까지 매일 들락날락.
        // 그의 주방에선 여전히 냄새 좋은 국물이 피어오르고, 하늘 위에서는 옥황상제가 이런 말을 한다.
        stayEnding3.gameObject.SetActive(true);
        // 페이드 아웃
        originalJumak.gameObject.SetActive(true);
        // 전환효과
        stayEnding4.gameObject.SetActive(true);
    }

    public void ShowChoices(string home, string stay, Action<int> onChoose)
    {
        btn1.GetComponentInChildren<TextMeshProUGUI>().text = home;
        btn2.GetComponentInChildren<TextMeshProUGUI>().text = stay;

        btn1.onClick.AddListener(() => onChoose(0)); // 돌아간다 0
        btn2.onClick.AddListener(() => onChoose(1)); // 남는다 1
    }
}
