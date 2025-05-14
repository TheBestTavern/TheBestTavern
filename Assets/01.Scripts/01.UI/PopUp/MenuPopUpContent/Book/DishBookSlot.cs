using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DishBookSlot : BaseBookSlot<Data_Book_Dish>
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI foodName;
    [SerializeField] TextMeshProUGUI desc;
    [SerializeField] List<TextMeshProUGUI> ingredients;


    public async override void SetSlot(Data_Book_Dish thing)
    {
        icon.sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>($"Assets/16.Image/FoodImage/{thing.resultFoodEnglishName}.png", true);
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

