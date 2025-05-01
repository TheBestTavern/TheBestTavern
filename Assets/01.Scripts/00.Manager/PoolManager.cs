using System.Collections.Generic;
using UnityEngine;
public enum PoolType
{
    Image,
    Slot,

}

public interface IPool
{
    public Component Issue();
    public void Regain(IPoolabe poolabe);
    public void Increase();
    public void Decrease();
}

public interface IPoolabe
{
    public void Init(); // 초기화, 출전준비
    public void Restore(); // 원상복구, 집에 있을때 상태
}

[System.Serializable]
public class Pool<T> : MonoBehaviour, IPool where T : MonoBehaviour, IPoolabe
{
    Stack<T> pool;
    [SerializeField] T Pref;
    [SerializeField] bool canDec = true;
    [SerializeField] float decPeriod;
    [SerializeField] float remainPeriod;

    private void Update()
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

    public Component Issue()
    {
        if (pool.TryPop(out T temp))
        {
            temp.Init();
            return temp; // component로 캐스팅을 해줘야할까? 안해줬을때, Manager에서 이걸 반환받았을때, 컴포넌트가 아니라 T로 받게되는 거 아닌가?
        }
        else
        {
            Increase();
            T temp2 = pool.Pop();
            temp2.Init();
            return temp2;
        }
    }

    public void Regain(IPoolabe poolabe)
    {
        poolabe.Restore();
        pool.Push((T)poolabe);
    }

    public void Increase()
    {
        T temp = Instantiate(Pref);
    }

    public void Decrease()
    {
        Destroy(pool.Pop());
    }
}

public class PoolManager : MonoSingleton<PoolManager>
{
    [SerializeField] 
    Dictionary<PoolType, IPool> pools = new();

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(gameObject);
    }


}

