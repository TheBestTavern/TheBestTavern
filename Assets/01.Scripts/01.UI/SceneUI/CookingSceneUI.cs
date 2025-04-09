using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingSceneUI : MonoBehaviour
{
    [SerializeField] private Button mainSceneButton;

    private void Awake()
    {
        mainSceneButton.onClick.AddListener(OnClickMainSceneButton);
    }

    void OnClickMainSceneButton()
    {
        _ = SceneLoader.Instance.LoadSceneAsync("MainSceneDev");
    }
}
