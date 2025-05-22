using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookQuestLetter : BasePopUp
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI bodyText;
    [SerializeField] TextMeshProUGUI from;

    [SerializeField] GameObject compensation;
    [SerializeField] TextMeshProUGUI compensationItemName;
    [SerializeField] Image image;

    // 편지 열때마다 필요한 초기화.
    public async void SetLetter(Quest quest, bool isInProgressQuest)
    {
        title.text = quest.Origin.name;
        string colored = quest.Origin.letterOffer;
        foreach (string keyword in quest.Origin.letterOfferKeyword)
        {
            colored = colored.Replace(keyword, $"<b><color=#2C6DA6>{keyword}</color></b>");
        }
        bodyText.text = colored;
        from.text = $"-{Data.GetNPC(quest.Origin.givingNPC).Origin.name}-";
        if (!isInProgressQuest)
        {
            var rawItem = Data.GetRawItem(quest.Origin.compensationID);
            image.sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>($"Assets/16.Image/FoodImage/{rawItem.englishName}.png", true);
            compensationItemName.text = rawItem.name;
            compensation.gameObject.SetActive(true);
        }
        else
        {
            compensation.gameObject.SetActive(false);
        }
    }
}
