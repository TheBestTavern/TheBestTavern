using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToolTip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI Grade;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform rectTransform;
    Coroutine currentCoroutine;
    Tween currentTween;

    Dictionary<int, Data_FoodCategory> foodCategoryDict;

    private void Awake()
    {
        gameObject.SetActive(false);
        foodCategoryDict = DataManager.Instance.DataLoader_FoodCategory.ItemsDict;
    }

    public void ShowToolTip(SlotHoverEnterEvent evt)
    {
        ShowToolTip(evt.ID);
    }

    public void HideToolTip(SlotHoverEndEvent evt)
    {
        HideToolTip();
    }

    public void ShowToolTip(int itemID)
    {
        gameObject.SetActive(true);
        canvas.sortingOrder = PopUpManager.Instance.GetNextSortingOrder();

        currentTween?.Kill();
        currentTween = canvasGroup.DOFade(0.8f, 0.2f);

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(UpdatePos());

        var food = Data.GetRawItem(itemID);
        //var foodCategory = foodCategoryDict[food.FoodCategory];
        //Name.text = foodCategory.categoryName;
        Name.text = food.name;
        switch (food.grade)
        {
            case DesignEnums.GradeType.legendary:
                Grade.text = "전설";
                break;
            case DesignEnums.GradeType.rare:
                Grade.text = "희귀";
                break;
            case DesignEnums.GradeType.common:
                Grade.text = "일반";
                break;
            default:
                Debug.Log($"{Name.text}({itemID}) 은 등급이 없어요");
                break;
        }
    }

    public void HideToolTip()
    {
        currentTween?.Kill();
        currentTween = canvasGroup.DOFade(0, 0.2f).OnComplete(() =>
            {
                if (currentCoroutine != null)
                    StopCoroutine(currentCoroutine);
                gameObject.SetActive(false);
            });
    }

    private IEnumerator UpdatePos()
    {
        while (true)
        {
            rectTransform.position = Input.mousePosition;
            yield return null;
        }
    }
}