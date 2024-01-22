using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField] ObjectDatabaseSO database;
    [SerializeField] PlacementSystem placementSystem;
    [SerializeField] SelectObject selectObject;
    //[SerializeField] PreviewStatus previewStatus;
    public TargetSelected targetSelected;
    public GameObject gameObj;
    private int token;
    private int offset = 1;
    public Material validMaterial;
    public Material InvalidMaterial;
    public MeshRenderer meshRenderer;
    public GameObject gridVisualization;
    public Grid grid;
    public void CreatObjectPreview()
    {  
        //to prevent placing the Game Object when click the UI
        if(gameObj != null)
        {
           StopPreview();
        }

        //clone gameObject reference by the id that recieve from class SelectObject
        gameObj = Instantiate(database.objects[selectObject.ID].Prefab);
        gameObj.tag = "Preview"; // change tag to preview to prevent village interact with it.
        gridVisualization.SetActive(true);

        //Send the Original Material to class PlacementSystem
        placementSystem.PlaceObjectState(gameObj); 

        CreateObjMaterial();

        //Add component from class DetectTrigger to make this Game Object can detect collider
        gameObj.AddComponent<DetectTrigger>();
        CanPlaceObjectStatus();
    }

    //Create new Material for the Game Object, First assign it as "validMaterial" so the player won't see the original material when it is instanciate
    private void CreateObjMaterial()
    {
        meshRenderer = gameObj.GetComponent<MeshRenderer>();
        meshRenderer.material = validMaterial;
    }

    //This method was called by class DetectTrigger when it can detect the collision (OnTriggerEnter)
    public void CanPlaceObjectStatus()
    {
        ShowValidPreview();
        placementSystem.PlaceObjectStatus(1);
      
    }
     
    //This method was called by class DetectTrigger when it cannot detect any collision (OnTriggerExit)
    public void CannotPlaceObjectStatus()
    {
        ShowInValidPreview();
        placementSystem.PlaceObjectStatus(0);
    }

    //Show the material of Game Object as "validMaterial"
    //check if because prevent error after destroy the Game Object 
    public void ShowValidPreview()
    {
       if(gameObj != null)meshRenderer.material = validMaterial;
    }

    //Show the material of Game Object as "InvalidMaterial"
    //check if because prevent error after destroy the Game Object
    public void ShowInValidPreview()
    {
       if (gameObj != null) meshRenderer.material = InvalidMaterial;
    }

    //Make the Game Object that was instantiated follow the mouse. The position of the mouse recieve from class PlacementSystem
    private void Update()
    {
        if(gameObj != null)
        {
            Vector3Int previewPosistion = grid.WorldToCell(placementSystem.currentPos);
            previewPosistion.y = previewPosistion.y + offset;
            gameObj.transform.position = grid.CellToWorld(previewPosistion);
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                placementSystem.PlaceObjectStatus(1);
                StopPreview();
            }
        }
        
    }

    // Destroy the Game Object and make Game Object at PlacementSystem is null
    public void StopPreview()
    {
        Destroy(gameObj);
        gridVisualization.SetActive(false);
        placementSystem.PlaceObjectState(null);
    }
    
}   
