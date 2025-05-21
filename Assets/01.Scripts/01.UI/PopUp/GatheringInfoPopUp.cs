using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GatheringInfoPopUp : BasePopUp
{
    [SerializeField] private Button animalInfoButton;
    [SerializeField] private GameObject animalInfoPanel;
    [SerializeField] private Button animalInfoCloseButton;

    public override void Awake()
    {
        base.Awake();
        animalInfoButton.onClick.AddListener(OnClickAnimalInfo);
        animalInfoCloseButton.onClick.AddListener(OnClickAnimalInfoClose);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClickAnimalInfo()
    {
        animalInfoPanel.SetActive(true);
        animalInfoButton.gameObject.SetActive(false);
        animalInfoCloseButton.gameObject.SetActive(true);
    }

    private void OnClickAnimalInfoClose()
    {
        animalInfoPanel.SetActive(false);
        animalInfoCloseButton.gameObject.SetActive(false);
        animalInfoButton.gameObject.SetActive(true);
    }
}
