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
        all = 0,
        gangwon = 1,
        hwanghae = 2,
        southChungcheong = 3,
        northChungcheong = 4,
        southGyeongsang = 5,
        northGyeongsang = 6,
        southJeolla = 7,
        northJeolla = 8,
        southHamgyong = 9,
        northHamgyong = 10,
        gyeonggi = 11,
        southPyongan = 12,
        northPyongan = 13,
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
}
