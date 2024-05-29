using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [Header("Loading Screen")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingSlider;

    [Header("Menu Panel")]
    [SerializeField] private GameObject menuPanel;

    #region Go To Main Menu
    public void GoToMainMenu()
    {
        loadingScreen.SetActive(true);
        menuPanel.SetActive(false);
        StartCoroutine(StartGoToMainMenu());
    }

    IEnumerator StartGoToMainMenu()
    {
        yield return new WaitForSeconds(1.5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main Menu");

        if(asyncLoad.isDone == false )
        {
            float progressValue = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
    #endregion

    #region Restart the Game
    public void RestartLevel(string sceneName)
    {
        loadingScreen.SetActive(true);
        menuPanel.SetActive(false);
        StartCoroutine(StartToRestartLevel(sceneName));
    }

    IEnumerator StartToRestartLevel(string sceneName)
    {
        yield return new WaitForSeconds(1.5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        if(asyncLoad.isDone == false )
        {
            float progressValue = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
    #endregion
}
