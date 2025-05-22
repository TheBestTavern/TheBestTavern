using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DishBookSlot : BaseBookSlot<Data_Book_Dish>
{

    [SerializeField] List<TextMeshProUGUI> ingredients;


    public async override void SetSlot(Data_Book_Dish thing)
    {
        if (HideUndiscoveredFood && !ItemRecordManager.Instance.IsDiscovered(thing.key))
        {
            SetUndiscoveredItem();
        }
        else
        {
            detailBtn.interactable = true;
            foodCatergoryID = thing.key;

            //icon.sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>($"Assets/16.Image/FoodImage/{thing.resultFoodEnglishName}.png", true);
            icon.sprite = await AddressablesLoader.Instance.AddressablesLoadSpriteFromAtlasAsync("FoodSpriteAtlas", thing.resultFoodEnglishName);

            foodName.text = thing.name;
            desc.text = thing.description;
            int i = 0;
            for (; i < thing.ingredientsName.Count; i++)
            {
                ingredients[i].gameObject.SetActive(true);
                ingredients[i].text = thing.ingredientsName[i];
            }
            for (; i < ingredients.Count; i++)
            {
                ingredients[i].gameObject.SetActive(false);
            }
        }
    }

    protected override void SetUndiscoveredItem()
    {
        base.SetUndiscoveredItem();
        foreach (var ingredient in ingredients)
        {
            ingredient.text = "";
        }
    }
}

