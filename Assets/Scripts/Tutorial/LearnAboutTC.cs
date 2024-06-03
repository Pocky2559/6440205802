using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnAboutTC : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject indicator;
    [SerializeField] private Transform townCenterPos;
    [SerializeField] private TutorialProgression tutorialProgression;
    private bool isPanFinish;

    private void Update()
    {
        if(tutorialProgression.learnResourcesDetails == true && tutorialProgression.learnAboutTC == false && isPanFinish == false)
        {
            mainCamera.transform.localPosition = new Vector3(99.4000015f, -48, 108);
            indicator.transform.position = new Vector3(99.6699982f, 2.79999995f, 99.4800034f);
            //indicator.transform.position = townCenterPos.position;
            indicator.SetActive(true);
            isPanFinish = true;
        }
    }
}
