using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PopUpType
{
    Menu,
    Option,
}


public class UIManager : MonoSingleton<UIManager>
{
    public MainUI mainUI;
    public OptionPopUp optionPopUp;
    public MenuPopUp menuPopUp;

    [SerializeField] private Transform canvas;

    [SerializeField] private GameObject menuPopUpPrefab;
    [SerializeField] private GameObject optionPopUpPrefab;

    public void ShowPopUp(PopUpType type)
    {
        switch (type)
        {
            case PopUpType.Menu:
                if(menuPopUp == null)
                {
                    menuPopUp = Instantiate(menuPopUpPrefab, canvas).GetComponent<MenuPopUp>();
                }
                menuPopUp.gameObject.SetActive(true);
                break;
            case PopUpType.Option:
                if (optionPopUp == null)
                {
                    optionPopUp = Instantiate(optionPopUpPrefab, canvas).GetComponent<OptionPopUp>();
                }
                optionPopUpPrefab.gameObject.SetActive(true);
                Time.timeScale = 0;
                break;
        }
    }

    public void HidePopUp(PopUpType type)
    {
        switch (type)
        {
            case PopUpType.Menu:
                menuPopUp.gameObject.SetActive(false);
                break;
            case PopUpType.Option:
                optionPopUpPrefab.gameObject.SetActive(false);
                Time.timeScale = 1;
                break;
        }
    }
}
