using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
//using static UnityEditor.PlayerSettings;

public class PoolManager : MonoSingleton<PoolManager>
{
    Dictionary<string, IPool> pools = new();
    Dictionary<string, (GameObject, Component)> cache = new();

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

    /// <summary>
    /// 이미 로드된 프리팹을 풀링.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="toGet"></param> 풀링할 오브젝트 프리팹
    /// <param name="pos"></param> 풀링 위치
    /// <param name="tsr"></param> 아이템이 풀링돼있는 동안 부모로 만들어 줄 대상. 기본값: 매니저에 유지.
    /// <returns></returns>
    public T Get<T>(T toGet, Vector3 pos, Transform spawnTsr = null) where T : MonoBehaviour, IPoolable
    {
        if (!pools.TryGetValue(toGet.ID, out IPool value))
        {
            value = AddPool(toGet, spawnTsr); // 미작성
        }

        return value.Issue(pos, spawnTsr) as T; // (T): 예외(InvalidCastException) 발생 vs  as T: 타입이 맞지 않으면 null 반환
    }

    /// <summary>
    /// 어드레서블로 로드 후 풀링
    /// </summary>
    /// <returns></returns>
    public async Task<T> GetAddressable<T>(string prefabName, Vector3 pos, Transform spawnTsr = null) where T : MonoBehaviour, IPoolable
    {
        T toGet;
        if (cache.TryGetValue(prefabName, out var tuple) && tuple.Item1 != null) // 어드레서블 메모리 해제 상황 예외처리
        {
            toGet = (T)tuple.Item2;
        }
        else
        {
            GameObject go = await AddressablesLoader.Instance.AddressablesLoadAsync<GameObject>(prefabName);

            if (!go.TryGetComponent<T>(out toGet))
            {
                Debug.LogError($"{prefabName}에 {typeof(T).Name} 컴포넌트가 없습니다.");
                return null;
            }

            cache[prefabName] = (go, toGet); // 값이 있더라도 덮어 씌움.
        }

        return Get<T>(toGet, pos, spawnTsr);
    }

    /// <summary>
    /// 리소스 폴더에서 로드 후 풀링
    /// </summary>
    /// <returns></returns>
    public T GetResourcesLoad<T>(string path, Vector3 pos, Transform spawnTsr = null) where T : MonoBehaviour, IPoolable
    {
        GameObject go = Resources.Load<GameObject>(path);
        if (!go.TryGetComponent<T>(out T toGet))
        {
            Debug.LogError($"{path}에 {typeof(T).Name} 컴포넌트가 없습니다.");
            return null;
        }

        return Get<T>(toGet, pos, spawnTsr);
    }

    private IPool AddPool<T>(T toAdd, Transform spawnTsr) where T : MonoBehaviour, IPoolable
    {
        string name = toAdd.ID;
        var despawnTsr = new GameObject(name).GetComponent<Transform>();
        despawnTsr.SetParent(gameObject.transform);

        Pool<T> pool = new Pool<T>();
        pool.Init(toAdd, despawnTsr, spawnTsr);
        pools.Add(toAdd.ID, pool);

        return pool;
    }
}