using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePopUp : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    public PopUpType popUpType;

    public virtual void Awake()
    {
        closeButton.onClick.AddListener(OnClickCloseButton);
    }

    public virtual void OnClickCloseButton()
    {
        UIManager.Instance.HidePopUp(popUpType);
    }
}
