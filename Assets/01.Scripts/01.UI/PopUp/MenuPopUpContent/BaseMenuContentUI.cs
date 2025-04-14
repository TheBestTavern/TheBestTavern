using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMenuContentUI : MonoBehaviour
{
    public Transform contentParent;
    public GameObject contentPrefab;

    public virtual void OnEnable()
    {
        CreateContent();
    }

    public virtual void CreateContent()
    {

    }
}
