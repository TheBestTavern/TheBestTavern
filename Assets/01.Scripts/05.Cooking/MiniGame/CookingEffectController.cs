using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 쿠킹 미니게임 이펙트 전담
/// </summary>
public class CookingEffectController : MonoBehaviour
{
    [field:SerializeField] public CookingEffectSO data { get; private set; }


    public void PlayBlackSmoke()
    {
        if (data.BlackSmoke != null)
        {
            var blackSmoke = Instantiate(data.BlackSmoke);
            blackSmoke.Play();
        }
    }

    public void PlayYellowSmoke()
    {
        if (data.YellowSmoke != null)
        {
          
            var yellowSmoke = Instantiate(data.YellowSmoke);
            yellowSmoke.Play();
        }
    }

    public void CookingGrillEffect(int matchCount)
    {
        //색이 진해지는 연출

        var sweetPotato = Instantiate(data.SweetPotato);
        var renderer = sweetPotato.GetComponent<MeshRenderer>();

        Color cookedColor = new Color32(255, 205, 0, 255);

        if (renderer != null)
        { renderer.material.color = Color.Lerp(renderer.material.color, cookedColor, matchCount / 7); }
    }
}
