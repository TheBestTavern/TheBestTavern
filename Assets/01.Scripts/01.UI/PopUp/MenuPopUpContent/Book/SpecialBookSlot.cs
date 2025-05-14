using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpecialBookSlot : BaseBookSlot<Data_Book_Special>
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI foodName;
    [SerializeField] TextMeshProUGUI desc;
    [SerializeField] TextMeshProUGUI npcName;

    public async override void SetSlot(Data_Book_Special thing)
    {
        icon.sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>($"Assets/16.Image/FoodImage/{thing.englishName}.png", true);

        foodName.text = thing.name;
        desc.text = thing.description;
        npcName.text = thing.givingNPCName;
    }
}

