using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ContentsType
{
    Inventory,
    Quest,
    Relation,
    Recipe,
}

public class MenuPopUp : BasePopUp
{
    [SerializeField] Button inventoryButtons;
    [SerializeField] GameObject inventory;

    [SerializeField] Button questButtons;
    [SerializeField] GameObject quest;

    [SerializeField] Button relationButtons;
    [SerializeField] GameObject relation;

    [SerializeField] Button recipeButtons;
    [SerializeField] GameObject recipe;

    Dictionary<ContentsType, GameObject> contentDic;


    public override void Awake()
    {
        base.Awake();
        popUpType = PopUpType.Menu;
        contentDic = new Dictionary<ContentsType, GameObject>()
        {
            {ContentsType.Inventory, inventory},
            {ContentsType.Quest ,quest},
            {ContentsType.Relation, relation},
            {ContentsType.Recipe, recipe},
        };

        inventoryButtons.onClick.AddListener(() => ShowContent(ContentsType.Inventory));
        questButtons.onClick.AddListener(() => ShowContent(ContentsType.Quest));
        relationButtons.onClick.AddListener(() => ShowContent(ContentsType.Relation));
        recipeButtons.onClick.AddListener(() => ShowContent(ContentsType.Recipe));
    }

    void ShowContent(ContentsType type)
    {
        foreach(var content in contentDic)
        {
            content.Value.SetActive(content.Key == type);
        }
    }
}
