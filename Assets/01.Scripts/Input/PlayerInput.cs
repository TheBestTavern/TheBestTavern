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
    private GameObject currentHovered = null;

    // ESC를 눌렀을 때
    public async void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // 게임 일시 정지 및 설정 팝업 불러오기 함수
            await UIManager.Instance.ShowPopUp(PopUpType.Setting);
        }
    }

    // TAB을 눌렀을 때
    public async void OnMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // 메뉴 팝업 불러오기
            await UIManager.Instance.ShowPopUp(PopUpType.Menu);
        }
    }

    // 채집용 마우스 움직임
    public void OnMouseMove(InputAction.CallbackContext context)
    {
        Vector2 screenPos = context.ReadValue<Vector2>();

        // 카메라에서 평면까지 거리
        float z = Mathf.Abs(Camera.main.transform.position.z);

        Vector3 screenPosWithZ = new Vector3(screenPos.x, screenPos.y, z);

        // 마우스 화면 좌표 월드 좌표로 바꾸기
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosWithZ);

        // Raycast로 감지
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        // 마우스에 닿은 게임 오브젝트 넣기
        GameObject hovered = hit.collider != null ? hit.collider.gameObject : null;

        // 마우스가 다른 오브젝트로 이동했을 경우
        if (hovered != currentHovered)
        {
            // 이전 오브젝트 Exit 처리
            if (currentHovered != null)
                currentHovered.GetComponent<GatheringProps>().ExitMouseAnim();

            // 새 오브젝트 Enter 처리
            if (hovered != null)
                hovered.GetComponent<GatheringProps>().OnMouseAnim();

            // 현재 오브젝트 갱신
            currentHovered = hovered;
        }
    }
}
