using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum InfoType
{
    Boil,
    Grill,
    Mill,
    Cut,
    Mix,
    Grind,
    Plate
}
public class CookingInfoPopUp : BasePopUp
{
    // 버튼
    [SerializeField] private Button boilBtn;
    [SerializeField] private Button grillBtn;
    [SerializeField] private Button millBtn;
    [SerializeField] private Button cutBtn;
    [SerializeField] private Button mixBtn;
    [SerializeField] private Button grindBtn;
    [SerializeField] private Button plateBtn;

    // 타이틀/조작법/설명 텍스트
    [Header("boil")]
    [SerializeField] private GameObject boilTitle;
    [SerializeField] private GameObject boilControl;
    [SerializeField] private GameObject boilDescription;
    [Header("grill")]
    [SerializeField] private GameObject grillTitle;
    [SerializeField] private GameObject grillControl;
    [SerializeField] private GameObject grillDescription;
    [Header("mill")]
    [SerializeField] private GameObject millTitle;
    [SerializeField] private GameObject millControl;
    [SerializeField] private GameObject millDescription;
    [Header("cut")]
    [SerializeField] private GameObject cutTitle;
    [SerializeField] private GameObject cutControl;
    [SerializeField] private GameObject cutDescription;
    [Header("mix")]
    [SerializeField] private GameObject mixTitle;
    [SerializeField] private GameObject mixControl;
    [SerializeField] private GameObject mixDescription;
    [Header("grind")]
    [SerializeField] private GameObject grindTitle;
    [SerializeField] private GameObject grindControl;
    [SerializeField] private GameObject grindDescription;
    [Header("plate")]
    [SerializeField] private GameObject plateTitle;
    [SerializeField] private GameObject plateControl;
    [SerializeField] private GameObject plateDescription;

    Dictionary<InfoType, GameObject[]> infoDic;

    private Button selectedBtn;

    public override void Awake()
    {
        base.Awake();

        popUpType = PopUpType.CookingInfo;

        infoDic = new Dictionary<InfoType, GameObject[]>()
        {
            { InfoType.Boil, new GameObject[] { boilTitle, boilControl, boilDescription } },
            { InfoType.Grill, new GameObject[] { grillTitle, grillControl, grillDescription } },
            { InfoType.Mill, new GameObject[] { millTitle, millControl, millDescription } },
            { InfoType.Cut, new GameObject[] { cutTitle, cutControl, cutDescription } },
            { InfoType.Mix, new GameObject[] { mixTitle, mixControl, mixDescription } },
            { InfoType.Grind, new GameObject[] { grindTitle, grindControl, grindDescription } },
            { InfoType.Plate, new GameObject[] { plateTitle, plateControl, plateDescription } },
        };

        boilBtn.onClick.AddListener(() => { ShowInfo(InfoType.Boil); AnimateButton(boilBtn); });
        grillBtn.onClick.AddListener(() => { ShowInfo(InfoType.Grill); AnimateButton(grillBtn); });
        millBtn.onClick.AddListener(() => { ShowInfo(InfoType.Mill); AnimateButton(millBtn); });
        cutBtn.onClick.AddListener(() => { ShowInfo(InfoType.Cut); AnimateButton(cutBtn); });
        mixBtn.onClick.AddListener(() => { ShowInfo(InfoType.Mix); AnimateButton(mixBtn); });
        grindBtn.onClick.AddListener(() => { ShowInfo(InfoType.Grind); AnimateButton(grindBtn); });
        plateBtn.onClick.AddListener(() => { ShowInfo(InfoType.Plate); AnimateButton(plateBtn); });

        ShowInfo(InfoType.Boil);
        AnimateButton(boilBtn);
    }

    void ShowInfo(InfoType type)
    {
        foreach (var info in infoDic)
        {
            bool isTarget = info.Key == type;
            foreach (var obj in info.Value)
            {
                obj.SetActive(isTarget);
            }
        } 
    }

    void AnimateButton(Button btn)
    {
        if (selectedBtn != null && selectedBtn != btn)
        {
            selectedBtn.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutQuad);
        }

        selectedBtn = btn;
        btn.transform.DOScale(1.2f, 1f).SetEase(Ease.OutBack);
    }
}
