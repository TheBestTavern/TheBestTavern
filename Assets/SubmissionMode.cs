using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmissionMode : MonoBehaviour
{
    bool isReady;
    Button submitBtn;
    Item submissionSlot;

    private void Init()
    {
        if (isReady) return;

        submitBtn.onClick.AddListener(() => Submit(submissionSlot));

        isReady = true;
    }

    public void OnEnter()
    {
        Init();

        //todo- 백그라운드 투명도 상승.
        MoveNPC(CopyNPC());
    }

    private void OnExit()
    {

    }

    private NPCSlot CopyNPC(NPCSlot npcSlot) // Area에서 선택된 npc 오브젝트를 복사.
    {
        GameObject go = Instantiate(npcSlot.gameObject);
        return npcSlot;
    }

    private void MoveNPC() // npc 비동기적 움직임
    {

    }

    public void TempSubmit() // 인벤토리에서 아이템 클릭시 실행. 인벤토리 클래스 내 이벤트에 구독.
    {

    }

    public  void Submit(Item itemForSubmission) // 제출 버튼에 구독
    {

    }
}
