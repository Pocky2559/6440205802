using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LearnBuildBarrack : MonoBehaviour
{
    [Header("Required Component")]
    [SerializeField] private TutorialProgression tutorialProgression;
    [SerializeField] private AudioSource mainMissionCompleteSound;
    [SerializeField] private AudioSource secondaryMissionCompleteSound;
    [SerializeField] private CameraController cameraController;

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
    private bool isPanBack;
    private bool isStopPanning;
    private bool isEnemySpawn;
    private bool isBarrackBuild;
    private bool isMilitaryTrain;
    private bool isEnemyEliminate;
    private bool isMainCompleteSoundPlay;
    private bool isBarrackBuildMissionCompletePlaySound;
    private bool isMilitaryTrainMissionCompletePlaySound;
    private bool isEnemyEliminateMissionCompletePlaySound;

 
    private void Start()
    {
        cameraOriginPos = cameraController.transform.position;//new Vector3(99.9000015f, -48f, 100f);
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
                StartPlaySecondaryMissionCompleteSound();
            }
            if(isMilitaryTrain == false && (GameObject.FindGameObjectWithTag("Gunner") 
                                            || GameObject.FindGameObjectWithTag("Landsknecht")
                                            || GameObject.FindGameObjectWithTag("Captain"))) //if finish train military
            {
                isMilitaryTrain = true;
                trainMilitaryMissionCheckIcon.SetActive(true);
                StartPlaySecondaryMissionCompleteSound();
            }

            if(isEnemyEliminate == false && GameObject.FindGameObjectWithTag("OttomanRecruit") == null) //if can eliminate enemy
            {
                isEnemyEliminate = true;
                eliminateEnemyMissionCheckIcon.SetActive(true);
                StartPlaySecondaryMissionCompleteSound();
            }

            if(isBarrackBuild == true && isMilitaryTrain == true && isEnemyEliminate == true)
            {
                tutorialProgression.learnBuildBarrack = true;
                StartPlayMainMissionCompleteSound();
            }
        }
    }

    private void PanCamera()
    {
        if(isPanBack == false) //Start Pan Camera to enemy
        {
            //mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.position, cameraEnemyPos, panSpeed * Time.deltaTime);
            cameraController.isStopPanning = true;
            cameraController.targetPosition = cameraEnemyPos;
            if (Vector3.Distance(mainCamera.transform.position, cameraEnemyPos) < cameraAndTargetDistance) //if reach cameraDemo reach enemy
            {
                StartCoroutine(StartPanCameraBack());
            }
        }


        if(isPanBack == true && isStopPanning == false) //pan cameraDemo back to town center
        {
            //mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraOriginPos, panSpeed * Time.deltaTime);
            //cameraController.targetPosition = cameraOriginPos;
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
        cameraController.targetPosition = cameraOriginPos;
        yield return new WaitForSeconds(1.5f);
        cameraController.isStopPanning = false;

    }

    private void StartPlayMainMissionCompleteSound()
    {
        if (isMainCompleteSoundPlay == false)
        {
            mainMissionCompleteSound.Play();
            isMainCompleteSoundPlay = true;
        }
    }

    private void StartPlaySecondaryMissionCompleteSound()
    {
 
        if (isBarrackBuild == true && isBarrackBuildMissionCompletePlaySound == false)
        {
            secondaryMissionCompleteSound.Play();
            isBarrackBuildMissionCompletePlaySound = true;
        }

        if (isMilitaryTrain == true && isMilitaryTrainMissionCompletePlaySound == false)
        {
            secondaryMissionCompleteSound.Play();
            isMilitaryTrainMissionCompletePlaySound = true;
        }

        if (isEnemyEliminate == true && isEnemyEliminateMissionCompletePlaySound == false)
        {
            secondaryMissionCompleteSound.Play();
            isEnemyEliminateMissionCompletePlaySound = true;
        }

    }
}
