using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public enum PopUpType
{
    Setting,
    Menu,
    SelectMap,
    Confirm,
    MiniGame,
    MailBox,
    Alarm,
    OfferLetter,
    ResultLetter,
    CookingResult,
    GatheringResult,
    SoundSetting,
    Letter,
    FoodDetail,
}

public interface IPopupManager
{
    public void PopupOpen(int id);
    public void PopupClose(int id);
    public void RecoverID(int id);
    public int GetNextSortingOrder();
}

public class PopUpManager : MonoSingleton<PopUpManager>, IPopupManager
{
    // 이미 불러왔던 팝업 목록 - 이미 불러왔다면 다시 안불러올 수 있도록 사용하는 용도 
    private Dictionary<PopUpType, BasePopUp> popUps = new Dictionary<PopUpType, BasePopUp>();

    // 확인 팝업 - 여러 곳에서 쓰이고 각자 사용하는 용도가 달라 각자 접근 할 수 있도록 캐싱  
    public ConfirmPopUp confirmPopUp;

    public AlarmPopUp alarmPopUp;

    int sortingOrderIndex = 100;
    public Stack<int> PopupIDs = new();
    public List<int> UsingPopups = new();

    ToolTipManager toolTipManager;


    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(this);
        GetReadyIDs(PopupIDs, 1000, 9999);

        toolTipManager = new();
        toolTipManager.Initialize();
    }

    void GetReadyIDs(Stack<int> IDs, int from, int to)
    {
        while (to >= from)
        {
            IDs.Push(to--);
        }
    }

    // 팝업 보여주기 함수 
    public async Task<BasePopUp> ShowPopUp(PopUpType popUpType)
    {
        // 해당 팝업을 한번도 불러온 적이 없다면  
        if (!popUps.TryGetValue(popUpType, out BasePopUp basePopUp))
        {
            // Addressables로 해당 팝업 프리펩 불러오기 
            GameObject popUpGameObject =
                await AddressablesLoader.Instance.AddressablesLoadAsync<GameObject>($"{popUpType.ToString()}PopUpPrefab.prefab");

            // 불러온 팝업 인스턴스화
            popUpGameObject = Instantiate(popUpGameObject);

            // BasePopUp 클래스 불러오기 - 
            basePopUp = popUpGameObject.GetComponentInChildren<BasePopUp>();
            basePopUp.Init(PopupIDs.Pop(), this);

            // 한번 불러온 팝업 딕셔너리에 넣어주기
            popUps.Add(popUpType, basePopUp);
        }

        // 확인 팝업이라면 
        if (popUpType == PopUpType.Confirm)
        {
            // ConfirmPopUp 클래스 캐싱
            confirmPopUp = basePopUp.GetComponent<ConfirmPopUp>();
        }
        else if (popUpType == PopUpType.Alarm)
        {
            alarmPopUp = basePopUp.GetComponent<AlarmPopUp>();
        }

        // 각 팝업들이 열릴 때 필요한 함수 실행
        basePopUp.OnOpen();

        return basePopUp;
    }

    public void PopupOpen(int id)
    {
        UsingPopups.Add(id);
    }

    public void PopupClose(int id)
    {
        UsingPopups.Remove(id);
        if (UsingPopups.Count == 0)
        {
            sortingOrderIndex = 100;
        }
    }

    public void RecoverID(int id)
    {
        PopupIDs.Push(id);
    }

    public int GetNextSortingOrder()
    {
        return sortingOrderIndex++;
    }

    public void OnSceneMove()
    {
        popUps.Clear();
        Debug.Log("팝업 클리어");
    }
}
