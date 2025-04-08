using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum UIType
{
    Setting,
    Menu,
}


public class UIManager : MonoSingleton<UIManager>
{
    public MainSceneUI mainUI;
    public SettingPopUp settingPopUp;
    public MenuPopUp menuPopUp;

    public void ShowPopUp(UIType type)
    {
        switch (type)
        {
            case UIType.Setting:
                if (settingPopUp == null)
                {
                    GameObject go = Instantiate(LoadPopUpResource("SettingPopUpPrefab"));
                    settingPopUp = go.GetComponentInChildren<SettingPopUp>();
                }
                settingPopUp.gameObject.SetActive(true);
                Time.timeScale = 0;
                break;
            case UIType.Menu:
                if (menuPopUp == null)
                {
                    GameObject go = Instantiate(LoadPopUpResource("MenuPopUpPrefab"));
                    menuPopUp = go.GetComponentInChildren<MenuPopUp>();
                }
                menuPopUp.gameObject.SetActive(true);
                break;
        }
    }

    public void HidePopUp(UIType type)
    {
        switch (type)
        {
            case UIType.Setting:
                settingPopUp.gameObject.SetActive(false);
                Time.timeScale = 1;
                break;
            case UIType.Menu:
                menuPopUp.gameObject.SetActive(false);
                break;
        }
    }

    private GameObject LoadPopUpResource(string resourceName)
    {
        GameObject resource = Resources.Load<GameObject>($"UI/PopUp/{resourceName}");
        if (resource == null)
            Debug.LogError($"UI Resource '{resourceName}' not found in path ");
        return resource;
    }
}
