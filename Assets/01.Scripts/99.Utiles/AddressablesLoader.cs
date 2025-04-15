using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// Addressables 관리 클래스
/// </summary>
public class AddressablesLoader : MonoSingleton<AddressablesLoader>
{
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
            // 결과 리턴
            return handle.Result;
        }

        // 실패시 null 반환
        return null;
    }
}
