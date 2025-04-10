using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_K_Quest : MonoBehaviour
{
    public Button btn;
    public GameObject MailBoxUI;
    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(() => MailBoxUI.SetActive(true));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
