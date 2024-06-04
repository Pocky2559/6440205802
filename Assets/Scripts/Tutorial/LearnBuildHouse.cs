using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LearnBuildHouse : MonoBehaviour
{
    [SerializeField] private HouseList houseList;
    [SerializeField] private ResourcesStatus resourcesStatus;

    [SerializeField] private GameObject houseCheckIcon;
    [SerializeField] private GameObject woodCheckIcon;
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject adviceCollectWoodFloatingUI;

    [SerializeField] private TMP_Text buildHouseMissionDetail;
    [SerializeField] private TMP_Text collectWoodMissionDetail;

    [SerializeField] private TutorialProgression tutorialProgression;

    private void Update()
    {
        if (tutorialProgression.learnTrainVilager == true && tutorialProgression.learnBuildHouse == false)
        {
            buildHouseMissionDetail.text = string.Format("{0} / {1}", houseList.houseList.Count, 1);
            collectWoodMissionDetail.text = string.Format("{0} / {1}", resourcesStatus.wood_Amount, 50);

            //if Don't have enough wood show advice Ui and indicator
            // Check House
            if (houseList.houseList.Count >= 1)
            {
                houseCheckIcon.SetActive(true);
                adviceCollectWoodFloatingUI.SetActive(false);
                indicator.SetActive(false);
            }
            else
            {
                houseCheckIcon.SetActive(false);
                indicator.SetActive(true);
                indicator.transform.position = new Vector3(112.470001f, 2.61999989f, 110.730003f);
                adviceCollectWoodFloatingUI.SetActive(true);
            }

            //if have enough wood show arrowIndicator to advice where is the house
            // Check Wood
            if (resourcesStatus.wood_Amount >= 50)
            {
                woodCheckIcon.SetActive(true);
                adviceCollectWoodFloatingUI.SetActive(false);
                indicator.SetActive(false);
            }
        }
    }
}
