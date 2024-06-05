using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LearnTrainVillager : MonoBehaviour
{
    [SerializeField] private UnitSelection unitSelection;
    [SerializeField] private TutorialProgression tutorialProgression;
    [SerializeField] private GameObject townCenterUI;
    [SerializeField] private GameObject arrowIndicator;
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject adviceSelectFloatingUI;
    [SerializeField] private TMP_Text missionTrainVillagerDetail;
    [SerializeField] private GameObject checkIconMissionTrainVillager;

    private void Update()
    {
        if(unitSelection.unitList.Count == 5 && tutorialProgression.learnTrainVilager == false) //Mission Completed
        {
                tutorialProgression.learnTrainVilager = true;
                checkIconMissionTrainVillager.SetActive(true);
                indicator.SetActive(false);
                arrowIndicator.SetActive(false);
                adviceSelectFloatingUI.SetActive(false);
        }

        if(tutorialProgression.learnAboutTC == true && tutorialProgression.learnTrainVilager == false)
        {
            missionTrainVillagerDetail.text = string.Format("{0} / {1}", unitSelection.unitList.Count -1, 4);

            if(townCenterUI.activeSelf == true) //if we select town center
            {
                indicator.SetActive(false);
                arrowIndicator.SetActive(true);
                adviceSelectFloatingUI.SetActive(false);
            }
            else
            {
                arrowIndicator.SetActive(false);
                indicator.SetActive(true);
                adviceSelectFloatingUI.SetActive(true);
            }
        }
    }
}
