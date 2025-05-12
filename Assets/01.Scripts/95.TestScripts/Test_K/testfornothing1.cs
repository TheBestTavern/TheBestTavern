using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class testfornotdashing : MonoBehaviour
{
    public Button btn;
    private void Start()
    {
        btn.onClick.AddListener(() => DayAndNightManager.Instance.pass1hour());
    }
}
