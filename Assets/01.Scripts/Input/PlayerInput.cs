using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInput : MonoBehaviour
{
    // ESC
    public async void OnPause(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            await UIManager.Instance.ShowPopUp(PopUpType.Setting);
        }
    }

    // TAB
    public async void OnMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            await UIManager.Instance.ShowPopUp(PopUpType.Menu);
        }
    }
}
