using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class BackGround : MonoBehaviour, IRunOnEnter
{
    [SerializeField] Image BG;
    [SerializeField] Image BG_Tree;

    public List<Image> images;

    public DesignEnums.SeasonType? currentSeason;

    private async void Awake()
    {
        EventBus.Subscribe<SeasonChangeEvent>(ToSetBG);

        await DayAndNightManager.Instance.InitAsync();
        foreach (var i in images)
        {
            i.material = DayAndNightManager.Instance.nightMat;
        }
    }

    public async UniTask RunAhead()
    {
        await ToSetBG();
    }

    //private void Start()
    //{
    //    ToSetBG();
    //}

    public async UniTask ToSetBG()
    {
        DesignEnums.SeasonType? season = CalendarManager.Instance.CurrentSeasonType;
        if (season != null)
            await SetBG(season.Value);
    }

    public void ToSetBG(SeasonChangeEvent seasonChangeEvent)
    {
        if (seasonChangeEvent.season != currentSeason)
        {
            currentSeason = seasonChangeEvent.season;
            SetBG(currentSeason.Value);
        }
    }

    private async UniTask SetBG(DesignEnums.SeasonType season)
    {
        BG_Tree.sprite = await AddressablesLoader.Instance.AddressablesLoadSpriteFromAtlasAsync("BackGroundSpriteAtlas", $"Main_Tree_{season.ToString()}", true);
        BG.sprite = await AddressablesLoader.Instance.AddressablesLoadSpriteFromAtlasAsync("BackGroundSpriteAtlas", $"Main_BG_{season.ToString()}", true);
    }

    private void OnDestroy()
    {
        EventBus.UnSubscribe<SeasonChangeEvent>(ToSetBG);
    }


}
