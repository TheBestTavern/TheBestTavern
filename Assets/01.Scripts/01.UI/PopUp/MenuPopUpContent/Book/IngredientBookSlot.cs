using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientBookSlot : BaseBookSlot<Data_Book_Ingredient>
{

    [SerializeField] TextMeshProUGUI region;
    [SerializeField] TextMeshProUGUI biome;
    [SerializeField] TextMeshProUGUI season;

    public async override void SetSlot(Data_Book_Ingredient thing)
    {
        foodCatergoryID = thing.key;

        icon.sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>($"Assets/16.Image/FoodImage/{thing.englishName}.png", true);
        foodName.text = thing.name;
        desc.text = thing.description;

        // 이 부분 문자열 switch 로직은 엑셀에서 처리하도록 하자.
        List<string> regionString = new();
        for (int i = 0; i < thing.region.Count; i++)
        {
            switch (thing.region[i])
            {
                case DesignEnums.RegionType.gyeonggi:
                    regionString.Add("경기도");
                    break;
                case DesignEnums.RegionType.gangwon:
                    regionString.Add("강원도");
                    break;
                case DesignEnums.RegionType.Chungcheong:
                    regionString.Add("충청도");
                    break;
                case DesignEnums.RegionType.Jeolla:
                    regionString.Add("전라도");
                    break;
                case DesignEnums.RegionType.Gyeongsang:
                    regionString.Add("경상도");
                    break;
            }
        }
        string regionS = "";
        foreach(string region in regionString)
        {
            regionS += region+ ", ";
        }
        regionS = regionS.Substring(0, regionS.Length - 2);
        region.text = regionS;

        string biomeS = "";
        switch (thing.biome)
        {
            case DesignEnums.BiomeType.forest:
                biomeS = "숲";
                break;
            case DesignEnums.BiomeType.sea:
                biomeS = "바다";
                break;
        }
        biome.text = biomeS;

        List<string> seasonString = new();
        for (int i = 0; i < thing.season.Count; i++)
        {
            switch (thing.season[i])
            {
                case DesignEnums.SeasonType.spring:
                    seasonString.Add("봄");
                    break;
                case DesignEnums.SeasonType.summer:
                    seasonString.Add("여름");
                    break;
                case DesignEnums.SeasonType.fall:
                    seasonString.Add("가을");
                    break;
                case DesignEnums.SeasonType.winter:
                    seasonString.Add("겨울");
                    break;
            }
        }
        string seasonS = "";
        foreach (string season in seasonString)
        {
            seasonS += season + ", ";
        }
        season.text = seasonS.Substring(0, seasonS.Length - 2);
    }
}
