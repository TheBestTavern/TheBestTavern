using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// fsm과 게임을 연결하는 컨트롤러
/// </summary>
public class CookingMiniGameController : MonoBehaviour
{
    public ICookingMiniGameHandler handler;
    public CookingStateMachine stateMachine;

    public void Init(MiniGameType type)
    {
        switch (type)
        {
            case MiniGameType.Cut:
                handler = new CookingCuttingMiniGame();
                break;
            case MiniGameType.Boil:
                handler = new CookingBoilMiniGame();
                break;
            case MiniGameType.Mill:
                handler = new CookingMillMiniGame();
                break;
            case MiniGameType.Grind:
                handler = new CookingGrindMiniGame();
                break;
            case MiniGameType.Grill:
                handler = new CookingGrillMiniGame();
                break;
        }
    }
    
}
