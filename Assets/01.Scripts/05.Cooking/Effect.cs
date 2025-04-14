using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Effect(UI) 전용
/// </summary>
public class Effect : MonoBehaviour
{
    [SerializeField] public CookingAnimationData animationData {  get; private set; }
    public Animator animator { get; private set; }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
       // animationData.Initialize(); 
    }

    /// <summary>
    /// 판정될 때 재생되는 애니메이션 (perfect/bad/good/miss)
    /// </summary>
    public void JudgeEffect(int animationHash)
    {
        animator.SetTrigger(animationHash);
    }
}
