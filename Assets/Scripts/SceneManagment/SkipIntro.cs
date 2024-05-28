using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class SkipIntro : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string sceneNameToSkip;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject introScreen;
    private bool isSceneLoading = false;
    [SerializeField] private Slider loadingSlider;

    private void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnVideoEnd(videoPlayer);
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        if(isSceneLoading == false)
        {
            isSceneLoading = true;
            loadingScreen.SetActive(true);
            introScreen.SetActive(false);
            LoadScene(sceneNameToSkip);
        }
       
    }
    private void OnDisable()
    {
        // Unsubscribe from the event to avoid memory leaks
        videoPlayer.loopPointReached -= OnVideoEnd;
    }

    private void LoadScene(string sceneName)
    {
        StartCoroutine(StartLoadScene(sceneName));
    }

    IEnumerator StartLoadScene(string sceneName)
    {
        yield return new WaitForSeconds(1.5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            float progressValue = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingSlider.value = progressValue;

            if (asyncLoad.progress >= 0.9f)
            {
                // The loading is almost done, we can activate the scene
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
