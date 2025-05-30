using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;
//using static UnityEngine.Rendering.VirtualTexturing.Debugging;

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
    public async Task<T> AddressablesLoadAsync<T>(string address, bool fallback = false)
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
        if (fallback)
        {
            return await AddressablesLoadAsync<T>("Default." + typeof(T).Name); ;
        }
        else
        {
            return default(T);
        }
    }

    public async UniTask PreloadAllFromLavelAsync(string label)
    {
        var locationHandle = Addressables.LoadResourceLocationsAsync(label);
        await locationHandle;

        if (!locationHandle.IsValid() || locationHandle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.Log($"{label} Location Load Fail");
            Addressables.Release(locationHandle);
        }

        var locations = locationHandle.Result;

        foreach (var location in locations)
        {
            Type type = location.ResourceType;
            await AddressablesLoadAsync<System.Object>(location.PrimaryKey);
        }

        Addressables.Release(locationHandle);
    }

    public async Task<Sprite> AddressablesLoadSpriteFromAtlasAsync(string AtalsAdress, string imageName, bool fallback = false)
    {
        var atlas = await AddressablesLoadAsync<SpriteAtlas>(AtalsAdress);
        return atlas.GetSprite(imageName);
    }


    public void Release(string address)
    {
        if (cache.TryGetValue(address, out var handle))
        {
            Addressables.Release(handle);
            cache.Remove(address);
        }
    }

    public async Task<List<GameObject>> AddressablesListLoadFromLabelAsync(string label)
    {
        List<GameObject> results = new List<GameObject>();
        var handle = Addressables.LoadAssetsAsync<GameObject>(label, null);
        results.AddRange(await handle.Task);
        return results;
    }


    // 모든 어드레서블 릴리즈 
    public void ReleaseAllLoadedAssets()
    {
        foreach (var pair in cache)
        {
            Addressables.Release(pair.Value);
        }
        cache.Clear();
    }

    // 게임 종료시 메모리 누수 방지
    protected override void OnDestroy()
    {
        ReleaseAllLoadedAssets();
    }
}
