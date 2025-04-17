using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 날짜 UI
/// </summary>
public class TimerUI : MonoBehaviour
{
    // 날짜 텍스트 
    [SerializeField] private TextMeshProUGUI dayText;

    // 날짜 설정 함수 
    public void SetDay(string day)
    {
        dayText.text = day;
    }
}
