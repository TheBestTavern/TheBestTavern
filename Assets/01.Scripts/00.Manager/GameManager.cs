using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Action newDayAction;

    public void OnNewDayAction()
    {
        newDayAction?.Invoke();
    }
}
