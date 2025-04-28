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
    [SerializeField] private GameObject sweetPotatoPrefab;

    private void Awake()
    {
        // 테스트용
        sweetPotatoPrefab = Instantiate(sweetPotatoPrefab);
        Color initialColor = new Color32(255, 243, 183, 255);
        var renderer = sweetPotatoPrefab.GetComponentInChildren<MeshRenderer>();

        renderer.sharedMaterial.color = initialColor;

    }
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

    public void CookingGrillEffect(int matchCount)
    {
        //색이 진해지는 연출

        var renderer = sweetPotatoPrefab.GetComponentInChildren<MeshRenderer>();

        
        if (renderer != null)
        {
            Color cookedColor = new Color32(255, 205, 0, 255);
            renderer.sharedMaterial.color = Color.Lerp(renderer.sharedMaterial.color, cookedColor, (float)matchCount / 7);
        }
    }
}
