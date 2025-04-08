using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopUp : BasePopUp
{
    public override void Awake()
    {
        base.Awake();
        uIType = UIType.Setting;
    }
}
