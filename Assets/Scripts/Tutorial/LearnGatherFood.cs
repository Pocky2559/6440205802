using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LearnGatherFood : MonoBehaviour
{
    [Header("Required Component")]
    [SerializeField] private UnitSelection unitSelection;
    [SerializeField] private TutorialProgression tutorialProgression;
    [SerializeField] private ResourcesStatus resourcesStatus;
    [SerializeField] private AudioSource mainMissionCompleteSound;
    [SerializeField] private AudioSource secondaryMissionCompleteSound; 

    [Header("TMP_Text Details")]
    [SerializeField] private TMP_Text missionGatherFoodDetails;
    [SerializeField] private TMP_Text missionTrainVillagerDetails;

    [Header("Icons and indicators")]
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject checkIconMissionGatherFood;
    [SerializeField] private GameObject checkIconMissionTrainVillager;

    [Header("UI Panel")]
    [SerializeField] private GameObject adviceGatherFoodFloatingUI;

    [Header("Boolean")]
    private bool isStopCountFood;
    private bool isEnoughVillager;
    private bool isTrainVillagerCompleteSoundPlay;
    private bool isEnoughFoodCompleteSoundPlay;
    private bool isMainCompleteSoundPlay;


    private void Update()
    {
        if(tutorialProgression.learnBuildHouse == true && tutorialProgression.learnGatherFood == false)
        {
            if(isStopCountFood == false) missionGatherFoodDetails.text = string.Format("{0} / {1}",resourcesStatus.food_Amount, 250);
            missionTrainVillagerDetails.text = string.Format("{0} / {1}",unitSelection.unitList.Count-5, 5);

            if(resourcesStatus.food_Amount < 250 && isStopCountFood == false) // if has no enough food show indicator and advice panel to gather wood
            {
                indicator.transform.localPosition = new Vector3(110.489998f, 1.58000004f, 85.0199966f);
                indicator.SetActive(true);
                adviceGatherFoodFloatingUI.SetActive(true);
            }
            else // if food more than or equal 250
            {
                isStopCountFood = true;
                checkIconMissionGatherFood.SetActive(true);
                indicator.SetActive(false);
                adviceGatherFoodFloatingUI.SetActive(false);
                StartPlayMainMissionComplete();
            }

            if(unitSelection.unitList.Count >= 10)
            {
                isEnoughVillager = true;
                checkIconMissionTrainVillager.SetActive(true);
                StartPlayMainMissionComplete();
            }

            if(unitSelection.unitList.Count >= 10 && isStopCountFood == true) //Mission Completed
            {
                tutorialProgression.learnGatherFood = true;
            }

        }
    }

    private void StartPlayMainMissionComplete()
    {
        if(isEnoughVillager == true && isTrainVillagerCompleteSoundPlay == false) 
        {
            secondaryMissionCompleteSound.Play();
            isTrainVillagerCompleteSoundPlay = true;
        }

        if(isStopCountFood == true && isEnoughFoodCompleteSoundPlay == false)
        {
            secondaryMissionCompleteSound.Play();
            isEnoughFoodCompleteSoundPlay = true;
        }
    }

    private void StartPlaySecondaryMissionComplete()
    {
        if(isMainCompleteSoundPlay == false)
        {
            mainMissionCompleteSound.Play();
            isMainCompleteSoundPlay = true;
        }
    }
}
