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

    private void Update()
    {

        missionTrainVillagerDetail.text = string.Format("{0} / {1}", unitSelection.unitList.Count -1, 4);

        if(unitSelection.unitList.Count == 5 && tutorialProgression.learnTrainVilager == false)
        {
                tutorialProgression.learnTrainVilager = true;
                indicator.SetActive(false);
                arrowIndicator.SetActive(false);
                adviceSelectFloatingUI.SetActive(false);
        }

        if(tutorialProgression.learnAboutTC == true && tutorialProgression.learnTrainVilager == false)
        {
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
