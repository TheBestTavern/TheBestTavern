using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mono<T> : MonoBehaviour
{
    protected static bool _isInitialized = false;

    /// <summary>
    ///if (_isInitialized) return; base.Init(); 을 반드시 최상단에서 실행해야합니다.
    /// </summary>
    public virtual void Init()
    {
        _isInitialized = true;
    }
}

public class MonoSingleton<T> : Mono<T> where T : Mono<T>
{
    //private static T _instance;
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

            if (!_isInitialized)
            {
                _instance.Init();
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
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
        if (_instance != null)
        {
            _instance = null;
        }
    }
}
