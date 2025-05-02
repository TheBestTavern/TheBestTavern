using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MiniGame/ResultRule")]
public class CookingMiniGameResultSO : ScriptableObject
{
    [Header("도마 미니게임 기준")]


    [Header("가마솥 - 끓이기 미니게임 기준")]


    [Header("가마솥 - 굽기 미니게임 기준")]
    // 7~8쌍 상 : 완벽한 구이
    // 4~6쌍 중 : 무난한 구이
    // 1~3쌍 하 : 덜 익음
    // 0쌍 : 실패
    public int legendaryMatchCount;
    public int rareMatchCount;
    public int commonMatchCount;
    public int failedMatchCount;


    [Header("맷돌 미니게임 기준")]

    [Header("절구 미니게임 기준")]
    public int failedCount;
}
