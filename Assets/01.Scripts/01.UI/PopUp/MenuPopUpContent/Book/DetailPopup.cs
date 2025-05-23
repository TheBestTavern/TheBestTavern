using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static DesignEnums;

public class DetailPopup : BasePopUp
{
    Stack<Data_FoodCategory> prevStack = new();
    Stack<Data_FoodCategory> nextStack = new();
    Data_FoodCategory current;

    [SerializeField] Button prevBtn;
    [SerializeField] Button nextBtn;

    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI foodName;
    [SerializeField] TextMeshProUGUI ItemType;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] List<Button> componentsBtn;
    [SerializeField] List<TextMeshProUGUI> componentsTMP;
    List<int> componentsID = new();
    [SerializeField] TextMeshProUGUI desc;

    public override void Init(int id, IPopupManager manager)
    {
        base.Init(id, manager);
        prevBtn.onClick.AddListener(OnPrevBtnClick);
        nextBtn.onClick.AddListener(OnNextBtnClick);

        for (int i = 0; i < componentsBtn.Count; i++)
        {
            int j = i;
            componentsBtn[i].onClick.AddListener(() => OnClickComponentsBtn(j));
        }
    }

    public void NewDetail(int foodCategoryID)
    {
        var newFoodCategory = DataManager.Instance.DataLoader_FoodCategory.GetByKey(foodCategoryID);

        // 가공재료는 기획상 도감이 없으므로 따로 원재료로 변환하여 처리함.
        if (newFoodCategory.itemType == DesignEnums.ItemType.processed)
        {
            newFoodCategory = Data.GetIngredientFromProcessed(newFoodCategory);
        }

        if (newFoodCategory == current) return;

        if (current != null)
            prevStack.Push(current);

        current = newFoodCategory;
        Set();
    }

    private void OnPrevBtnClick()
    {
        if (prevStack.TryPop(out var temp))
        {
            nextStack.Push(current);
            current = temp;
            Set();
        }
    }

    private void OnNextBtnClick()
    {
        if (nextStack.TryPop(out var temp))
        {
            prevStack.Push(current);
            current = temp;
            Set();
        }
    }

    private void Set()
    {
        SetSortingOrder();
        switch (current.itemType)
        {
            case DesignEnums.ItemType.ingredient:
                SetIngredient();
                break;
            case DesignEnums.ItemType.special:
                SetSpecial();
                break;
            case DesignEnums.ItemType.mix:
                SetMix();
                break;
            case DesignEnums.ItemType.dish:
                SetDish();
                break;
        }
    }

    private async void SetIngredient()
    {
        var thing = DataManager.Instance.DataLoader_Book_Ingredient.GetByKey(current.key);
        icon.sprite = await AddressablesLoader.Instance.AddressablesLoadSpriteFromAtlasAsync("FoodSpriteAtlas", thing.englishName, true);
        foodName.text = thing.name;
        desc.text = thing.description;
        ItemType.text = "원재료";
        ItemType.color = new Color32(94, 124, 0, 255);
        title.text = "획득 조건";

        List<string> regionString = new();
        for (int i = 0; i < thing.region.Count; i++)
        {
            switch (thing.region[i])
            {
                case DesignEnums.RegionType.gyeonggi:
                    regionString.Add("경기도");
                    break;
                case DesignEnums.RegionType.gangwon:
                    regionString.Add("강원도");
                    break;
                case DesignEnums.RegionType.chungcheong:
                    regionString.Add("충청도");
                    break;
                case DesignEnums.RegionType.jeolla:
                    regionString.Add("전라도");
                    break;
                case DesignEnums.RegionType.gyeongsang:
                    regionString.Add("경상도");
                    break;
            }
        }
        string regionS = "";
        foreach (string region in regionString)
        {
            regionS += region + ", ";
        }
        regionS = regionS.Substring(0, regionS.Length - 2);
        componentsTMP[0].text = regionS;

        string biomeS = "";
        switch (thing.biome)
        {
            case DesignEnums.BiomeType.forest:
                biomeS = "숲";
                break;
            case DesignEnums.BiomeType.sea:
                biomeS = "바다";
                break;
        }
        componentsTMP[1].text = biomeS;

        List<string> seasonString = new();
        for (int i = 0; i < thing.season.Count; i++)
        {
            switch (thing.season[i])
            {
                case DesignEnums.SeasonType.spring:
                    seasonString.Add("봄");
                    break;
                case DesignEnums.SeasonType.summer:
                    seasonString.Add("여름");
                    break;
                case DesignEnums.SeasonType.fall:
                    seasonString.Add("가을");
                    break;
                case DesignEnums.SeasonType.winter:
                    seasonString.Add("겨울");
                    break;
            }
        }
        string seasonS = "";
        foreach (string season in seasonString)
        {
            seasonS += season + ", ";
        }
        componentsTMP[2].text = seasonS.Substring(0, seasonS.Length - 2);

        componentsBtn[0].gameObject.SetActive(true);
        componentsBtn[1].gameObject.SetActive(true);
        componentsBtn[2].gameObject.SetActive(true);
        componentsBtn[3].gameObject.SetActive(false);

        foreach (var btn in componentsBtn)
        {
            btn.interactable = false;
        }
    }

    private async void SetSpecial()
    {
        var thing = DataManager.Instance.DataLoader_Book_Special.GetByKey(current.key);
        icon.sprite = await AddressablesLoader.Instance.AddressablesLoadSpriteFromAtlasAsync("FoodSpriteAtlas", thing.englishName, true);
        foodName.text = thing.name;
        desc.text = thing.description;
        ItemType.text = "특수재료";
        ItemType.color = new Color32(209, 90, 153, 255);

        title.text = "지급 NPC";
        componentsTMP[0].text = thing.givingNPCName;

        componentsBtn[0].gameObject.SetActive(true);
        componentsBtn[1].gameObject.SetActive(false);
        componentsBtn[2].gameObject.SetActive(false);
        componentsBtn[3].gameObject.SetActive(false);

        foreach (var btn in componentsBtn)
        {
            btn.interactable = false;
        }
    }
    private async void SetMix()
    {
        var thing = DataManager.Instance.DataLoader_Book_Mix.GetByKey(current.key);
        icon.sprite = await AddressablesLoader.Instance.AddressablesLoadSpriteFromAtlasAsync("FoodSpriteAtlas", thing.resultFoodEnglishName, true);
        foodName.text = thing.name;
        desc.text = thing.description;
        ItemType.text = "조합재료";
        ItemType.color = new Color32(16, 93, 152, 255);
        title.text = "재료 목록";

        int i = 0;
        componentsID.Clear();
        for (; i < thing.ingredientsName.Count; i++)
        {
            componentsBtn[i].gameObject.SetActive(true);
            componentsBtn[i].interactable = true;
            componentsTMP[i].text = thing.ingredientsName[i];
            //componentsID[i] = thing.ingredients[i];
            componentsID.Add(thing.ingredients[i]);
        }
        for (; i < componentsBtn.Count; i++)
        {
            componentsBtn[i].gameObject.SetActive(false);
        }
    }
    private async void SetDish()
    {
        var thing = DataManager.Instance.DataLoader_Book_Dish.GetByKey(current.key);
        icon.sprite = await AddressablesLoader.Instance.AddressablesLoadSpriteFromAtlasAsync("FoodSpriteAtlas", thing.resultFoodEnglishName, true);
        foodName.text = thing.name;
        desc.text = thing.description;
        ItemType.text = "요리";
        ItemType.color = new Color32(169, 87, 24, 255);
        title.text = "재료 목록";

        int i = 0;
        componentsID.Clear();
        for (; i < thing.ingredientsName.Count; i++)
        {
            componentsBtn[i].gameObject.SetActive(true);
            componentsBtn[i].interactable = true;
            componentsTMP[i].text = thing.ingredientsName[i];
            componentsID.Add(thing.ingredients[i]);
        }
        for (; i < componentsBtn.Count; i++)
        {
            componentsBtn[i].gameObject.SetActive(false);
        }
    }

    private void OnClickComponentsBtn(int index)
    {
        NewDetail(componentsID[index]);
    }
}
