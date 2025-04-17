using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SubmissionMode : MonoBehaviour
{
    Quest quest;
    NPCArea area;
    NPCSlot npcSlot;
    [Range(0f, 5f), SerializeField] float duration = 0.3f;
    [SerializeField] Button submitBtn;
    [SerializeField] Transform NPCPos;
    [SerializeField] Image BG;
    Item submissionSlot;
    bool isReady;

    private void Init(NPCArea area)
    {
        submitBtn.onClick.AddListener(() => Submit(submissionSlot));
        this.area = area;

        isReady = true;
    }

    public void OnEnter(Quest quest, NPCSlot npcSlot, NPCArea area)
    {
        if (!isReady) Init(area);

        //todo- 백그라운드 투명도 상승.
        this.npcSlot = npcSlot;

        BG.DOColor(new Color(0, 0, 0, 0.2f), duration).SetEase(Ease.OutQuad);
        this.quest = quest;
        area.AllocateExternalPos(npcSlot.index);
        MoveNPC(SwitchParent(npcSlot, true));

    }

    private void OnExit() // 언제 나갈지 아직 미정.
    {
        // todo- 나갈때 효과. (npc 리스트 재정렬 , 부모 switch)
        area.ExitQuestSubmissionMode();
        // 1.화면 상에서 npc 제거
        //area.active
    }

    //private GameObject CopyNPC(GameObject npcObj) // Area에서 선택된 npc 오브젝트를 복사.
    //{
    //    GameObject go = Instantiate(npcObj, transform, true);
    //    return go;
    //}

    private NPCSlot SwitchParent(NPCSlot npcSlot, bool Enter)
    {
        //todo - 부모를 여기로 변경.
        if (Enter)
        {
            npcSlot.transform.SetParent(transform, true);
        }
        else
        {
            npcSlot.transform.SetParent(area.transform, true);
        }
        return npcSlot;
    }

    private void MoveNPC(NPCSlot npcObj) // npc 비동기적 움직임
    {
        npcObj.transform.DOLocalMove(NPCPos.position, duration + 0.2f).SetEase(Ease.OutQuad);
    }

    public void TempSubmit(Item item) // 인벤토리에서 아이템 클릭시 실행. 인벤토리 클래스 내 이벤트에 구독.
    {
        submissionSlot = item;
    }

    public void Submit(Item itemForSubmission) // 제출 버튼에 구독
    {
        if (QuestManager.Instance.TryCompleteQuest(quest, itemForSubmission))
        {
            // todo - 성공 효과 ( 화면 상에서 npc 제거, 보상 획득 )

            // 1.퀘스트 성공 후처리 (보상 획득, 완료 리스트화)
        }
        else
        {
            // todo - 실패 효과 ( 화면 상에서 npc제거? 기회 다시 줘? )

            // 1.원래 자리로 복귀

            
        }
    }
}
