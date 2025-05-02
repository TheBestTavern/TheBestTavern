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
        gangwon = 0,
        hwanghae = 1,
        southChungcheong = 2,
        northChungcheong = 3,
        southGyeongsang = 4,
        northGyeongsang = 5,
        southJeolla = 6,
        northJeolla = 7,
        southHamgyong = 8,
        northHamgyong = 9,
        gyeonggi = 10,
        southPyongan = 11,
        northPyongan = 12,
    }
    public enum BiomeType
    {
        forest = 0,
        sea = 1,
    }
    public enum ForeignType
    {
        japan = 0,
        china = 1,
        none = 2,
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
        dish = 1,
        processed = 2,
    }
}
