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
    [SerializeField] CanvasGroup submissionPanel;
    [SerializeField] Transform NPCPivot;
    [SerializeField] Image BG;
    Item submissionSlot;
    bool isReady;

    Sequence currentSeq;
    Sequence showSeq;
    Sequence hideSeq;


    private void Init(NPCArea area)
    {
        submitBtn.onClick.AddListener(() => Submit(submissionSlot));
        this.area = area;

        showSeq = DOTween.Sequence();
        showSeq.Pause();
        showSeq.AppendCallback(() => submissionPanel.transform.position -= new Vector3(-10, -10, 0)); // 람다는 변수가 캡쳐되어 실행 시점에 평가됨
        showSeq.Join(BG.DOFade(0.2f, duration).SetEase(Ease.OutQuad));
        showSeq.Join(submissionPanel.DOFade(1, duration));
        showSeq.Join(submissionPanel.transform.DOMove(
            new Vector2(submissionPanel.transform.position.x + 10, submissionPanel.transform.position.y + 10), duration)); // 해당 vector 값은 seq 생성 시점에서 복사되어 고정됨.

        hideSeq = DOTween.Sequence();
        hideSeq.Pause();
        hideSeq.Join(BG.DOFade(0, duration).SetEase(Ease.OutQuad));
        hideSeq.Join(submissionPanel.DOFade(1, duration));
        hideSeq.Join(submissionPanel.transform.DOMove(
                new Vector2(submissionPanel.transform.position.x - 10, submissionPanel.transform.position.y - 10), duration
                ).OnComplete(() => submissionPanel.transform.position -= new Vector3(10, 10, 0)));

        isReady = true;
    }

    public void OnEnter(Quest quest, NPCSlot npcSlot, NPCArea area)
    {
        if (!isReady) Init(area);

        gameObject.SetActive(true);
        this.npcSlot = npcSlot;
        this.quest = quest;

        // 제출 UI 나타남
        submissionPanel.gameObject.SetActive(true);
        PlayeSeq(showSeq);

        this.npcSlot.transform.SetParent(transform, true);
        area.PlaceNPCsOutside(npcSlot.index); // 선택 안된 NPC 정렬
        MoveNPC(npcSlot); // // 선택된 NPC 정렬
    }

    private void OnExit() // 실행 위치 아직 미정.
    {
        // npcSlot pool로 복귀, npcSlots에서 삭제
        area.HideNPC(npcSlot.index);

        // 제출 UI 사라짐
        submissionPanel.gameObject.SetActive(false);
        PlayeSeq(hideSeq);

        //남은 npcSlot 화면에 재정렬
        area.PlaceNPCsInside();
    }

    private void MoveNPC(NPCSlot npcObj) // 클릭된 NPCSlot을 화면 중앙으로 이동.
    {
        npcObj.transform.DOMove(NPCPivot.position, duration + 0.2f).SetEase(Ease.OutQuad);
    }

    public void TempSubmit(Item item) // 인벤토리에서 아이템 클릭시 실행. 인벤토리 클래스 내 이벤트에 구독.
    {
        submissionSlot = item;
    }


    public async void Submit(Item itemForSubmission) // 제출 버튼에 구독
    {
        if (submissionSlot == null)
        {
            await UIManager.Instance.ShowPopUp(PopUpType.Alarm);
            UIManager.Instance.alarmPopUp.SetAlarm("제출할 아이템을 먼저 선택하세요.");
        }
        else
        {
            // todo - 퀘스트 제출 후처리 ( 성공 실패 대기열 등록, 아이템 감소 ) 성공 실패 체크와 효과는 다음날 나타남.

            //1. 성공 실패 체크 대기열 등록 (대기열 체크는 endDay 단계에서 확인)
            QuestManager.Instance.questCheckQueue.Enqueue((quest.origin.key, itemForSubmission.origin.key));

            //2. 아이템 감소
        }
    }

    private void PlayeSeq(Sequence sequence) // 제출 판넬, 인벤토리 나타남.
    {
        currentSeq?.Kill();

        currentSeq = sequence;
        currentSeq.Play();
    }

    //private void SetSubmitUI(bool OnOff) // 제출 판넬, 인벤토리 나타남.
    //{
    //    if (OnOff)
    //    {
    //        currentSeq?.Kill();

    //        currentSeq = showSeq;
    //        currentSeq.Play();
    //    }
    //    else
    //    {
    //        currentSeq?.Kill();

    //        currentSeq = hideSeq;
    //        currentSeq.Play();
    //    }
    //}
}
