using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Progression : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button level2Button;
    [SerializeField] private Button level3Button;
    [SerializeField] private Button level4Button;

    [Header("Icons")]
    [SerializeField] private GameObject level2LockIcon;
    [SerializeField] private GameObject level3LockIcon;
    [SerializeField] private GameObject level4LockIcon;

    private void Update()
    {
        CheckConditionToContinue();
    }

    public void CheckConditionToContinue()
    {
        if(PlayerPrefs.HasKey("Level1") == true)
        {
            level2Button.enabled = true;
            level2LockIcon.SetActive(false);
        }
        else
        {
            level2Button.enabled = false;
            level2LockIcon.SetActive(true);
        }

        //////////////////////////////////////////
        if(PlayerPrefs.HasKey("Level2") == true)
        {
            level3Button.enabled = true;
            level3LockIcon.SetActive(false);
        }
        else
        {
            level3Button.enabled = false;
            level3LockIcon.SetActive(true);
        }

        //////////////////////////////////////////
        if (PlayerPrefs.HasKey("Level3") == true)
        {
            level4Button.enabled = true;
            level4LockIcon.SetActive(false);
        }
        else
        {
            level4Button.enabled = false;
            level4LockIcon.SetActive(true);
        }
    }
}
