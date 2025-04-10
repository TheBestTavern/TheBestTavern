using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public int num;
    public Button btn;

    private void Start()
    {
        btn.onClick.AddListener(() => GetRandomData(num));
    }

    private void GetRandomData(int num)
    {
        Debug.Log( DataManager.Instace.DataLoader_CookingSteps.ItemsList[num].name);
    }
}