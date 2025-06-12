using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

/// <summary>
/// 플레이어 입력 관리 클래스
/// </summary>
public class PlayerInput : MonoBehaviour
{
    // ESC를 눌렀을 때
    public async void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if(PopUpManager.Instance.popUpStack.TryPop(out BasePopUp popUp))
            {
                popUp.OnClickCloseButton();
            }
            //if (PopUpManager.Instance.PopUps.TryGetValue(PopUpType.Setting, out BasePopUp basePopUp) && basePopUp.gameObject.activeSelf)
            //{
            //    SettingPopUp settingPopUp = basePopUp as SettingPopUp;
            //    settingPopUp.OnClickCloseButton();
            //}
            else
            {
                // 게임 일시 정지 및 설정 팝업 불러오기 함수
                await PopUpManager.Instance.ShowPopUp(PopUpType.Setting);
            }
        }
    }

    // TAB을 눌렀을 때
    public async void OnMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (PopUpManager.Instance.PopUps.TryGetValue(PopUpType.Menu, out BasePopUp basePopUp) && basePopUp.gameObject.activeSelf)
            {
                MenuPopUp menuPopUp = basePopUp as MenuPopUp;
                menuPopUp.OnClickCloseButton();
            }
            else
            {
                // 메뉴 팝업 불러오기
                await PopUpManager.Instance.ShowPopUp(PopUpType.Menu);
            }
        }
    }
}
