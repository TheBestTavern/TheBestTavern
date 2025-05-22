using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    [SerializeField] Image BG;
    [SerializeField] Image BG_Tree;

    public DesignEnums.SeasonType? currentSeason;

    private void Awake()
    {
        EventBus.Subscribe<SeasonChangeEvent>(ToSetBG);
    }

    private void Start()
    {
        ToSetBG();
    }

    public void ToSetBG()
    {
        DesignEnums.SeasonType? season = CalendarManager.Instance.CurrentSeasonType;
        if(season != null)
        SetBG(season.Value);
    }

    public void ToSetBG(SeasonChangeEvent seasonChangeEvent)
    {
        if (seasonChangeEvent.season != currentSeason)
        {
            currentSeason = seasonChangeEvent.season;
            SetBG(currentSeason.Value);
        }
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
