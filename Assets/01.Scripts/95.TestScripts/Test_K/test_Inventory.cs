using UnityEngine;
using UnityEngine.UI;

public class test_Inventory : MonoBehaviour
{
    [SerializeField] Button addBtn_goguma;
    [SerializeField] Button addBtn_butter;
    [SerializeField] Button addBtn_manuel;
    [SerializeField] Button addBtn_gochu;
    [SerializeField] Button addBtn;
    [SerializeField] Button subtractBtn_goguma;
    [SerializeField] Button subtractBtn_butter;
    [SerializeField] Button subtractBtn_manuel;
    [SerializeField] Button subtractBtn_gochu;
    [SerializeField] Button subtractBtn;
    [SerializeField] int key;
    [SerializeField] int count;

    private void Start()
    {
        addBtn_goguma.onClick.AddListener(() => AddItemBtn(101011));
        addBtn_butter.onClick.AddListener(() => AddItemBtn(104061));
        addBtn_manuel.onClick.AddListener(() => AddItemBtn(104062));
        addBtn_gochu.onClick.AddListener(() => AddItemBtn(104063));
        addBtn.onClick.AddListener(() => AddItemBtn(key));

        subtractBtn_goguma.onClick.AddListener(() => SubtractItemBtn(101015));
        subtractBtn_butter.onClick.AddListener(() => SubtractItemBtn(104061));
        subtractBtn_manuel.onClick.AddListener(() => SubtractItemBtn(104062 ));
        subtractBtn_gochu.onClick.AddListener(() => SubtractItemBtn(104063));
        subtractBtn.onClick.AddListener(() => SubtractItemBtn(key));
    }

    private void AddItemBtn(int key)
    {
        if(InventoryManager.Instance.Invens[InvenType.Player].AcquireItem(Data.GetRawItem(key), count))
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
        if(InventoryManager.Instance.Invens[InvenType.Player].LooseItem(Data.GetRawItem(key), count))
        {
            Debug.Log("아이템 감소 가능");
        }
        else
        {
            Debug.Log("아이템 감소 불가능");
        }
    }
}