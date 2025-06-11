using System.Threading.Tasks;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    [Header("메인 버튼")]
    [SerializeField] private Button gameStartButton;
    [SerializeField] private Button gameLoadStartButton;
    [SerializeField] private Button gameExitButton;

    [Header("튜토리얼 선택")]
    [SerializeField] private GameObject tutorialSkipPanel;
    [SerializeField] private Button tutorialSkipButton;
    [SerializeField] private Button doTutorialButton;

    [Header("기존 데이터 갱신 확인")]
    [SerializeField] private GameObject renewPanel;
    [SerializeField] private Button renewButton;
    [SerializeField] private Button rejectButton;

    [Header("애널리틱스 동의")]
    [SerializeField] private GameObject acceptAnalyticsPanel;
    [SerializeField] private Button acceptAnalyticsButton;
    [SerializeField] private Button rejectAnalyticsButton;

    private SceneType? nextSceneName = null;
    private bool isLoadMode = false;
    private bool loadFail = false;

    private bool? doTutorial = null;

    void Start()
    {
        UIManager.Instance.startSceneUI = this;

        // 버튼 리스너 등록
        gameStartButton.onClick.AddListener(HandleNewGame);
        gameLoadStartButton.onClick.AddListener(HandleLoadGame);
        gameExitButton.onClick.AddListener(ExitGame);

        tutorialSkipButton.onClick.AddListener(() => ConfirmTutorial(false));
        doTutorialButton.onClick.AddListener(() => ConfirmTutorial(true));

        renewButton.onClick.AddListener(() => ShowTutorialChoice(true));
        rejectButton.onClick.AddListener(() => ShowTutorialChoice(false));

        acceptAnalyticsButton.onClick.AddListener(OnAcceptAnalytics);
        rejectAnalyticsButton.onClick.AddListener(OnRejectAnalytics);
    }

    private void HandleNewGame()
    {
        isLoadMode = false;
        if (SaveLoadManager.Instance.LoadData(out _))
            renewPanel.SetActive(true);
        else
            tutorialSkipPanel.SetActive(true);
    }

    private void HandleLoadGame()
    {
        isLoadMode = true;
        nextSceneName = SceneType.MainScene;
        acceptAnalyticsPanel.SetActive(true);
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void ConfirmTutorial(bool doTutorial)
    {
        nextSceneName = doTutorial ? SceneType.TutorialScene : SceneType.MainScene;
        this.doTutorial = doTutorial;

        tutorialSkipPanel.SetActive(false);
        acceptAnalyticsPanel.SetActive(true);
    }

    private void ShowTutorialChoice(bool show)
    {
        renewPanel.SetActive(false);
        if (show)
            tutorialSkipPanel.SetActive(true);
    }

    private async void OnAcceptAnalytics()
    {
        acceptAnalyticsButton.interactable = false;
        rejectAnalyticsButton.interactable = false;
        loadFail = false;

        await InitializeAnalytics();

        if (doTutorial != null)
        {
            var tutorialEvent = new AnalyticsTutorial("TutorialData")
            {
                watchTutorial = (bool)doTutorial
            };
            AnalyticsService.Instance.RecordEvent(tutorialEvent);
        }

        if (isLoadMode)
        {
            if (SaveLoadManager.Instance.LoadData(out PlayerGameData data))
            {
                ApplyPlayerData(data);
                await SceneLoader.Instance.LoadSceneAsync(SceneType.MainScene);
                return;
            }
            else
            {
                Debug.LogWarning("불러오기 실패");
                await PopUpManager.Instance.ShowPopUp(PopUpType.Alarm);
                PopUpManager.Instance.alarmPopUp.SetAlarm("저장된 데이터가 없습니다.");
                loadFail = true;
                acceptAnalyticsPanel.SetActive(false);
                acceptAnalyticsButton.interactable = true;
                rejectAnalyticsButton.interactable = true;
            }

        }

        if (nextSceneName != null && !loadFail)
            await SceneLoader.Instance.LoadSceneAsync(nextSceneName.Value);
    }

    private async void OnRejectAnalytics()
    {
        acceptAnalyticsButton.interactable = false;
        rejectAnalyticsButton.interactable = false;
        loadFail = false;

        GameManager.Instance.isAnalyticsAgreed = false;

        if (isLoadMode)
        {
            if (SaveLoadManager.Instance.LoadData(out PlayerGameData data))
            {
                ApplyPlayerData(data); 
                await SceneLoader.Instance.LoadSceneAsync(SceneType.MainScene);
                return;
            }
            else 
            {
                Debug.LogWarning("불러오기 실패");
                await PopUpManager.Instance.ShowPopUp(PopUpType.Alarm);
                PopUpManager.Instance.alarmPopUp.SetAlarm("저장된 데이터가 없습니다.");
                loadFail = true;
                acceptAnalyticsPanel.SetActive(false);
                acceptAnalyticsButton.interactable = true;
                rejectAnalyticsButton.interactable = true;
            }
        }

        if (nextSceneName != null && !loadFail)
            await SceneLoader.Instance.LoadSceneAsync(nextSceneName.Value);
    }

    private async Task InitializeAnalytics()
    {
        GameManager.Instance.isAnalyticsAgreed = true;
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
    }

    private void ApplyPlayerData(PlayerGameData data)
    {
        TimerManager.Instance.ApplyLoadData(data.today);
        ItemRecordManager.Instance.ApplyLoadData(data.itemRecords);
        NPCManager.Instance.NPCData.ApplyLoadData(data.AllNPC);
        QuestManager.Instance.questData.ApplyLoadData(
            data.AllQuests, data.AcceptedQuests, data.OnceSuccessQuests,
            data.JustCompleteQuests, data.TodayAvailableQuest, data.QuestCheckQueueForSerialization, data.TodaySpawnNPC
        );
        CalendarManager.Instance.ApplyLoadData(data.CurrentSeasonType);
        ItemStackManager.Instance.ApplyLoadData(data.IDsForSerialization, data.AllItemStack);
        InventoryManager.Instance.Invens[InvenType.Player].ApplyLoadData(data.foodKey2IDs, data.itemStackIDs);

        EndingManager.Instance.SetEndingState(data.hasSeenEnding);
    }
}
