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

    private string nextSceneName = "";
    private bool isLoadMode = false;

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
        if (SaveLoadManager.Instance.LoadData(out _))
            renewPanel.SetActive(true);
        else
            tutorialSkipPanel.SetActive(true);
    }

    private void HandleLoadGame()
    {
        isLoadMode = true;
        nextSceneName = "MainScene";
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
        nextSceneName = doTutorial ? "TutorialScene" : "MainScene";
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
        await InitializeAnalytics();

        if (isLoadMode)
        {
            if (SaveLoadManager.Instance.LoadData(out PlayerGameData data))
            {
                ApplyPlayerData(data);
                await SceneLoader.Instance.LoadSceneAsync("MainScene");
                return;
            }

            Debug.LogWarning("불러오기 실패");
        }

        await SceneLoader.Instance.LoadSceneAsync(nextSceneName);
    }

    private async void OnRejectAnalytics()
    {
        GameManager.Instance.isAnalyticsAgreed = false;        
        if (isLoadMode)
        {
            if (SaveLoadManager.Instance.LoadData(out PlayerGameData data))
            {
                ApplyPlayerData(data);
                await SceneLoader.Instance.LoadSceneAsync("MainScene");
                return;
            }

            Debug.LogWarning("불러오기 실패");
        }
        await SceneLoader.Instance.LoadSceneAsync(nextSceneName);
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
            data.JustCompleteQuests, data.TodayAvailableQuest, data.QuestCheckQueue
        );
        CalendarManager.Instance.ApplyLoadData(data.CurrentSeasonType);

        foreach (var item in data.playerInvenData.ItemList)
        {
            InventoryManager.Instance.Invens[InvenType.Player].AcquireItem(item.Origin, item.Count);
        }
    }
}
