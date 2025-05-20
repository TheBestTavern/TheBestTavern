using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLetter : BasePopUp
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI bodyText;
    [SerializeField] TextMeshProUGUI from;
    [SerializeField] Image image;

    // 편지 열때마다 필요한 초기화.
    public async void SetLetter(Quest quest, bool isInProgressQuest)
    {
        title.text = quest.Origin.name;
        bodyText.text = quest.Origin.letterOffer;
        from.text = Data.GetNPC(quest.Origin.givingNPC).Origin.name;
        if (!isInProgressQuest)
        {
            string englishName = Data.GetRawItem(quest.Origin.compensationID).englishName;
            image.sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>($"Assets/16.Image/FoodImage/{englishName}.png", true);
        }
    }
}
