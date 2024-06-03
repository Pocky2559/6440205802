using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnResourcesDetails : MonoBehaviour
{
    [SerializeField] private GameObject resourcesIcons;
    [SerializeField] private TutorialProgression tutorialProgression;
    private void Start()
    {
        resourcesIcons.SetActive(true);
    }
}
