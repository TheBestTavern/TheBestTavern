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

    [SerializeField] Image BG;
    [SerializeField] CanvasGroup submissionPanel;
    [SerializeField] CanvasGroup InventoryUI;
    [SerializeField] Slot submissionSlot;
    private Vector3 submissionPanelOriginalPos;
    private Vector3 InventoryUIOriginalPos;
    [SerializeField] Transform NPCPivot;
    [SerializeField] Button submitBtn;

    [Range(0f, 5f), SerializeField] float duration = 0.3f;
    private Color BGColor;
    bool isInitialized;

    Sequence currentSeq;
    Sequence showSeq;
    Sequence hideSeq;

    [SerializeField] Button devBtn; // 테스트용

    private void Init(NPCArea area)
    {
        //1. 인벤토리와 연결
        InventoryViewLoose submitInventory = InventoryUI.GetComponent<InventoryViewLoose>();
        submitInventory.OnEnableTargetSlot = TempSubmit;

        //2. 시퀀스 생성
        submitBtn.onClick.AddListener(Submit);
        this.area = area;
        submissionPanelOriginalPos = submissionPanel.transform.position;
        InventoryUIOriginalPos = InventoryUI.transform.position;
        BGColor = BG.color;

        showSeq = DOTween.Sequence();
        showSeq.Pause();
        showSeq.SetAutoKill(false);
        showSeq.AppendCallback(() =>
        {
            submissionPanel.gameObject.SetActive(true);
            InventoryUI.gameObject.SetActive(true);
        }); // 람다는 변수 자체가 캡쳐되어 실행 시점에 평가됨

        showSeq.Join(BG.DOFade(0.2f, duration).From(0).SetEase(Ease.OutQuad));
        showSeq.Join(submissionPanel.DOFade(1, duration).From(0));
        showSeq.Join(InventoryUI.DOFade(1, duration).From(0));
        showSeq.Join(submissionPanel.transform.DOMove(submissionPanelOriginalPos, duration).From(submissionPanelOriginalPos - new Vector3(10, 10, 0))); // 해당 vector 값은 seq 생성 시점에서 복사되어 고정됨.
        showSeq.Join(InventoryUI.transform.DOMove(InventoryUIOriginalPos, duration).From(InventoryUIOriginalPos - new Vector3(10, 10, 0))); // 해당 vector 값은 seq 생성 시점에서 복사되어 고정됨.

        hideSeq = DOTween.Sequence();
        hideSeq.Pause();
        hideSeq.SetAutoKill(false);
        hideSeq.AppendCallback(() =>
        {
            submissionPanel.gameObject.SetActive(true);
            InventoryUI.gameObject.SetActive(true);
        });
        hideSeq.Join(BG.DOFade(0, duration).From(0.2f).SetEase(Ease.OutQuad));
        hideSeq.Join(submissionPanel.DOFade(0, duration).From(1));
        hideSeq.Join(InventoryUI.DOFade(0, duration).From(1));
        hideSeq.Join(submissionPanel.transform.DOMove(submissionPanelOriginalPos - new Vector3(10, 10, 0), duration).From(submissionPanelOriginalPos).OnComplete(() =>
                {
                    submissionPanel.gameObject.SetActive(false);
                }));
        hideSeq.Join(InventoryUI.transform.DOMove(InventoryUIOriginalPos - new Vector3(10, 10, 0), duration).From(InventoryUIOriginalPos).OnComplete(() =>
        {
            InventoryUI.gameObject.SetActive(false);
        }));

        devBtn.onClick.AddListener(DevBtn); // 테스트용

        isInitialized = true;
    }

    public void OnEnter(Quest quest, NPCSlot npcSlot, NPCArea area)
    {
        if (!isInitialized) Init(area);

        gameObject.SetActive(true);
        this.npcSlot = npcSlot;
        this.quest = quest;

        // 제출 UI 나타남

        PlayeSeq(showSeq);

        this.npcSlot.transform.SetParent(transform, true);
        area.PlaceNPCsOutside(npcSlot.index); // 선택 안된 NPC 정렬
        MoveNPC(npcSlot); // // 선택된 NPC 정렬
    }

    private void OnExit()
    {
        npcSlot.OnExit();

        gameObject.SetActive(false);

        // npcSlot pool로 복귀, npcSlots에서 삭제
        area.HideNPC(npcSlot.index);

        //남은 npcSlot 화면에 재정렬
        area.PlaceNPCsInside();
    }

    private void MoveNPC(NPCSlot npcObj) // 클릭된 NPCSlot을 화면 중앙으로 이동.
    {
        npcObj.transform.DOMove(NPCPivot.position, duration + 0.2f).SetEase(Ease.OutQuad);
    }

    public void TempSubmit(List<Data_Foods> selectedItems) // 인벤토리에서 아이템 클릭시 실행. 인벤토리 클래스 내 이벤트에 구독.
    {
        if (selectedItems.Count == 0)
        {
            submissionSlot.Clear();
            return;
        }

        submissionSlot.SetSlot(selectedItems[0]);
    }


    public async void Submit() // 제출 버튼에 구독
    {
        if (submissionSlot == null)
        {
            await UIManager.Instance.ShowPopUp(PopUpType.Alarm);
            UIManager.Instance.alarmPopUp.SetAlarm("제출할 아이템을 먼저 선택하세요.");
        }
        else
        {
            Data_Foods itemForSubmission = submissionSlot.GetSlotItem();

            // todo - 퀘스트 제출 후처리 ( 성공 실패 대기열 등록, 아이템 감소 ) 성공 실패 체크와 효과는 다음날 나타남.

            //1. 성공 실패 체크 대기열 등록 (대기열 체크는 endDay 단계에서 확인)
            QuestManager.Instance.QuestCheckQueue.Enqueue((quest.origin.key, itemForSubmission.key));

            //2. 아이템 감소
            InventoryManager.Instance.Invens[InvenType.Player].아이템잃음(itemForSubmission, 1);
        }
        StartCoroutine(AfterSubmit());
    }

    private void PlayeSeq(Sequence sequence) // 제출 판넬, 인벤토리 나타남.
    {
        currentSeq?.Pause();
        currentSeq = sequence;

        if (sequence.IsPlaying() || sequence.IsComplete())
        {
            currentSeq.Restart();
        }
        else
        {
            currentSeq.Play();
        }
    }

    private void DevBtn()  // 테스트용
    {
        Debug.Log("dev-넘기기");
        QuestManager.Instance.QuestCheckQueue.Enqueue((quest.origin.key, 888888));
        StartCoroutine(AfterSubmit());
    }

    IEnumerator AfterSubmit()
    {
        //메세지 표시, 제출 UI 사라짐
        npcSlot.ShowMessage();

        PlayeSeq(hideSeq);
        yield return new WaitForSeconds(1);
        npcSlot.OnExit();
        OnExit();
    }
}
