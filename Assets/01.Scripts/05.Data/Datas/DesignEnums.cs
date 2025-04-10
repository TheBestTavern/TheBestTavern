using System;

public static class DesignEnums
{
    public enum Grade
    {
        common = 0,
        rare = 1,
        legendary = 2,
    }
    public enum Region
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
    public enum Biome
    {
        forest = 0,
        sea = 1,
    }
    public enum Foreign
    {
        japan = 0,
        china = 1,
        none = 2,
    }
    public enum Season
    {
        spring = 0,
        summer = 1,
        fall = 2,
        winter = 3,
    }
    public enum Chance
    {
        veryLow = 0,
        low = 1,
        medium = 2,
        high = 3,
    }
    public enum Route
    {
        gather = 0,
        NPC = 1,
        cook = 2,
    }
    public enum CookingTool
    {
        doma = 0,
        julgu = 1,
        matdol = 2,
        gamasot = 3,
        sotdduggung = 4,
        mixingbowl = 5,
        dish = 6,
    }
}
