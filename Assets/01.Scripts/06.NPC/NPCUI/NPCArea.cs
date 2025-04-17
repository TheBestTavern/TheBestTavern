using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class NPCArea : MonoBehaviour
{
    [SerializeField] int npcNumber = 5;
    [SerializeField] NPCSlot npcSlotPref;
    [SerializeField] Transform slotsPrt;

    private Queue<NPCSlot> slotPool = new();
    private Dictionary<int /* key: npc id */, NPCSlot> activeSlots = new();

    [SerializeField] SubmissionMode submissionMode;

    private void Awake()
    {
        Init();
        QuestManager.Instance.questData.onTriggerNPC += ShowNPC;
    }

    public void Init() // 실행 어디서?
    {
        //풀에 슬롯 생성
        for (int i = 0; i < npcNumber; i++)
        {
            NPCSlot slot = Instantiate(npcSlotPref, slotsPrt);
            slot.Init();
            slotPool.Enqueue(slot);
        }
        Debug.Log($"npc슬롯 {npcNumber}개 생성 완료");
    }

    private void ShowNPC(Quest quest)
    {
        NPCSlot slot = slotPool.Dequeue();
        slot.gameObject.SetActive(true);
        slot.SetSlot(quest, this);
        if (!activeSlots.TryAdd(quest.origin.givingNPC, slot))
        {
            Debug.Log("ActiveSlots에 이미 해당 npc가 포함되어있습니다.");
            // 한 npc로부터의 복수 퀘스트 수락을 원천적으로 막고 있기 때문에, 발생한다면 버그임.
        }
    }

    private void HideNPC(Quest quest)
    {
        slotPool.Enqueue(activeSlots[quest.origin.givingNPC]);
        activeSlots.Remove(quest.origin.givingNPC);
    }

    public void EnterQuestSubmissionMode(Quest quest, NPCSlot npcSlot)
    {
        Debug.Log("아이템 제출 모드 진입");
        submissionMode.OnEnter(quest, npcSlot, this);
    }
    public void ExitQuestSubmissionMode()
    {
        StartMove(AllocateInternalPos());
    }

    /*  
     *  NPCSlot 정렬 로직   
     */
    [SerializeField] Transform left;
    [SerializeField] Transform right;

    private List<Vector2> AllocateExternalPos(int clickedSlotIndex) // 화면 바깥의 위치 지정(클릭된 슬롯 중심으로 가까운 바깥으로 나가도록)
    {
        float width = Screen.width;
        int count = activeSlots.Count();
        
        //List<Vector2> targetPositions = new();

        //for(int i = 0; i < clickedSlotIndex; i++)
        //{
        //    targetPositions.Add()
        //} 

        //for(int i = clickedSlotIndex;i < count; i++) // i = clickedSlotIndex는 할당은 하지만, 안움직이게 처리해주어야함.
        //{

        //}


    }

    private List<Vector2> AllocateInternalPos() // 화면 내부의 위치 지정
    {
        float width = Screen.width;
        int count = activeSlots.Count();
        float gap = width / count;
        float center = width / 2;

        List<Vector2> targetPositions = new();
        for (int i = 0; i < count; i++)
        {
            targetPositions.Add(new Vector2(center - (((count - 1) / 2) + i) * gap, transform.position.y));
        }
         return targetPositions;
    }

    private void StartMove(List<Vector2> targetPositions)
    {

    }
}
