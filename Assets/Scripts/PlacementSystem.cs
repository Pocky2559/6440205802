using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementSystem : MonoBehaviour
{  
    [SerializeField] ObjectDatabaseSO database;
    [SerializeField] SelectObject selectObject;
    public Vector3 currentPos;
    public GameObject gameObj;
    public LayerMask layername;
    private int placeToken = 1;
    public bool canItPlace;
    private GameObject refGameObject;
    public Grid grid;
    private Vector3Int currentPosGrid;
    MeshRenderer meshRenderer;
    Material originMaterial;
    public BuildingResourcesSpend buildingResourcesSpend;
    public TargetSelected targetSelected;

    //this method recieve the Game Object from class PreviewSystem
    public void PlaceObjectState(GameObject gameOBJ)
    {
        //Assign Game Object
       gameObj = gameOBJ;
        // refGameObject = GameObject.FindGameObjectWithTag(database.objects[selectObject.ID].Name); //Find Game Object in hierachy with ID from class SelectObject
        refGameObject = database.objects[selectObject.ID].Prefab;

       if(gameObj != null)
        { 
          //Create the original material from the Original Game Object and assign that original material in "originMaterial"
          meshRenderer = gameObj.GetComponent<MeshRenderer>();
          originMaterial = meshRenderer.material;
        }
       
    } 

    //start to instantiate Game Object and its material is the original material
    public void StartPlacement()
    {
        buildingResourcesSpend.CheckAvailbleResources(selectObject.ID); // Check available resources first
        if(canItPlace == true) // if is has available resources
         {
             Destroy(refGameObject.GetComponent<DetectTrigger>()); // Delete DetectTrigger from new game object that we place
             refGameObject.layer = 6; // 6 layer = Building // change layermask of placed building
             currentPosGrid.y = 0; // make building stick to the ground
             meshRenderer.material = originMaterial;
             refGameObject = Instantiate(refGameObject, grid.CellToWorld(currentPosGrid), Quaternion.identity); //clone object
             refGameObject.AddComponent<TargetSelected>(); // add this compent for select and look details of this building
         }
    }

    //make the Game Object in this class is null and destroy it
    /* public void StopPlacement()
     {
        gameObj = null;
        Destroy(gameObj);
     }*/

    // this method recieve value from class PreviewSystem to assign the value of the token
    // ; token use for checking the state of placement that you can or cannot place the Game Object
    // 1 = can place, 0 = cannot place

    public void PlaceObjectStatus(int token)
    {
        placeToken = token;
        Debug.Log("Token " + placeToken);
    }

    // this method use for find the current position reference from the mouse position
    void Update()
    {
        ///////////////////Current Position////////////////////////
        /// Use Ray to detect world position from mouse position///
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        /// if the ray go from this original point to that direction, in distant 100 unit and hit something that has this type of layer
        if (Physics.Raycast(ray, out hit, 100, layername))
        {   
            //Assign the Current position as Vector3Int and set the y axis value = 1 to make the Game Object stick to the ground
            currentPos = hit.point;
            currentPos.y = 1;
            currentPosGrid = grid.WorldToCell(currentPos);
        }

        /// Checking the Game Object first, it cannot be null to prevent and error when place the Game Object/// 
        if (gameObj != null)
        {
            
            ///if click left mouse button, the Game Object will be placed
            if (Input.GetMouseButtonDown(0) || placeToken == 0)
            {   
                //if click on the UI Nothing will happen, this if statement use for preventing place the Game Object when click the UI
                // or the token = 0
                if (EventSystem.current.IsPointerOverGameObject() || placeToken == 0)
                {
                   
                }

                // if the token = 1; the Game Object wii be placed
                else if(placeToken == 1)
                {  
                    StartPlacement();
                    placeToken = 0;
                    Debug.Log("StartPlace");
                }
                

                /*if (Input.GetKeyDown(KeyCode.Mouse1)) 
                {   
                    placeToken = 1;
                    StopPlacement();
                }*/
            }
        } 
    }

       
}
