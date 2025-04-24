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


    [field: Header("BoilData")]
    //[field: SerializeField] public CookingBoilData boilData { get; private set; }
    [field: SerializeField] public float BoilTimer { get; private set; } = 15f;


    [field: Header("MillData")]
    //[field: SerializeField] public CookingMillData millData { get; private set; }
    [field: SerializeField] public float MillTimer { get; private set; } = 15f;


    [field: Header("GrindData")]
    //[field: SerializeField] public CookingGrindData grindData { get; private set; }
    [field: SerializeField] public float GrindTimer { get; private set; } = 15f;
   
    // 노트속도
    [field: SerializeField][field: Range(0f, 2f)] public float NoteSpeed { get; private set; }

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

