using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSceneManagement : MonoBehaviour
{
    [Header("Loading Screen")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingSlider;

    public void ChooseScene(string sceneName)
    {
        if (sceneName == "Level1")
        {
            loadingScreen.SetActive(true);
            StartCoroutine(LoadScene1("IntroLevel1"));
        }

        if (sceneName == "Level2")
        {
            loadingScreen.SetActive(true);
            StartCoroutine(LoadScene1("IntroLevel2"));
        }

        if (sceneName == "Level3")
        {
            loadingScreen.SetActive(true);
            StartCoroutine(LoadScene1("IntroLevel3"));
        }

        if (sceneName == "Level4")
        {
            loadingScreen.SetActive(true);
            StartCoroutine(LoadScene1("IntroLevel4"));
        }

        if (sceneName == "Tutorial")
        {
            loadingScreen.SetActive(true);
            StartCoroutine(LoadScene1("Tutorial"));
        }

        if (sceneName == "Outro")
        {
            loadingScreen.SetActive(true);
            StartCoroutine(LoadScene1("Outro"));
        }
    }

    public void ExitToDeskTop()
    {
        Application.Quit();
    }
    
    IEnumerator LoadScene1(string sceneName)
    {
        yield return new WaitForSeconds(1.5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while(!asyncLoad.isDone) 
        {
          float progressValue = Mathf.Clamp01(asyncLoad.progress / 0.9f);
          loadingSlider.value = progressValue;
          yield return null;
        }
    }
}
