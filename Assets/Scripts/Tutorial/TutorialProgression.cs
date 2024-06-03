using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialProgression : MonoBehaviour
{
    private bool isExitWelcome;
    private bool isFinishLearnMoveUnit;
    private bool isFinishLearnResourcesDetails;

     public bool exitWelcomePanel;
     public bool learnPanCam;
     public bool learnMoveUnit;
     public bool learnResourcesDetails;
    public bool learnAboutTC;

    [SerializeField] private GameObject welcomePanel;

    [SerializeField] private GameObject learnPanCamPanel;

    [SerializeField] private GameObject learnMoveUnitPanel;
    [SerializeField] private GameObject learnMoveUnitCheckIcon;

    [SerializeField] private GameObject learnResourcesDetailsPanel;

    [SerializeField] private GameObject learnAboutTCPanel;

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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && exitWelcomePanel == true && isExitWelcome == false) //if Skip learn pan cemera
        {
            FinishLearnPanCam();
            exitWelcomePanel = false;
            isExitWelcome = true;
        }

        if(learnMoveUnit == true && learnResourcesDetails == false && isFinishLearnMoveUnit == false) // if finish learn move unit
        {
            FinishLearnMoveUnit();
            isFinishLearnMoveUnit = true;
        }

        if(Input.GetKeyDown(KeyCode.Space) && learnMoveUnit == true && isFinishLearnResourcesDetails == false) //if Skip learn resources details
        {
            FinishLearnResourcesDetails();
            isFinishLearnResourcesDetails = true;
        }

        

        
    }
}
