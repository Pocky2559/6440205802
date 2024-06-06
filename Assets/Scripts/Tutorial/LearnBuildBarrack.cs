using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LearnBuildBarrack : MonoBehaviour
{
    [Header("Required Component")]
    [SerializeField] private TutorialProgression tutorialProgression;

    [Header("Camera and enemy")]
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float panSpeed;
    [SerializeField] private Vector3 cameraOriginPos;
    [SerializeField] private Vector3 cameraEnemyPos;
    [SerializeField] private float cameraAndTargetDistance;

    [Header("Icons and indicators")]
    [SerializeField] private GameObject arrowIndicator1;
    [SerializeField] private GameObject arrowIndicator2;
    [SerializeField] private GameObject arrowIndicator3;
    [SerializeField] private GameObject buildBarrackMissionCheckIcon;
    [SerializeField] private GameObject trainMilitaryMissionCheckIcon;
    [SerializeField] private GameObject eliminateEnemyMissionCheckIcon;

    [Header("Building Panel")]
    [SerializeField] private GameObject buildingPanel;
    [SerializeField] private GameObject militaryPanel;
    [SerializeField] private GameObject enemyIndicator;

    [Header("Boolean")]
    [SerializeField] private bool isBarrackBuild;
    [SerializeField] private bool isPanBack;
    [SerializeField] private bool isStopPanning;
    [SerializeField] private bool isEnemySpawn;
    [SerializeField] private bool isMilitaryTrain;
    [SerializeField] private bool isEnemyEliminate;
 
    private void Start()
    {
        cameraOriginPos = new Vector3(99.9000015f, -48f, 100f);
        cameraEnemyPos = new Vector3(99.9000015f, -48f, 63f);
  
    }

    private void Update()
    {
        if(tutorialProgression.learnEconomicBuilding == true && tutorialProgression.learnBuildBarrack == false)
        {
            PanCamera();

            if (isEnemySpawn == false)
            {
                isEnemySpawn = true;
                enemy.SetActive(true);
                enemyIndicator.SetActive(true);
            }

            // Start show advice build barrack indicator if player did not build a barrack
            if(isStopPanning == true && isBarrackBuild == false)
            {
                // Arrow Indicators
                if (buildingPanel.activeSelf == false)
                {
                    arrowIndicator1.SetActive(true);
                    arrowIndicator2.SetActive(false);
                    arrowIndicator3.SetActive(false);
                }

                if (buildingPanel.activeSelf == true && militaryPanel.activeSelf == false) //if building panel is enable but it is not in economic catalog
                {
                    arrowIndicator2.SetActive(true); //show indicator to point at housing catalog
                    arrowIndicator1.SetActive(false);
                    arrowIndicator3.SetActive(false);
                }

                if (buildingPanel.activeSelf == true && militaryPanel.activeSelf == true)
                {
                    arrowIndicator3.SetActive(true);
                    arrowIndicator1.SetActive(false);
                    arrowIndicator2.SetActive(false);
                }
            }

            if(isBarrackBuild == false && GameObject.FindGameObjectWithTag("Barrack") != null) //if finish build barrack
            {
                isBarrackBuild = true;
                buildBarrackMissionCheckIcon.SetActive(true);
                arrowIndicator1.SetActive(false);
                arrowIndicator2.SetActive(false);
                arrowIndicator3.SetActive(false);
            }
            if(isMilitaryTrain == false && (GameObject.FindGameObjectWithTag("Gunner") 
                                            || GameObject.FindGameObjectWithTag("Landsknecht")
                                            || GameObject.FindGameObjectWithTag("Captain"))) //if finish train military
            {
                isMilitaryTrain = true;
                trainMilitaryMissionCheckIcon.SetActive(true);
            }

            if(isEnemyEliminate == false && GameObject.FindGameObjectWithTag("OttomanRecruit") == null) //if can eliminate enemy
            {
                isEnemyEliminate = true;
                eliminateEnemyMissionCheckIcon.SetActive(true);
            }

            if(isBarrackBuild == true && isMilitaryTrain == true && isEnemyEliminate == true)
            {
                tutorialProgression.learnBuildBarrack = true;
            }
        }
    }

    private void PanCamera()
    {
        if(isPanBack == false) //Start Pan Camera to enemy
        {
            mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.position, cameraEnemyPos, panSpeed * Time.deltaTime);
            if (Vector3.Distance(mainCamera.transform.position, cameraEnemyPos) < cameraAndTargetDistance) //if reach camera reach enemy
            {
                StartCoroutine(StartPanCameraBack());
            }
        }


        if(isPanBack == true && isStopPanning == false) //pan camera back to town center
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraOriginPos, panSpeed * Time.deltaTime);
            if (Vector3.Distance(mainCamera.transform.position, cameraOriginPos) < cameraAndTargetDistance) //if reach town center stop panning
            {
                isStopPanning = true;
            }
        }
    }
    IEnumerator StartPanCameraBack()
    {
        yield return new WaitForSeconds(3);
        isPanBack = true;
    }
}
