using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HideSubUI : MonoBehaviour
{
    public GameObject[] targetUI;
    public GameObject showUI;
    public Button BT;
    public GameObject buttonName;

    private void Awake()
    {
        buttonName = GameObject.FindGameObjectWithTag(buttonName.name);
        Debug.Log("Awake Running");

        if(buttonName != null )  BT = buttonName.GetComponent<Button>();
    }
    public void StartHiding(int signal) // signal = 1 means "clicked"
    {
        if(signal == 1)
        {
            showUI.SetActive(true);
            targetUI[0].SetActive(false);
            targetUI[1].SetActive(false);
        }
    }
}


