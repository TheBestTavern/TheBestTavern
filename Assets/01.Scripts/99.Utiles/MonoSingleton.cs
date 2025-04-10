using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    [SerializeField] protected bool isDontDestroyOnLoad = false;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                string name = typeof(T).Name;
                _instance = new GameObject(name).AddComponent<T>();
                Debug.Log($"{name} 싱글톤 오브젝트 생성");
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Debug.Log("중복 생성된 싱글톤 객체 삭제");
            Destroy(this);
        }
        else
        {
            _instance = this.gameObject.GetComponent<T>(); // 씬에 이미 배치된 경우 여기서 할당을 해줌.

            if (isDontDestroyOnLoad)
            {
                DontDestroyOnLoad(this);
            }
        }
    }

    protected virtual void OnDestroy()
    {
        if(_instance != null)
        {
            _instance = null;   
        }
    }
}
