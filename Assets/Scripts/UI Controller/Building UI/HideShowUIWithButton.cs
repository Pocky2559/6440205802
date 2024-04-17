using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HideShowUIWithButton : MonoBehaviour
{
    public GameObject targetUI;
    private Button buttonBuilding;
    public GameObject SelectedUI;

    private void Awake()
    {
        buttonBuilding = GetComponent<Button>();
    }

    private void Update()
    {   
        // Check if the TargetUI because if we hit the button when the target UI is showing it will hide and if it hide, it will show.
        if(targetUI.activeInHierarchy)
        {
            buttonBuilding.onClick.AddListener(ShowUI);
        }
        if (!targetUI.activeInHierarchy)
        {
            buttonBuilding.onClick.AddListener(HideUI);
        }

        if(Input.GetMouseButtonDown(1)) // to hide seleceted building UI after right clicked
        {
            targetUI.SetActive(false);
        }
    }
    void ShowUI()
    {
        targetUI.SetActive(false);

        if(SelectedUI!= null)
        {
            SelectedUI.SetActive(false);
        }
    }

    private void HideUI()
    {
        targetUI.SetActive(true);

        if (SelectedUI != null)
        {
            SelectedUI.SetActive(false);
        }
    }

   
}
