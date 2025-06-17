using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public enum AreaState
{
    normal,
    submission
}

public class NPCArea : MonoBehaviour
{

    [Range(0f, 5f), SerializeField] float duration = 0.3f;
    [SerializeField] int npcNumber = 5; // 한번에 화면에 나타날 npc 숫자
    [SerializeField] NPCSlot npcSlotPref;
    [SerializeField] Transform slotsPrt;

    private Dictionary<int, NPCSlot> slotPool = new();
    private Dictionary<int, NPCSlot> activeSlots = new(); // <슬롯 인덱스, npcSlot객체>

    [SerializeField] SubmissionMode submissionMode;
    bool setNPCCount;
    int npcCount;
    List<float> targetPositions = new();

    LunarDateTime dateID = new(); // 테스트용



    private void Awake()
    {
        dateID = TimerManager.Instance.GetToday(); // 테스트용
        Init();
        //QuestManager.Instance.questData.onTriggerNPC += ShowNPC;
        //QuestManager.Instance.questData.onSpawnNPC += PlaceNPCsInside;
        EventBus.Subscribe<NPCVisitEvent>(VisitNPC);
    }

    private void OnDestroy()
    {
        //QuestManager.Instance.questData.onTriggerNPC -= ShowNPC;
        //QuestManager.Instance.questData.onSpawnNPC -= PlaceNPCsInside;
        EventBus.UnSubscribe<NPCVisitEvent>(VisitNPC);
    }
    public void Init()
    {
        //풀에 슬롯 생성
        for (int i = 0; i < npcNumber; i++)
        {
            NPCSlot slot = Instantiate(npcSlotPref, slotsPrt);
            slot.Init(i, this);
            slotPool.Add(i, slot);
        }
        //Debug.Log($"npc슬롯 {npcNumber}개 생성 완료");

        OnNewDay command = new(this);
        CommandManager.Instance.AddCommand(command);
    }

    public void VisitNPC(NPCVisitEvent evt)
    {
        ShowNPC();
        PlaceNPCsInside();
    }

    private void ShowNPC()
    {
        List<int> NPCKeys = QuestManager.Instance.questData.TodaySpawnNPCQuestIDs;
        int i = 0;
        foreach (int NPCKey in NPCKeys)
        {
            //NPCSlot slot;
            for (; i < npcNumber; i++)
            {
                if (slotPool.TryGetValue(i, out NPCSlot value))
                {
                    //slot = value;
                    //slotPool.Remove(i);
                    //activeSlots.Add(slot.index, slot);

                    //slot.gameObject.SetActive(true);
                    //slot.SetSlot(key);

                    slotPool.Remove(i);
                    activeSlots.Add(value.index, value);

                    value.SetSlot(NPCKey);
                    value.gameObject.SetActive(true);
                    break;
                }
            }

        }
    }

    public void HideNPC(int index)
    {
        slotPool[index] = activeSlots[index];
        activeSlots[index].gameObject.SetActive(false);
        activeSlots.Remove(index);
    }

    public void EnterQuestSubmissionMode(Quest quest, NPCSlot npcSlot)
    {
        Debug.Log("아이템 제출 모드 진입");
        submissionMode.OnEnter(quest, npcSlot, this);
    }

    /*  
     *  NPCSlot 정렬 로직   
     */

    public void PlaceNPCsInside()
    {
        StartMove(AllocateInternalPos());
    }

    public void PlaceNPCsOutside(int index)
    {
        StartMove(AllocateExternalPos(index));
    }

    private void StartMove(List<float> targetPositions)
    {
        int i = 0;
        foreach (var pair in activeSlots)
        {
            activeSlots[pair.Key].transform.DOMoveX(targetPositions[i], duration);
            i++;
        }
    }
    private List<float> AllocateInternalPos() // 화면 내부에 위치 지정
    {
        float width = Screen.width;
        int count = activeSlots.Count();
        float gap = width / count;
        float center = width / 2;

        targetPositions.Clear();
        for (int i = 0; i < count; i++)
        {
            targetPositions.Add(center - ((count - 1f) / 2f - i) * gap);
        }
        return targetPositions;
    }

    private List<float> AllocateExternalPos(int clickedSlotIndex) // 화면 바깥에 위치 지정(클릭된 슬롯 중심으로 가까운 바깥으로 나가도록)
    {
        float width = Screen.width;
        
        if (!setNPCCount)
        {
            npcCount = activeSlots.Count();
            setNPCCount = true;
        }

        List<float> targetPositions = new();
        for (int i = 0; i < clickedSlotIndex; i++)
        {
            if (activeSlots.TryGetValue(i, out var slot))
            {
                targetPositions.Add(slot.transform.position.x - width);
            }
        }
        for (int i = clickedSlotIndex; i < npcCount; i++) // 클릭된 슬롯은 더 늦게 호출된 다른 트윈에 의해 Kill()됨.
        {
            if (activeSlots.TryGetValue(i, out var slot))
            {
                targetPositions.Add(slot.transform.position.x + width);
            }
        }
        return targetPositions;
    }


    public class OnNewDay : IDayCommand
    {
        public NPCArea prt;
        public OnNewDay(NPCArea nPCArea)
        {
            this.prt = nPCArea;
        }

        public int Priority => 500;

        public Task Execute()
        {
            HideNPCs();
            prt.setNPCCount = false;

            return Task.CompletedTask;
        }

        public void HideNPCs()
        {
            foreach (var key in prt.activeSlots.Keys.ToList())
            {
                prt.HideNPC(key);
            }
            //Debug.Log("NPC Area 비우기");
        }

        public bool isValid()
        {
            return prt != null;
        }
    }
}
