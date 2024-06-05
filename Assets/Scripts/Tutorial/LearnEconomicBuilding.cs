using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnEconomicBuilding : MonoBehaviour
{
    [Header("Required Component")]
    [SerializeField] private TutorialProgression tutorialProgression;

    [Header("Icons and indicator")]
    [SerializeField] private GameObject arrowIndicator1;
    [SerializeField] private GameObject arrowIndicator2;
    [SerializeField] private GameObject checkIconMissionBuildLumberCamp;
    [SerializeField] private GameObject checkIconMissionBuildMiningCart;
    [SerializeField] private GameObject checkIconMissionBuildWindMill;

    [Header("Buildinf Panel")]
    [SerializeField] private GameObject buildingPanel;
    [SerializeField] private GameObject economicPanel;

    [Header("Boolean")]
    [SerializeField] private bool isLumberCampBuild;
    [SerializeField] private bool isMiningCartBuild;
    [SerializeField] private bool isWindMillBuild;

    private void Update()
    {
        if (tutorialProgression.learnGatherFood == true &&  tutorialProgression.learnEconomicBuilding == false)
        {
            if(GameObject.FindGameObjectWithTag("Wood Storage") == true)
            {
                isLumberCampBuild = true;
                checkIconMissionBuildLumberCamp.SetActive(true);
            }

            if (GameObject.FindGameObjectWithTag("Gold Stone Storage") == true)
            {
                isMiningCartBuild = true;
                checkIconMissionBuildMiningCart.SetActive(true);
            }

            if (GameObject.FindGameObjectWithTag("Food Storage") == true)
            {
                isWindMillBuild = true;
                checkIconMissionBuildWindMill.SetActive(true);
            }

            // Arrow Indicators
            if(buildingPanel.activeSelf == false)
            {
                arrowIndicator1.SetActive(true);
                arrowIndicator2.SetActive(false);
            }

            if (buildingPanel.activeSelf == true && economicPanel.activeSelf == false) //if building panel is enable but it is not in economic catalog
            {
                arrowIndicator2.SetActive(true); //show indicator to point at housing catalog
                arrowIndicator1.SetActive(false);
            }

            if (buildingPanel.activeSelf == true && economicPanel.activeSelf == true)
            {
                arrowIndicator1.SetActive(false);
                arrowIndicator2.SetActive(false);
            }

            if (isLumberCampBuild == true && isMiningCartBuild == true && isWindMillBuild == true) //Mission Completed
            {
                tutorialProgression.learnEconomicBuilding = true;
            }
        }
    }
}
