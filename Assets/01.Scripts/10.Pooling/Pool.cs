using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public interface IPool
{
    public void ManualUpdate();
    public Component Issue(Vector3 pos);
    public void Regain(IPoolable poolable);
    public void Increase();
    public void Decrease();
}

public interface IPoolable
{
    public string ID { get; }
    public event Action<IPoolable> OnReturn;
    public bool CanDec { get; }
    public float DecPeriod { get; }
    public void Initialize(Action<IPoolable> a); // 초기화
    public void OnSpawn(Vector3 pos); // 출전
    public void OnDespawn(); // 원상복구, 집에 있을때 상태
    public void TriggerReturn();
}

public class Pool<T> : IPool where T : MonoBehaviour, IPoolable
{
    Stack<T> pool = new();
    T pref;
    bool canDec;
    float decPeriod;
    float remainPeriod = 0;

    bool swapTsr = false;
    Transform despawnTsr;
    Transform spawnTsr;

    public void Init(T pref, Transform DespawnTsr, Transform SpawnTsr)
    {
        this.pref = pref;
        if (SpawnTsr)
        {
            this.spawnTsr = SpawnTsr;
            swapTsr = true;
        }
        this.despawnTsr = DespawnTsr;
        this.canDec = pref.CanDec;
        this.decPeriod = pref.DecPeriod;
        this.remainPeriod = decPeriod;
    }

    public void ManualUpdate()
    {
        if (canDec)
        {
            if (remainPeriod < 0)
            {
                Decrease();
            }
            else
            {
                remainPeriod -= Time.deltaTime;
            }
        }
    }

    public Component Issue(Vector3 pos)
    {
        if (!pool.TryPop(out T temp))
        {
            Increase();
            temp = pool.Pop();
        }
        temp.gameObject.SetActive(true);
        if (swapTsr) temp.transform.SetParent(spawnTsr);
        temp.OnSpawn(pos);

        return temp; // component로 캐스팅을 해줘야할까? 안해줬을때, Manager에서 이걸 반환받았을때, 컴포넌트가 아니라 T로 받게되는 거 아닌가?
    }

    public void Regain(IPoolable poolable)
    {
        var thing = poolable as T;
        thing.OnDespawn();
        if (swapTsr) thing.transform.SetParent(despawnTsr);
        pool.Push(thing);
    }

    public void Increase()
    {
        T temp = UnityEngine.Object.Instantiate(pref, despawnTsr, true);
        Debug.Log("오브젝트 생성");
        temp.Initialize(Regain);
        pool.Push(temp);
    }

    public void Decrease()
    {
        if (pool.TryPop(out T result))
        {
            UnityEngine.Object.Destroy(result.gameObject);
            Debug.Log("오브젝트 파괴");
        }
        remainPeriod = decPeriod;
    }
}

