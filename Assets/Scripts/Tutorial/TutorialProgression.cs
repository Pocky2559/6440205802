using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialProgression : MonoBehaviour
{
    private bool isExitWelcome;
    private bool isFinishLearnMoveUnit;
    private bool isFinishLearnResourcesDetails;
    private bool isFinishLearnAboutTC;
    private bool isFinishLearnTrainVillager;
    private bool isFinishLearnBuildHouse;

     public bool exitWelcomePanel;
     public bool learnPanCam;
     public bool learnMoveUnit;
     public bool learnResourcesDetails;
     public bool learnAboutTC;
     public bool learnTrainVilager;
     public bool learnBuildHouse;

    [SerializeField] private GameObject welcomePanel;

    [SerializeField] private GameObject learnPanCamPanel;

    [SerializeField] private GameObject learnMoveUnitPanel;
    [SerializeField] private GameObject learnMoveUnitCheckIcon;

    [SerializeField] private GameObject learnResourcesDetailsPanel;

    [SerializeField] private GameObject learnAboutTCPanel;

    [SerializeField] private GameObject learnTrainVillagerPanel;
    [SerializeField] private GameObject learnTrainVillagerCheckIcon;

    [SerializeField] private GameObject learnBuildHousePanel;
    [SerializeField] private GameObject learnBuildHouseCheckIcon;

    private void Start()
    {
        welcomePanel.SetActive(true);
    }

    public void FinishWelcomePanel()
    {
        exitWelcomePanel = true;
        welcomePanel.SetActive(false); //close welcome panel
        learnPanCamPanel.SetActive(true); // Show learn pan cam panel
    }

    public void FinishLearnPanCam()
    {
        learnPanCam = true;
        learnPanCamPanel.SetActive(false); //close learn pan cam panel
        learnMoveUnitPanel.SetActive(true); // show learn move unit panel
    }

    #region Finish Learn Move Unit
    public void FinishLearnMoveUnit()
    {
        learnMoveUnitCheckIcon.SetActive(true);
        StartCoroutine(CloseFinishLearnMoveUnit());
    }

    IEnumerator CloseFinishLearnMoveUnit()
    {
        yield return new WaitForSeconds(3);
        learnMoveUnitCheckIcon.SetActive(false);
        learnMoveUnitPanel.SetActive(false);
        learnResourcesDetailsPanel.SetActive(true);
    }
    #endregion

    public void FinishLearnResourcesDetails()
    {
        learnResourcesDetails = true;
        learnResourcesDetailsPanel.SetActive(false);
        learnAboutTCPanel.SetActive(true);
    }

    public void FinishLearnAboutTc()
    {
        learnAboutTC = true;
        learnAboutTCPanel.SetActive(false);
        learnTrainVillagerPanel.SetActive(true);
    }

    #region Learn Train Villager
    public void FinishLearnTrainVillager()
    {
        learnTrainVillagerCheckIcon.SetActive(true);
        StartCoroutine(CloseFinishLearnTrainVillager());
    }

    IEnumerator CloseFinishLearnTrainVillager()
    {
        yield return new WaitForSeconds(3);
        learnTrainVillagerCheckIcon.SetActive(false);
        learnTrainVillagerPanel.SetActive(false);
        learnBuildHousePanel.SetActive(true);
    }
    #endregion

    #region Learn Build Hpuse
    public void FinishLearnBuildHouse()
    {
       learnBuildHouseCheckIcon.SetActive(true);
       StartCoroutine(CloseFinishLearnBuildHouse());
    }

    IEnumerator CloseFinishLearnBuildHouse()
    {
        yield return new WaitForSeconds(3);
        learnBuildHouseCheckIcon.SetActive(true);
        learnBuildHousePanel.SetActive(false);
    }


    #endregion

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && exitWelcomePanel == true && isExitWelcome == false) //if Skip learn pan cemera
        {
            FinishLearnPanCam();
            exitWelcomePanel = false;
            isExitWelcome = true;
        }

        else if(learnMoveUnit == true && learnResourcesDetails == false && isFinishLearnMoveUnit == false) // if finish learn move unit
        {
            FinishLearnMoveUnit();
            isFinishLearnMoveUnit = true;
        }

        else if(Input.GetKeyDown(KeyCode.Space) && learnMoveUnit == true && isFinishLearnResourcesDetails == false) //if Skip learn resources details
        {
            FinishLearnResourcesDetails();
            isFinishLearnResourcesDetails = true;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && learnResourcesDetails == true && isFinishLearnAboutTC == false) //if skip learn about tc
        {
            FinishLearnAboutTc();
            isFinishLearnAboutTC = true;
        }

        else if (learnTrainVilager == true && isFinishLearnTrainVillager == false)
        {
            FinishLearnTrainVillager();
            isFinishLearnTrainVillager = true;
        }

        else if (learnBuildHouse == true && isFinishLearnBuildHouse == false)
        {
            FinishLearnBuildHouse();
            isFinishLearnBuildHouse = true;
        }
    }
}
