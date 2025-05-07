using System;
using UnityEngine;

public class test_PoolableCircle : MonoBehaviour, IPoolable
{
    public string ID => "circle";
    public event Action<IPoolable> OnReturn;

    public bool CanDec => true;

    public float DecPeriod => 5;

    bool isActive;
    public void Initialize(Action<IPoolable> a)
    {
        OnReturn = a;
    }

    public void OnSpawn()
    {
        Debug.Log("출격");
        transform.position = new Vector3(0, 0, 0);
        isActive = true;
        flexibleTime = disappearTime;
    }

    public void OnDespawn()
    {
        isActive = false;
        gameObject.SetActive(false);
    }

    float disappearTime = 3;
    float flexibleTime;

    private void Update()
    {
        if (!isActive) return;

        flexibleTime -= Time.deltaTime;
        if (flexibleTime < 0)
        {
            OnReturn?.Invoke(this);
            Debug.Log("원래 자리로 복귀");
            flexibleTime = disappearTime;
        }
    }
}
