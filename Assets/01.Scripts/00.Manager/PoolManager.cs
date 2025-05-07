using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    Dictionary<string, IPool> pools = new();

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        foreach (var poolPair in pools)
        {
            poolPair.Value.ManualUpdate();
        }
    }

    public T Get<T>(T toGet) where T : MonoBehaviour, IPoolable
    {
        if (!pools.TryGetValue(toGet.ID, out IPool value))
        {
            value = AddPool(toGet); // 미작성
        }

        return value.Issue() as T; // (T): 예외(InvalidCastException) 발생 vs  as T: 타입이 맞지 않으면 null 반환
    }

    //public GameObject Get(GameObject prefab)
    //{
    //    IPoolable ipool = prefab.GetComponent<IPoolable>();
    //    if (ipool == null)
    //    {
    //        Debug.LogError("프리팹에 IPoolable 컴포넌트가 없습니다.");
    //        return null;
    //    }
    //    return GetTyped(ipool);
    //}

    //private GameObject GetTyped<T>(T prefab) where T : MonoBehaviour, IPoolable
    //{
    //    if (!pools.TryGetValue(prefab.ID, out IPool value))
    //    {
    //        value = AddPool(prefab); // 미작성
    //    }

    //    return value.Issue().gameObject; // (T): 예외(InvalidCastException) 발생 vs  as T: 타입이 맞지 않으면 null 반환
    //}

    private IPool AddPool<T>(T toAdd) where T : MonoBehaviour, IPoolable
    {
        string name = toAdd.ID;
        var tsr = new GameObject(name).GetComponent<Transform>();
        tsr.SetParent(gameObject.transform);

        Pool<T> pool = new Pool<T>();
        pool.Init(toAdd, tsr);
        pools.Add(toAdd.ID, pool);

        return pool;
    }
}

