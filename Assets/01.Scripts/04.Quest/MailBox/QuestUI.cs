using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] Button SwapBtn;
    [SerializeField] GameObject SwapTarget;

    [SerializeField] QuestSlot questSlotPref;
    [SerializeField] Transform slotPrt;

    Stack<QuestSlot> questSlots = new();
    
    [SerializeField] QuestLetter letterPref;
    [SerializeField] Transform canvasTrs;


    bool isReady;

    //private void Awake()
    //{
    //    QuestManager.Instace.OnNewDayStarted += HandleNewDay;
    //}

    private void Start()
    {
        SwapBtn.onClick.AddListener(OnSwap);
    }

    void OnSwap()
    {
        gameObject.SetActive(false);
        SwapTarget.SetActive(true);
    }

    // 매일 실행할 것. 퀘스트 매니저 이벤트에 등록해서 실행.
    //public void HandleNewDay()
    //{
    //    Debug.Log("매일 가능한 퀘스트 리스트 받아옴");
    //    // 1.가능한 퀘스트 리스트 받아오기
    //     TodayAvailableQuest = QuestManager.Instace.questData.AllQuests;
    //}


    private void OnEnable()
    {
        Debug.Log("활성화");

        if (!isReady)
        {
            // 0. 전날 있던 퀘스트 리스트 삭제.
            foreach(var slot in questSlots)
            {
                Destroy(questSlots.Pop());
            }

            // 1. QuestSlot을 생성(TodayAvailableQuest 목록을 통해서 QuestSlot을 생성하고 slotPrt 밑에 붙이기.) (슬롯 pool로 관리하면 좋을듯)
            Debug.Log("가능 퀘스트 리스트를 통해 슬롯 생성");
            QuestSlot pref;
            int i = 1;
            foreach (var quest in QuestManager.Instance.questData.TodayAvailableQuest)
            {

                pref = Instantiate(questSlotPref, slotPrt);
                pref.Init(this);
                pref.SetSlot(quest, i);
                Debug.Log($"{quest.origin.name} 퀘스트 슬롯 생성 완료");
                i++;
            }


            // 2. isReady true로 바꾸기.
            isReady = true;
        }
    }

    public QuestLetter OpenLetter(Quest quest)
    {
        // 1. 편지 프리팹 생성
        QuestLetter letter = Instantiate(letterPref, canvasTrs);

        // 2. 퀘스트 내용 편지에 넣어주기
        letter.FirstInit(quest);
        letter.EveryInit(quest);

        // 3. 반환
        return letter;
    }
}
