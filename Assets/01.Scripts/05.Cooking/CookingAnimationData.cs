using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CookingAnimationData 
{
    [SerializeField] private string perfectParameterName = "Perfect";
    [SerializeField] private string goodParameterName = "Good";
    [SerializeField] private string badParameterName = "Bad";
    [SerializeField] private string missParameterName = "Miss";

    public int PerfectParameterHash { get; private set; }
    public int GoodParameterHash { get; private set; }

    public int BadParameterHash { get; private set; }

    public int MissParameterHash { get; private set; }

    public void Initialize()
    {
        PerfectParameterHash = Animator.StringToHash(perfectParameterName);
        GoodParameterHash = Animator.StringToHash(goodParameterName);
        BadParameterHash = Animator.StringToHash(badParameterName);
        MissParameterHash = Animator.StringToHash(missParameterName);
    }
}
