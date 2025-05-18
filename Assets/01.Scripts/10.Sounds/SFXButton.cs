using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXButton : MonoBehaviour
{
    [SerializeField] private string sfxName = "Button1";

    private void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(PlaySFX);
    }

    private void PlaySFX()
    {
        SoundManager.Instance.PlaySFX(sfxName);
    }
}
