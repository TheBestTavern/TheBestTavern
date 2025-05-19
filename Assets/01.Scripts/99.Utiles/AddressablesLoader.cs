using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

/// <summary>
/// Addressables 관리 클래스
/// </summary>
public class AddressablesLoader : MonoSingleton<AddressablesLoader>
{
    List<AsyncOperationHandle> handles = new();
    /// <summary>
    /// Addressables로 설정된 프리펩 불러오기 함수
    /// </summary>
    /// <param name="address">불러올 Addressables 프리펩 경로</param>
    /// <returns></returns>
    public async Task<GameObject> AddressablesLoadAsync(string address)
    {
        // Addressables 프리펩 불러오기 
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(address);

        // 다 불러올 때까지 기다리기
        await handle.Task;

        // 다 불러왔다면 
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            // Release용 리스트에 추가
            handles.Add(handle);
            // 결과 리턴
            return handle.Result;
        }
        // 실패시 null 반환
        return null;
    }

    public async Task<List<GameObject>> AddressablesListLoadFromLabelAsync(string label)
    {
        List<GameObject> results = new List<GameObject>();
        var handle = Addressables.LoadAssetsAsync<GameObject>(label, null);
        results.AddRange(await handle.Task);
        return results;
    }

    protected override void OnDestroy()
    {
        ReleaseAllLoadedAssets();
        base.OnDestroy();
    }

    // 모든 어드레서블 릴리즈 
    public void ReleaseAllLoadedAssets()
    {
        foreach (var handle in handles)
        {
            Addressables.Release(handle);
        }
        handles.Clear();
    }
}
