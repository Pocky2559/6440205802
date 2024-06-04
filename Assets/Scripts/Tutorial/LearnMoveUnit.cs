using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnMoveUnit : MonoBehaviour
{
    [SerializeField] private TutorialProgression tutorialProgression;
    [SerializeField] private UnitSelection unitSelection;
    [SerializeField] private GameObject moveIndicatorTutorial;
    [SerializeField] private GameObject moveTarget;
    [SerializeField] private float markerOffset;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private GameObject adviceSelectFlotingUI;
    [SerializeField] private GameObject adviceMoveFloatingUI;

    public void Update()
    {
        if(tutorialProgression.learnPanCam == true && tutorialProgression.learnMoveUnit == false) //if finish learn pan cam 
        {
            if (unitSelection.unitSelected.Count == 0)
            {
                Vector3 markerPosition = gameObject.transform.position;
                markerPosition.y = markerPosition.y + markerOffset;
                moveIndicatorTutorial.transform.position = markerPosition;
                moveIndicatorTutorial.SetActive(true);
                adviceSelectFlotingUI.SetActive(true);
                adviceMoveFloatingUI.SetActive(false);
            }
            else
            {
                Vector3 markerPosition = moveTarget.transform.position;
                markerPosition.y = markerPosition.y + markerOffset;
                moveIndicatorTutorial.transform.position = markerPosition;
                adviceSelectFlotingUI.SetActive(false);
                adviceMoveFloatingUI.SetActive(true);
            }

            if (unitSelection.unitSelected.Count > 0 && Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(transform.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayerMask))
                {
                    tutorialProgression.learnMoveUnit = true;
                    moveIndicatorTutorial.SetActive(false);
                    adviceMoveFloatingUI.SetActive(false);
                }
            }
        }
    }
}
