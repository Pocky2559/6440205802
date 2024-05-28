using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class StartIntro : MonoBehaviour
{
    [SerializeField] private GameObject blackScreen;

    private void Start()
    {
        StartCoroutine(ShowIntroScene());
    }

    IEnumerator ShowIntroScene()
    {
        yield return new WaitForSeconds(1);
        blackScreen.SetActive(false);
    }
}
