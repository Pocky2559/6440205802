using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [Header("Preview System")]
    [SerializeField] private GameObject gridVisualization;
    [SerializeField] private Grid grid;
    public GameObject previewBuilding;
    private int token;
    private int offset = 1; 
    

    [Header("Required Components")]
    [SerializeField] ObjectDatabaseSO database;
    [SerializeField] PlacementSystem placementSystem;
    [SerializeField] SelectObject selectObject;
    [SerializeField] private PlacementNotification placementNotification;

    public void CreatObjectPreview()
    {
        //to prevent placing the Game Object when click the UI
        if (previewBuilding != null)
        {
            StopPreview();
        }

        selectObject.isSelectBuilding = true;

        //clone gameObject reference by the id that recieve from class SelectObject
        previewBuilding = Instantiate(database.objects[selectObject.ID].Prefab);
        previewBuilding.tag = "Preview"; // change tag to preview to prevent village interact with it.
        gridVisualization.SetActive(true);

        //Send the Original Material to class PlacementSystem
        placementSystem.PlaceObjectState(); 

       
        //Add component from class DetectTrigger to make this Game Object can detect collider
        previewBuilding.AddComponent<DetectTrigger>();
        CanPlaceObjectStatus();
    }

    

    //This method was called by class DetectTrigger when it can detect the collision (OnTriggerEnter)
    public void CanPlaceObjectStatus()
    {
        //ShowValidPreview();
        placementSystem.PlaceObjectStatus(1);
        placementNotification.ShowIndicatorCanPlace(previewBuilding);
    }
     
    //This method was called by class DetectTrigger when it cannot detect any collision (OnTriggerExit)
    public void CannotPlaceObjectStatus()
    {
        //ShowInValidPreview();
        placementSystem.PlaceObjectStatus(0);
        placementNotification.ShowIndicatorCannotPlace(previewBuilding);
    }


    //Make the Game Object that was instantiated follow the mouse. The position of the mouse recieve from class PlacementSystem
    private void Update()
    {
        if(previewBuilding != null)
        {
            Vector3Int previewPosistion = grid.WorldToCell(placementSystem.currentPos);
            previewPosistion.y = previewPosistion.y + offset;
            previewBuilding.transform.position = grid.CellToWorld(previewPosistion);
            if (Input.GetKeyDown(KeyCode.Mouse1)) // if right click stop preview
            {
                placementSystem.PlaceObjectStatus(0);
                StopPreview();
            }
        }
    }

    // Destroy the Game Object and make Game Object at PlacementSystem is null
    public void StopPreview()
    {
        Destroy(previewBuilding);
        gridVisualization.SetActive(false);
        placementNotification.HideIndicator();
        placementNotification.HideNeedMoreResources();
        selectObject.isSelectBuilding = false;
    }
}   
