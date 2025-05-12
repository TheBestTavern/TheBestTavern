using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class test_Button_pooling_addressable : MonoBehaviour
{
    public string PrefabName;
    
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(async () =>
        {
            //GameObject go = await Addressables.LoadAssetAsync<GameObject>(PrefabName + ".prefab");
            //PoolManager.Instance.Get<test_PoolableCircle>(go.GetComponent<test_PoolableCircle>(), Vector3.zero);
            await PoolManager.Instance.GetAddressable<test_PoolableCircle>(PrefabName + ".prefab", Vector3.zero);
        });
    }
}
