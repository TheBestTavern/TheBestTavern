using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// Addressables 관리 클래스
/// </summary>
public class AddressablesLoader : MonoSingleton<AddressablesLoader>
{
    Dictionary<string, AsyncOperationHandle> cache = new();

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Addressables로 설정된 프리펩 불러오기 함수
    /// </summary>
    /// <param name="address">불러올 Addressables 프리펩 경로</param>
    /// <returns></returns>
    public async Task<T> AddressablesLoadAsync<T>(string address)
    {
        if (cache.TryGetValue(address, out var cacheHandle))
        {
            if (cacheHandle.IsValid())
            {
                return (T)cacheHandle.Result;
            }

            cache.Remove(address);
        }

        // Addressables 프리펩 불러오기 
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);

        // 다 불러올 때까지 기다리기
        await handle.Task;

        // 다 불러왔다면 
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            cache[address] = handle;
            // 결과 리턴
            return handle.Result;
        }

        Debug.LogError($"에셋 로드 실패: {address}");
        Addressables.Release(handle);
        // 실패시 null 반환
        return default(T);
    }

    public void Release(string address)
    {
        if (cache.TryGetValue(address, out var handle))
        {
            Addressables.Release(handle);
            cache.Remove(address);
        }
    }
}
