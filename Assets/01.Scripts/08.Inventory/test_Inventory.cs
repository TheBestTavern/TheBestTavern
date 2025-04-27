using UnityEngine;
using UnityEngine.UI;

public class test_Inventory : MonoBehaviour
{
    [SerializeField] Button addBtn_goguma;
    [SerializeField] Button addBtn_butter;
    [SerializeField] Button subtractBtn_goguma;
    [SerializeField] Button subtractBtn_butter;
    [SerializeField] int count;

    private void Start()
    {
        InventoryManager.Instance.CreateInventory(InvenType.Player, 35, 10);
        addBtn_goguma.onClick.AddListener(() => AddItemBtn(101015));
        addBtn_butter.onClick.AddListener(() => AddItemBtn(103007));
        subtractBtn_goguma.onClick.AddListener(() => SubtractItemBtn(101015));
        subtractBtn_butter.onClick.AddListener(() => SubtractItemBtn(103007));
    }

    private void AddItemBtn(int key)
    {
        if(InventoryManager.Instance.Invens[InvenType.Player].아이템획득(Data.GetRawItem(key), count))
        {
            Debug.Log("아이템 증가 가능");
        }
        else
        {
            Debug.Log("아이템 증가 불가능");
        }
    }

    private void SubtractItemBtn(int key)
    {
        if(InventoryManager.Instance.Invens[InvenType.Player].아이템잃음(Data.GetRawItem(key), count))
        {
            Debug.Log("아이템 감소 가능");
        }
        else
        {
            Debug.Log("아이템 감소 불가능");
        }
    }
}