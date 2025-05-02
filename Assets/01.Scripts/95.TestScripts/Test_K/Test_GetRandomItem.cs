using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Test_GetRandomItem : MonoBehaviour
{
    public int num;
    public Button btn;

    private void Start()
    {
        btn.onClick.AddListener(() => GetRandomData(num));
    }

    private void GetRandomData(int num)
    {
        Debug.Log( DataManager.Instance.DataLoader_CookingSteps.ItemsList[num].name);
    }
}