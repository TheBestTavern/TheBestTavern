using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoSingleton<DataManager>
{
    [field: SerializeField] public Data_IngredientsLoader DataLoader {  get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    public void Init()
    {
        DataLoader = new Data_IngredientsLoader();
    }
}
