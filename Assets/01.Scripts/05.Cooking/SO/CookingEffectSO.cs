using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 요리 미니게임에 사용되는 이펙트 데이터
/// </summary>
[CreateAssetMenu(menuName = "MiniGame/Effect")]
public class CookingEffectSO : ScriptableObject
{
    [field: Header("ParticleSystem")]
    [field: SerializeField] public ParticleSystem YellowSmoke { get; private set; }
    [field: SerializeField] public ParticleSystem BlackSmoke { get; private set; }


    [field: Header("Cooked")]
    [field: SerializeField] public GameObject SweetPotato { get; private set; }

    [field: Header("Camera")]

    [field: Header("Text")]
    [field: SerializeField] public GameObject PerfectText { get; private set; }
    [field: SerializeField] public GameObject GoodText { get; private set; }
    [field: SerializeField] public GameObject BadText { get; private set; }
    [field: SerializeField] public GameObject MissText { get; private set; }


    [field: Header("Animation")]
    [field: SerializeField] private CookingAnimationData animationData;
}
