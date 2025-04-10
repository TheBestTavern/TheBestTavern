using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TestCalendar : MonoBehaviour
{
    public Button PrintButton;
    public Button PassDaysButton;
    public TextMeshProUGUI dateView;

    public int days = 1;

    public DateTime Sundate;
    public LunarDateTime lunarDate;


    private void Start()
    {
        PrintButton.onClick.AddListener(PrintDate);
        PassDaysButton.onClick.AddListener(() => AddDays(days));

        Sundate = new(2020, 5, 30);
        lunarDate = Sundate.ToLunarConvertor();
        //lunarDate = new LunarDateTime(2020, 4, 25, true);
        CheckYear(2020);

        DateTime TestDate = new DateTime(2020, 10, 25);
        Debug.Log(TestDate.ToString());
        LunarDateTime TestDate2 = TestDate.ToLunarConvertor();
        Debug.Log(TestDate2.ToString());
        DateTime TestDate3 = Extensions.lunarCalendar.ToDateTime(TestDate2.year, TestDate2.month, TestDate2.day,0,0,0,0,1);
        Debug.Log(TestDate3.ToString());
    }

    // 매월 1일을 음력으로 변환해보기 검사
    private void CheckYear(int year)
    {
        for (int i = 1; i < 13; i++)
        {
            DateTime solarDateTest = new(year, i, 1);
            Debug.Log("양력" + Sundate.ToString());

            LunarDateTime lunarDateTest = solarDateTest.ToLunarConvertor();
            Debug.Log("음력" + lunarDate.ToString());
        }
    }

    private void Update()
    {
        dateView.text = lunarDate.ToString();
    }

    public void AddDays(int i)
    {
        lunarDate = lunarDate.AddDays(i);
        Debug.Log(lunarDate.ToString());
    }

    public void PrintDate()
    {
        Debug.Log(lunarDate.ToString());
    }

}