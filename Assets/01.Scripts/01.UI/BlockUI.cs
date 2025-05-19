using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class BlockUI : MonoBehaviour 
{
    [SerializeField] Canvas canvas;

    private void Start()
    {

        gameObject.SetActive(false);
    }
}