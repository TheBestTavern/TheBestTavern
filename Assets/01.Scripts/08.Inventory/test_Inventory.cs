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
        addBtn_goguma.onClick.AddListener(() => AddItemBtn(101015));
        addBtn_butter.onClick.AddListener(() => AddItemBtn(103007));
        addBtn_manuel.onClick.AddListener(() => AddItemBtn(101001));
        addBtn_gochu.onClick.AddListener(() => AddItemBtn(101002));
        addBtn.onClick.AddListener(() => AddItemBtn(key));

        subtractBtn_goguma.onClick.AddListener(() => SubtractItemBtn(101015));
        subtractBtn_butter.onClick.AddListener(() => SubtractItemBtn(103007));
        subtractBtn_manuel.onClick.AddListener(() => SubtractItemBtn(101001));
        subtractBtn_gochu.onClick.AddListener(() => SubtractItemBtn(101002));
        subtractBtn.onClick.AddListener(() => SubtractItemBtn(key));
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