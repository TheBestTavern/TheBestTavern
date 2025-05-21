using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    [SerializeField] Image BG;
    [SerializeField] Image BG_Tree;

    private void Awake()
    {
        EventBus.Subscribe<SeasonChangeEvent>(ToSetBG);
    }

    public void ToSetBG()
    {
        DesignEnums.SeasonType season = CalendarManager.Instance.CurrentSeasonType;

        SetBG(season);
    }

    public void ToSetBG(SeasonChangeEvent seasonChangeEvent)
    {
        SetBG(seasonChangeEvent.season);
    }

    private async void SetBG(DesignEnums.SeasonType season)
    {
        var result1 = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>($"Main_Tree_{season.ToString()}");
        var result2 = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>($"Main_BG_{season.ToString()}");
        BG_Tree.sprite = result1;
        BG.sprite = result2;
    }

    private void OnDestroy()
    {
        EventBus.UnSubscribe<SeasonChangeEvent>(ToSetBG);
    }
}
