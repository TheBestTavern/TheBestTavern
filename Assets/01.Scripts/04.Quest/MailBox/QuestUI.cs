using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] Button SwapBtn;
    [SerializeField] GameObject SwapTarget;
    [SerializeField] Transform slotPrt;

    bool isReady;

    private void Start()
    {
        SwapBtn.onClick.AddListener(OnSwap);
        QuestManager.Instace.newDayAction += OnNewDay;
    }

    void OnSwap()
    {
        gameObject.SetActive(false);
        SwapTarget.SetActive(true);
    }

    // 매일 실행할 것.
    public void OnNewDay()
    {
        // 1.가능한 퀘스트 리스트 받아오기
    }


    private void OnEnable()
    {
        if (!isReady)
        {
            // 1. QuestSlot 생성
            // 2. isReady true로 바꾸기.
        }

    }

}
