using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;
using Button = UnityEngine.UI.Button;

public enum ResultOfInputAction
{
    Success,
    OutOfValue,
    WrongValueType
}

/// <summary>
/// 확인 팝업 클래스
/// </summary>
public class ConfirmPopUp : BasePopUp
{
    // 확인 버튼
    [SerializeField] private Button okButton;
    [SerializeField] private Button yesButton;

    // 확인 텍스트
    [SerializeField] private TextMeshProUGUI alarmText;

    [SerializeField] private TMP_InputField inputField;

    // 확인 액션
    public Action confirmAction;
    public Func<string, Task<ResultOfInputAction>> inputAction;

    public override void Awake()
    {
        base.Awake();
        okButton.onClick.AddListener(OnClickCloseButton);
        yesButton.onClick.AddListener(OnClickYesButton);
    }

    // 확인 버튼 클릭 함수
    async void OnClickYesButton()
    {
        if (inputField.IsActive())
        {
            ResultOfInputAction resultType = await inputAction?.Invoke(inputField.text);
            ConfirmPopUp popup;
            switch (resultType)
            {
                case ResultOfInputAction.OutOfValue:
                    inputField.text = "";
                    //Debug.Log("effectsound - 실패음");
                    popup = (ConfirmPopUp)await PopUpManager.Instance.ShowPopUp(PopUpType.Confirm);
                    popup.SetConfirm("유효한 수량의 값을 입력해주세요");
                    break;
                case ResultOfInputAction.WrongValueType:
                    inputField.text = "";
                    //Debug.Log("effectsound - 실패음");
                    popup = (ConfirmPopUp)await PopUpManager.Instance.ShowPopUp(PopUpType.Confirm);
                    popup.SetConfirm("잘못된 타입의 값입니다.");
                    break;
                case ResultOfInputAction.Success:
                    inputField.text = "";
                    //Debug.Log("effectsound - 성공음");
                    this.OnClickCloseButton();
                    break;
            }
        }
        else
        {
            confirmAction?.Invoke();
            OnClickCloseButton();
        }
    }

    /// <summary>
    /// 확인 팝업 설정 함수
    /// </summary>
    /// <param name="text">확인 텍스트 넣기 (Ex : 정말 이동하시겠습니까?)</param>
    /// <param name="action">확인 액션 넣기 (Ex : 씬 이동 함수)</param>
    public void SetConfirm(string text) // 알림 팝업
    {
        // 확인 텍스트 설정 
        yesButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        okButton.gameObject.SetActive(true);

        alarmText.text = text;

        inputField.gameObject.SetActive(false);
    }

    public void SetConfirm(string text, Action action) // 예/아니오 팝업
    {
        yesButton.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(true);
        okButton.gameObject.SetActive(false);

        alarmText.text = text;

        inputField.gameObject.SetActive(false);

        confirmAction = action;
    }

    public void SetConfirm<T>(string text, Func<T, bool> action) // 입력 필드가 있는 예/아니오 팝업
    {
        yesButton.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(true);
        okButton.gameObject.SetActive(false);

        alarmText.text = text;

        inputField.gameObject.SetActive(true);

        if (typeof(T) == typeof(string))
        {
            inputField.contentType = TMP_InputField.ContentType.Standard;
        }
        else if (typeof(T) == typeof(int))
        {
            inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        }
        else if (typeof(T) == typeof(float))
        {
            inputField.contentType = TMP_InputField.ContentType.DecimalNumber;
        }

        inputAction = async (input) =>
        {
            try
            {
                T cast = (T)Convert.ChangeType(input, typeof(T));
                bool success = (bool)(action?.Invoke(cast));
                return success ? ResultOfInputAction.Success : ResultOfInputAction.OutOfValue;
            }
            catch (Exception e)
            {
                Debug.Log($"잘못된 입력 값 변환 실패{e.Message}");
                return ResultOfInputAction.WrongValueType;
            }
        };
    }

    // 팝업 닫을 때 필요한 함수
    public override void OnClose()
    {
        base.OnClose();
    }
}
