using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LearnBuildHouse : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private HouseList houseList;
    [SerializeField] private ResourcesStatus resourcesStatus;
    [SerializeField] private TutorialProgression tutorialProgression;

    [Header("Icon and indicators")]
    [SerializeField] private GameObject houseCheckIcon;
    [SerializeField] private GameObject woodCheckIcon;
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject arrowIndicator1;
    [SerializeField] private GameObject arrowIndicator2;
    [SerializeField] private GameObject arrowIndicator3;
    [SerializeField] private GameObject adviceCollectWoodFloatingUI;

    [Header("TMP_Text Details")]
    [SerializeField] private TMP_Text buildHouseMissionDetail;
    [SerializeField] private TMP_Text collectWoodMissionDetail;

    [Header("Building Panel")]
    [SerializeField] private GameObject buildingPanel;
    [SerializeField] private GameObject housingPanel;

    [Header("Boolean")]
    [SerializeField] private bool isStopCountWood;

   

    private void Update()
    {
        if (tutorialProgression.learnTrainVilager == true && tutorialProgression.learnBuildHouse == false)
        {
            buildHouseMissionDetail.text = string.Format("{0} / {1}", houseList.houseList.Count, 1);

            if(isStopCountWood == false) collectWoodMissionDetail.text = string.Format("{0} / {1}", resourcesStatus.wood_Amount, 50);

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
                isStopCountWood = true; //Stop Count Wood
                woodCheckIcon.SetActive(true);
                adviceCollectWoodFloatingUI.SetActive(false);
                indicator.SetActive(false);

                //Show arrowIndicator to building
                if(buildingPanel.activeSelf == false) //if building panel isn't show 
                {
                    arrowIndicator1.SetActive(true); // we show the indicator to navigate player
                    arrowIndicator2.SetActive(false);
                    arrowIndicator3.SetActive(false);
                }

                if(buildingPanel.activeSelf == true && housingPanel.activeSelf == false) //if building panel is enable but it is not in housing catalog
                {
                    arrowIndicator2.SetActive(true); //show indicator to point at housing catalog
                    arrowIndicator1.SetActive(false);
                    arrowIndicator3.SetActive(false);
                }

                if (buildingPanel.activeSelf == true && housingPanel.activeSelf == true)
                {
                    arrowIndicator3.SetActive(true);
                    arrowIndicator1.SetActive(false);
                    arrowIndicator2.SetActive(false);
                }
            }

            else
            {
                arrowIndicator1.SetActive(false);
                arrowIndicator2.SetActive(false);
                arrowIndicator3.SetActive(false);             
            }
        }

        if(houseList.houseList.Count >= 1 && isStopCountWood == true && tutorialProgression.learnBuildHouse == false) //Mission Completed
        {
            tutorialProgression.learnBuildHouse = true;
        }
    }
}
