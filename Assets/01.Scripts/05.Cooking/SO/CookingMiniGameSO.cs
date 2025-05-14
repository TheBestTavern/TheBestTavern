using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 요리 미니게임 관련 데이터
/// (타이밍, 바 속도, 판정 범위 등의 게임 규칙)
/// </summary>
[CreateAssetMenu(fileName = "Cooking", menuName = "MiniGame")]
public class CookingMiniGameSO : ScriptableObject
{
    [field:Header("CutData")]
    //[field: SerializeField] public CookingCutData cutData { get; private set; }
    [field: SerializeField] public float CutTimer { get; private set; } = 15f;


    [field: Header("GrillData")]
    //[field: SerializeField] public CookingGrillData grillData { get; private set; }
    [field: SerializeField] public float GrillTimer { get; private set; } = 15f;


    [field: Header("BoilData")] // 가마솥 - 끓이기
    //[field: SerializeField] public CookingBoilData boilData { get; private set; }
    [field: SerializeField] public float BoilTimer { get; private set; } = 15f;
    [field: SerializeField] public float SpawnInterval { get; private set; } = 4f;
    [field: SerializeField] public int PerfectBoilCount { get; private set; } = 4;
    [field: SerializeField] public int GoodBoilCount { get; private set; } = 3;

    [field: SerializeField] public int BadBoilCount { get; private set; } = 2;

    [field: SerializeField] public int MissBoilCount { get; private set; } = 3;




    [field: Header("MillData")] // 맷돌
    //[field: SerializeField] public CookingMillData millData { get; private set; }
    [field: SerializeField] public float MillTimer { get; private set; } = 15f;
    [field: SerializeField] public float PerfectTime { get; private set; } = 12f;
    [field: SerializeField] public float GoodTime { get; private set; } = 8f;
    [field: SerializeField] public float BadTime { get; private set; } = 5f;
    [field: SerializeField] public float FailTime { get; private set; } = 0f;


    [field: Header("GrindData")] // 절구
    //[field: SerializeField] public CookingGrindData grindData { get; private set; }
    [field: SerializeField] public float GrindTimer { get; private set; } = 15f;
    
    // 노트판정 횟수 기준
    [field: SerializeField] public int PerfectCount { get; private set; } = 4;
    [field: SerializeField] public int GoodCount { get; private set; } = 4;
    [field: SerializeField] public int BadCount { get; private set; } = 4;
    [field: SerializeField] public int MissCount { get; private set; } = 4;

    // 노트판정 오차 기준
    [field: SerializeField] public float PerfectDiff { get; private set; } = 0.15f;
    [field: SerializeField] public float GoodDiff { get; private set; } = 0.3f;
    [field: SerializeField] public float BadDiff { get; private set; } = 0.5f;
    [field: SerializeField] public float MissDiff { get; private set; } = 0.6f;



    // 노트속도
    [field: SerializeField][field: Range(0f, 2f)] public float NoteSpeed { get; private set; }
    [field: SerializeField] public float NoteTravelTime = 1.5f; // 노트가 도착까지 걸리는 시간 
    [field: SerializeField] public float NoteRespwanTime = 2f; // 2초마다 노트 생성

    [field: Header("MixingBowlData")]
    [field: SerializeField] public float MixingBowlTimer { get; private set; } = 15f;
    [field: SerializeField] public float MixingTime { get; private set; } = 5f;


}
/// <summary>
/// 도마 미니게임 관련 데이터
/// </summary>
[Serializable]
public class CookingCutData 
{
    
}

/// <summary>
/// 가마솥 굽기 미니게임 관련 데이터
/// </summary>
[Serializable]
public class CookingGrillData 
{
    
}

/// <summary>
/// 가마솥 끓이기 미니게임 관련 데이터
/// </summary>
[Serializable]
public class CookingBoilData 
{

}

/// <summary>
/// 맷돌 미니게임 관련 데이터
/// </summary>
[Serializable]
public class CookingMillData 
{

}

/// <summary>
/// 절구 미니게임 관련 데이터
/// </summary>
[Serializable]
public class CookingGrindData 
{

}

