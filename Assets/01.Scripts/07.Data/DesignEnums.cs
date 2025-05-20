using System;

public static class DesignEnums
{
    public enum GradeType
    {
        common = 0,
        rare = 1,
        legendary = 2,
    }
    public enum RegionType
    {
        gyeonggi = 0,
        gangwon = 1,
        chungcheong = 2,
        jeolla = 3,
        gyeongsang = 4,
    }
    public enum BiomeType
    {
        forest = 0,
        sea = 1,
    }
    public enum SeasonType
    {
        spring = 0,
        summer = 1,
        fall = 2,
        winter = 3,
    }
    public enum ChanceType
    {
        veryLow = 0,
        low = 1,
        medium = 2,
        high = 3,
    }
    public enum RouteType
    {
        gather = 0,
        NPC = 1,
        cook = 2,
    }
    public enum CookingToolType
    {
        doma = 0,
        julgu = 1,
        matdol = 2,
        gamasot = 3,
        sotdduggung = 4,
        mixingbowl = 5,
        dish = 6,
    }
    public enum ItemType
    {
        ingredient = 0,
        processed = 1,
        special = 2,
        mix = 3,
        dish = 4,
    }
}
