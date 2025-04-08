using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLetter : MonoBehaviour
{
    private int days;

    [SerializeField] Button closeBtn;

    [SerializeField] Button btn7;
    [SerializeField] Button btn11;
    [SerializeField] Button btn14;
    
    [SerializeField] Button Yes;
    [SerializeField] Button No;
    
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI from;

    // 편지에서 변하지 않는 부분
    private void Start()
    {
        btn7.onClick.AddListener(() => days = 7);
        btn11.onClick.AddListener(() => days = 11);
        btn14.onClick.AddListener(() => days = 14);
    }

    // 편지 열때 초기화
    private void OpenLetter(Quest quest)
    {
        // 확인/거절 버튼 초기화
        //Yes.onClick.AddListener();
        //No.onClick.AddListener();

        // 문구 초기화
        // title
        // text
        // from
    }

    // 수락 버튼 메서드
    private void AcceptQuest()
    {
        //미구현
    }

    // 거절 버튼 메서드
    private void RejectQuest()
    {
        //미구현
    }
}
