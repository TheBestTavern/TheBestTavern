using UnityEngine;
using System.IO;

public class AddressablesResetHelper : MonoBehaviour
{
    [ContextMenu("완전 캐시 삭제")]
    public void ClearAllAddressablesCache()
    {
        // 1. Unity Caching API 캐시 삭제
        if (Caching.ClearCache())
        {
            Debug.Log("Caching API 캐시 삭제 성공");
        }
        else
        {
            Debug.LogWarning("Caching API 캐시 삭제 실패 또는 캐시 없음");
        }

        // 2. PersistentDataPath 내부 폴더 삭제
        string path = Path.Combine(Application.persistentDataPath, "com.unity.addressables");
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
            Debug.Log("Persistent 캐시 폴더 삭제 완료");
        }
        else
        {
            Debug.Log("Persistent 경로에 addressables 폴더 없음");
        }
    }
}
