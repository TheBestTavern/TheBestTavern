using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// UI MANAGER 생기면 수정예정 (임시)
/// </summary>


public enum GradeText
{
        Perfect,
        Good,
        Bad,
        Miss
    }
public class CookingJudgeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI perfectText;
    [SerializeField] private TextMeshProUGUI goodText;
    [SerializeField] private TextMeshProUGUI badText;
    [SerializeField] private TextMeshProUGUI missText;

    Dictionary<GradeText, TextMeshProUGUI> dict = new();

    private void Start()
    {
        dict[GradeText.Perfect] = perfectText;
        dict[GradeText.Good] = goodText;
        dict[GradeText.Bad] = badText;
        dict[GradeText.Miss] = missText;
    }
    public void ShowText(GradeText grade)
    {
        if (dict.TryGetValue(grade, out TextMeshProUGUI text))
        {
            text.gameObject.SetActive(true);
            text.DOFade(1f, 0.5f);
            text.DOFade(0f, 0.2f).OnKill(() => text.gameObject.SetActive(false));
        }
    }

}


