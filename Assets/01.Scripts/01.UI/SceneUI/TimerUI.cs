using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;

    public void SetDay(string day)
    {
        dayText.text = day;
    }
}
