using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class NPCArea : MonoBehaviour
{
    [Range(0f, 5f), SerializeField] float duration = 0.3f;
    [SerializeField] int npcNumber = 5;
    [SerializeField] NPCSlot npcSlotPref;
    [SerializeField] Transform slotsPrt;

    private Queue<NPCSlot> slotPool = new();
    private Dictionary<int, NPCSlot> activeSlots = new();

    [SerializeField] SubmissionMode submissionMode;

    private void Awake()
    {
        Init();
        QuestManager.Instance.questData.onTriggerNPC += ShowNPC;
        QuestManager.Instance.questData.onSpawnNPC += PlaceNPCs;
    }

    public void Init() // 실행 어디서?
    {
        //풀에 슬롯 생성
        for (int i = 0; i < npcNumber; i++)
        {
            NPCSlot slot = Instantiate(npcSlotPref, slotsPrt);
            slot.Init(i);
            slotPool.Enqueue(slot);
        }
        Debug.Log($"npc슬롯 {npcNumber}개 생성 완료");
    }

    private void ShowNPC(Quest quest)
    {
        NPCSlot slot = slotPool.Dequeue();
        slot.gameObject.SetActive(true);
        slot.SetSlot(quest, this);
        activeSlots.Add(slot.index, slot);
    }

    public void HideNPC(int index)
    {
        slotPool.Enqueue(activeSlots[index]);
        activeSlots.Remove(index);
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

    private void PlaceNPCs()
    {
        StartMove(AllocateInternalPos());
    }

    public  List<float> AllocateExternalPos(int clickedSlotIndex) // 화면 바깥의 위치 지정(클릭된 슬롯 중심으로 가까운 바깥으로 나가도록)
    {
        float width = Screen.width;
        int count = activeSlots.Count();

        List<float> targetPositions = new();
        for (int i = 0; i < clickedSlotIndex; i++)
        {
            targetPositions.Add(activeSlots[i].transform.position.x - width);
        }
        for (int i = clickedSlotIndex; i < count; i++) // 클릭된 슬롯은 더 늦게 호출된 다른 트윈에 의해 Kill()됨.
        {
            targetPositions.Add(activeSlots[i].transform.position.x + width);
        }
        return targetPositions;
    }

    private List<float> AllocateInternalPos() // 화면 내부의 위치 지정
    {
        float width = Screen.width;
        int count = activeSlots.Count();
        float gap = width / count;
        float center = width / 2;

        List<float> targetPositions = new();
        for (int i = 0; i < count; i++)
        {
            targetPositions.Add(center - (((count - 1) / 2) + i) * gap);
        }
        return targetPositions;
    }

    private void StartMove(List<float> targetPositions)
    {
        for (int i = 0; i < targetPositions.Count; i++)
        {
            activeSlots[i].transform.DOLocalMoveX(targetPositions[i], duration);
        }
    }

    //[SerializeField] Transform left;
    //[SerializeField] Transform right;

    //private List<Vector2> AllocateExternalPos(int clickedSlotIndex) // 화면 바깥의 위치 지정(클릭된 슬롯 중심으로 가까운 바깥으로 나가도록)
    //{
    //    float width = Screen.width;
    //    int count = activeSlots.Count();
    //    for (int i = 0; i < clickedSlotIndex; i++)
    //    {
    //        activeSlots[i].transform.SetParent(left, true);
    //    }
    //    for (int i = clickedSlotIndex + 1; i < count; i++)
    //    {
    //        activeSlots[i].transform.SetParent(right, true);
    //    }

    //    left.DOLocalMoveX(left.position.x- width, duration);
    //    right.DOLocalMoveX(left.position.x+width, duration);
    //}
}
