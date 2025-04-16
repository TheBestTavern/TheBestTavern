using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// TAB 메뉴 컨텐츠 타입
/// </summary>
public enum ContentsType
{
    Inventory,
    Quest,
    Relation,
    Recipe,
}

/// <summary>
/// TAB 메뉴 팝업
/// </summary>
public class MenuPopUp : BasePopUp
{
    // 인벤토리 보기 버튼
    [SerializeField] Button inventoryButton;
    // 인벤토리 게임 오브젝트 
    [SerializeField] GameObject inventoryGameObject;

    // 퀘스트 보기 버튼
    [SerializeField] Button questButton;
    // 퀘스트 오브젝트
    [SerializeField] GameObject questGameObject;

    // NPC 관계 보기 버튼
    [SerializeField] Button relationButton;
    // NPC 관계 게임 오브젝트 
    [SerializeField] GameObject relationGameObject;

    // 레시피 보기 버튼
    [SerializeField] Button recipeButton;
    // 레시피 게임 오브젝트
    [SerializeField] GameObject recipeGameObject;

    // 메뉴 컨텐츠 딕셔너리
    Dictionary<ContentsType, GameObject> contentDic;


    public override void Awake()
    {
        base.Awake();

        // 팝업 타입 메뉴로 설정
        popUpType = PopUpType.Menu;

        // 컨텐츠 딕셔너리 초기화
        contentDic = new Dictionary<ContentsType, GameObject>()
        {
            {ContentsType.Inventory, inventoryGameObject},
            {ContentsType.Quest ,questGameObject},
            {ContentsType.Relation, relationGameObject},
            {ContentsType.Recipe, recipeGameObject},
        };

        // 인벤토리 보기 버튼 클릭 이벤트 리스너 추가
        inventoryButton.onClick.AddListener(() => ShowContent(ContentsType.Inventory));
        // 퀘스트 보기 버튼 클릭 이벤트 리스너 추가
        questButton.onClick.AddListener(() => ShowContent(ContentsType.Quest));
        // NPC 관계 보기 버튼 클릭 이벤트 리스너 추가
        relationButton.onClick.AddListener(() => ShowContent(ContentsType.Relation));
        // 레시피 보기 버튼 클릭 이벤트 리스너 추가
        recipeButton.onClick.AddListener(() => ShowContent(ContentsType.Recipe));
    }
    
    // 메뉴 컨텐츠 보여주기 함수
    void ShowContent(ContentsType type)
    {
        // 메뉴 컨텐츠 순회
        foreach (var content in contentDic)
        {
            // 입력된 메뉴 컨텐츠만 활성화
            content.Value.SetActive(content.Key == type);
        }
    }

    // 팝업 열때 필요한 함수
    public override void OnOpen()
    {
        base.OnOpen();
        // 메뉴 팝업 아래로 내려오기 애니메이션
        transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosY(320, 1f).SetEase(Ease.OutCubic);
    }

    // 팝업 닫을 때 필요한 함수
    public override void OnClose()
    {
        base.OnClose();
        // 메뉴팝업 위로 올라가기 애니메이션 후 비활성화
        transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosY(1640, 1f).OnComplete(()=> gameObject.SetActive(false));
    }
}
