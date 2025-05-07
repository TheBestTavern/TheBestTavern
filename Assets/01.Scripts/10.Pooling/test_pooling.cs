using UnityEngine;
using UnityEngine.UI;

public class test_pooling : MonoBehaviour
{
    public test_PoolableCircle CirclePref;
    public 
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(() => PoolManager.Instance.Get<test_PoolableCircle>(CirclePref));
    }
}
