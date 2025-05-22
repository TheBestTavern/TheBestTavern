using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpecialBookSlot : BaseBookSlot<Data_Book_Special>
{

    [SerializeField] TextMeshProUGUI npcName;

    public async override void SetSlot(Data_Book_Special thing)
    {
        if (HideUndiscoveredFood && !ItemRecordManager.Instance.IsDiscovered(thing.key))
        {
            SetUndiscoveredItem();
        }
        else
        {
            detailBtn.interactable = true;
            foodCatergoryID = thing.key;
            title.text = "획득 NPC";

            //icon.sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>($"Assets/16.Image/FoodImage/{thing.englishName}.png", true);
            icon.sprite = await AddressablesLoader.Instance.AddressablesLoadSpriteFromAtlasAsync("FoodSpriteAtlas", thing.englishName);

            foodName.text = thing.name;
            desc.text = thing.description;
            npcName.text = thing.givingNPCName;
        }
    }

    protected override void SetUndiscoveredItem()
    {
        base.SetUndiscoveredItem();
        title.text = "";
        npcName.text = "";
    }
}

