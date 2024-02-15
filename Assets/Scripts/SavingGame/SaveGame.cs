using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public void SaveLevel1()
    {
        PlayerPrefs.SetString("Level1", "Passed");
        PlayerPrefs.Save();
    }   

    public void SaveLevel2()
    {
        PlayerPrefs.SetString("Level2", "Passed");
        PlayerPrefs.Save();
    }

    public void SaveLevel3()
    {
        PlayerPrefs.SetString("Level3", "Passed");
        PlayerPrefs.Save();
    }

    public void SaveLevel4()
    {
        PlayerPrefs.SetString("Level4", "Passed");
        PlayerPrefs.Save();
    }
}
