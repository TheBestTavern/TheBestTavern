using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMapPopUp : BasePopUp
{
    [SerializeField] private GameObject selectForestOcean;

    public override void Awake()
    {
        base.Awake();
        popUpType = PopUpType.SelectMap;
    }
}
