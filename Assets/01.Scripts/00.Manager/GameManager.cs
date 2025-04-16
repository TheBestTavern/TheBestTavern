using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Action onNewDayAction;

    public void TriggerNewDayAction()
    {
        onNewDayAction?.Invoke();
    }
}
