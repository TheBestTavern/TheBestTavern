using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 쿠킹 미니게임 이펙트 전담
/// </summary>
public class CookingEffectController : MonoBehaviour
{
    [field:SerializeField] public CookingEffectSO Data { get; private set; }

    // 테스트용
    //[SerializeField] private GameObject ingredientPrefab;

    public void PlayBlackSmoke()
    {
        if (Data.BlackSmoke != null)
        {
            var blackSmoke = Instantiate(Data.BlackSmoke);
            blackSmoke.Play();
        }
    }

    public void PlayYellowSmoke()
    {
        if (Data.YellowSmoke != null)
        {
          
            var yellowSmoke = Instantiate(Data.YellowSmoke);
            yellowSmoke.Play();
        }
    }

}
