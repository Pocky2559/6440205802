using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementSystem : MonoBehaviour
{
    [Header("Building System")]
    [SerializeField] private ObjectDatabaseSO database;
    [SerializeField] private SelectObject selectObject;
    [SerializeField] private Grid grid;
    public Vector3 currentPos;
    private Vector3Int currentPosGrid;
    public LayerMask layernameToPlace;
    private GameObject refBuilding;
    public bool canItPlace;
    private int placeToken = 1;
    private string refBuildingName;

    [Header("Required Components")]
    [SerializeField] private BuildingResourcesSpend buildingResourcesSpend;
    [SerializeField] private HouseList listOfHouse;
    [SerializeField] private PreSelection preSelection;
    [SerializeField] private SoundEffectController soundEffectController;

    public void PlaceObjectState()
    {
        refBuilding = database.objects[selectObject.ID].Prefab;
        refBuildingName = database.objects[selectObject.ID].Name;
    } 

    //start to instantiate Game Object and its material is the original material
    public void StartPlacement()
    {
        buildingResourcesSpend.CheckAvailbleResources(selectObject.ID); // Check available resources first
        if(canItPlace == true) // if is has available resources
        {
             Destroy(refBuilding.GetComponent<DetectTrigger>()); // Delete DetectTrigger from new game object that we place
             refBuilding.layer = 6; // 6 layer = Building // change layermask of placed building
             currentPosGrid.y = 0; // make building stick to the ground
             refBuilding = Instantiate(refBuilding, grid.CellToWorld(currentPosGrid), Quaternion.identity); //clone object
             refBuilding.AddComponent<TargetSelected>(); // add this compent for select and look details of this building
             StartToPlayPlaceBuildingSound();

             //Add Component PreSelection and assign its value
             refBuilding.AddComponent<PreSelection>();
             preSelection = refBuilding.GetComponent<PreSelection>();
             preSelection.preSelectionIndicator = refBuilding.transform.GetChild(0).gameObject;
        }

        if (canItPlace == true && refBuildingName == "House" && listOfHouse.currentPopulationCapacity <= 200)
        {
            listOfHouse.AddHouseToList(refBuilding);
        }
    }

    public void PlaceObjectStatus(int token)
    {
        placeToken = token;
    }

    private void StartToPlayPlaceBuildingSound()
    {
        soundEffectController = refBuilding.GetComponentInChildren<SoundEffectController>();
        soundEffectController.PlayPlaceBuildingSound();
    }

    // this method use for find the current position reference from the mouse position
    void Update()
    {
        ///////////////////Current Position////////////////////////
        /// Use Ray to detect world position from mouse position///
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        /// if the ray go from this original point to that direction, in distant 100 unit and hit something that has this type of layer
        if (Physics.Raycast(ray, out hit, 100, layernameToPlace))
        {   
            //Assign the Current position as Vector3Int and set the y axis value = 1 to make the Game Object stick to the ground
            currentPos = hit.point;
            currentPos.y = 1;
            currentPosGrid = grid.WorldToCell(currentPos);
        }

        /// Checking the Game Object first, it cannot be null to prevent and error when place the Game Object/// 
        //if (previewBuilding != null)
        if(refBuilding != null)
        {
            ///if click left mouse button, the Game Object will be placed
            if (Input.GetMouseButtonDown(0) && placeToken == 1)
            {   
                //if click on the UI Nothing will happen, this if statement use for preventing place the Game Object when click the UI
                // or the token = 0
                if (EventSystem.current.IsPointerOverGameObject() || placeToken == 0)
                {
                   //Do nothing
                }

                // if the token = 1; the Game Object wii be placed
                else if(placeToken == 1)
                {  
                    StartPlacement();
                    placeToken = 0;
                }
            }
        } 
    }
}
