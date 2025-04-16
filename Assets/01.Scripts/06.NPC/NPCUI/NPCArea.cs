using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCArea : MonoBehaviour
{
    [SerializeField] int npcNumber = 5;
    [SerializeField] NPCSlot npcSlotPref;
    [SerializeField] Transform slotsPrt;

    private Queue<NPCSlot> slotPool = new();
    private Dictionary<int, NPCSlot> activeSlots = new();

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
        if(!activeSlots.TryAdd(quest.origin.givingNPC, slot))
        {
            Debug.Log("ActiveSlots에 이미 해당 npc가 포함되어있습니다."); 
            // 한 npc에게 복수의 퀘스트를 수락할 수 없도록 막고 있기 때문에 해당 오류 발생 시 퀘스트 수락 로직 버그임.
        }
    }

    private void HideNPC(Quest quest)
    {
        slotPool.Enqueue(activeSlots[quest.origin.givingNPC]);
        activeSlots.Remove(quest.origin.givingNPC);
    }

    public void EnterQuestSubmissionMode(Quest quest)
    {
        Debug.Log("아이템 제출 모드 진입");
    }
}
