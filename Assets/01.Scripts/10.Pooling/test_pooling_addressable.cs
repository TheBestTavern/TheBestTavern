using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class test_pooling_addressable : MonoBehaviour
{
    public string PrefabName;
    public
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(async () =>
        {
            GameObject go = await Addressables.LoadAssetAsync<GameObject>(PrefabName + ".prefab");
            PoolManager.Instance.Get<test_PoolableCircle>(go.GetComponent<test_PoolableCircle>());
        });
    }
}
