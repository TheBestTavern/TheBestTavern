using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 요리 미니게임 관련 데이터 (SO)
/// </summary>
[CreateAssetMenu(fileName = "Cooking", menuName = "MiniGame")]
public class CookingMiniGameSO : ScriptableObject
{
    [field: SerializeField] public CookingCutData cutData { get; private set; }
    [field: SerializeField] public CookingRoastData roastData { get; private set; }

    [field: SerializeField] public CookingBoilData boilData { get; private set; }

    [field: SerializeField] public CookingMillData millData { get; private set; }

    [field: SerializeField] public CookingGrindData grindData { get; private set; }

}
/// <summary>
/// 도마 미니게임 관련 데이터
/// </summary>
[Serializable]
public class CookingCutData 
{
    // 노트속도
    // [field: SerializeField][field: Range(0f, 2f)] public float NoteSpeed { get; private set; }

    // 미니게임 시간
    //[field: SerializeField][field: Range(0f, 15f)] public float GameTime { get; private set; }

}

/// <summary>
/// 가마솥 굽기 미니게임 관련 데이터
/// </summary>
[Serializable]
public class CookingRoastData 
{
    // 미니게임 시간
    //[field: SerializeField][field: Range(0f, 15f)] public float GameTime { get; private set; }
}

/// <summary>
/// 가마솥 끓이기 미니게임 관련 데이터
/// </summary>
[Serializable]
public class CookingBoilData { }

/// <summary>
/// 맷돌 미니게임 관련 데이터
/// </summary>
[Serializable]
public class CookingMillData { }

/// <summary>
/// 절구 미니게임 관련 데이터
/// </summary>
[Serializable]
public class CookingGrindData { }

