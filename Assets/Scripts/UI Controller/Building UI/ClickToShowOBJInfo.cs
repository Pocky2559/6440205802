using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickToShowOBJInfo : MonoBehaviour // this script controll the UIs that show building details (name,functions,etc.)
{
    public LayerMask Building;
    public LayerMask Ground;
    public LayerMask npc;

    public Button removeButtonTownCenter;
    public Button removeButtonHouse;
    public Button removeButtonLumberCamp;
    public Button removeButtonMiningCart;
    public Button removeButtonWindMill;
    public Button removeButtonBarrack;
    public Button removeButtonArtillary;

    public Button produceVillagerButton;
    public Button produceGunnerButton;
    public Button produceLandsknetchButton;
    public Button produceCaptainButton;

    public Text nameText;

    public GameObject selectedObject;
    public GameObject buildingUIDetails;
    public Vector3 selectedObjectPosition;

    public PreviewSystem previewSystem;
    public ObjectData data;
    public UnitDatabaseSO unitDatabase;
    public TownCenter_ProduceVillager produceVillager;
    public Barrack_Producing produceMilitary;
    public HouseList listOfHouse;

    /// Building Details UI
    public GameObject townCenterUI;
    public GameObject houseUI;
    public GameObject lumberCampUI;
    public GameObject miningCartUI;
    public GameObject windMillUI;
    public GameObject barrackUI;
    public GameObject artillaryUI;
    private GameObject buildingSelectionIndicator;


    private void Start()
    {
        removeButtonTownCenter.onClick.AddListener(RemoveObject);
        removeButtonHouse.onClick.AddListener(RemoveObject);
        removeButtonLumberCamp.onClick.AddListener(RemoveObject);
        removeButtonMiningCart.onClick.AddListener(RemoveObject);
        removeButtonWindMill.onClick.AddListener(RemoveObject);
        removeButtonBarrack.onClick.AddListener(RemoveObject);
        removeButtonArtillary.onClick.AddListener(RemoveObject);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Building))
            {
                if (hit.collider.GetComponent<TargetSelected>() != null)
                {
                    selectedObject = hit.collider.gameObject;
                    selectedObjectPosition = hit.transform.position;
                    data = selectedObject.GetComponent<ObjectData>();

                    if (hit.collider.CompareTag("Town Center"))
                    {
                        if(buildingSelectionIndicator != null)
                        {
                            buildingSelectionIndicator.SetActive(false);
                        }

                        produceVillager = hit.collider.GetComponent<TownCenter_ProduceVillager>();
                        produceVillagerButton.onClick.RemoveAllListeners();
                        produceVillagerButton.onClick.AddListener(produceVillager.AddVillagerQue);
                        buildingSelectionIndicator = hit.collider.transform.GetChild(1).gameObject;

                        #region Show UI
                        buildingUIDetails.SetActive(true);
                        townCenterUI.SetActive(true);
                        buildingSelectionIndicator.SetActive(true);
                        #endregion

                        #region Hide UI
                        houseUI.SetActive(false);
                        lumberCampUI.SetActive(false);
                        miningCartUI.SetActive(false);
                        windMillUI.SetActive(false);
                        barrackUI.SetActive(false);
                        artillaryUI.SetActive(false);
                        #endregion
                    }

                    if (hit.collider.CompareTag("House"))
                    {
                        if (buildingSelectionIndicator != null)
                        {
                            buildingSelectionIndicator.SetActive(false);
                        }

                        #region Show UI
                        buildingUIDetails.SetActive(true);
                        houseUI.SetActive(true);
                        buildingSelectionIndicator = hit.collider.transform.GetChild(1).gameObject;
                        buildingSelectionIndicator.SetActive(true);
                        
                        #endregion

                        #region Hide UI
                        townCenterUI.SetActive(false);
                        lumberCampUI.SetActive(false);
                        miningCartUI.SetActive(false);
                        windMillUI.SetActive(false);
                        barrackUI.SetActive(false);
                        artillaryUI.SetActive(false);
                        #endregion
                    }

                    if (hit.collider.CompareTag("Wood Storage"))
                    {
                        if (buildingSelectionIndicator != null)
                        {
                            buildingSelectionIndicator.SetActive(false);
                        }

                        #region Show UI
                        buildingUIDetails.SetActive(true);
                        lumberCampUI.SetActive(true);
                        buildingSelectionIndicator = hit.collider.transform.GetChild(1).gameObject;
                        buildingSelectionIndicator.SetActive(true);
                        #endregion

                        #region Hide UI 
                        townCenterUI.SetActive(false);
                        houseUI.SetActive(false);
                        miningCartUI.SetActive(false);
                        windMillUI.SetActive(false);
                        barrackUI.SetActive(false);
                        artillaryUI.SetActive(false);
                        #endregion
                    }

                    if (hit.collider.CompareTag("Gold Stone Storage"))
                    {
                        if (buildingSelectionIndicator != null)
                        {
                            buildingSelectionIndicator.SetActive(false);
                        }

                        #region Show UI
                        buildingUIDetails.SetActive(true);
                        miningCartUI.SetActive(true);
                        buildingSelectionIndicator = hit.collider.transform.GetChild(1).gameObject;
                        buildingSelectionIndicator.SetActive(true);
                        
                        #endregion

                        #region Hide UI
                        townCenterUI.SetActive(false);
                        houseUI.SetActive(false);
                        lumberCampUI.SetActive(false);
                        windMillUI.SetActive(false);
                        barrackUI.SetActive(false);
                        artillaryUI.SetActive(false);
                        #endregion
                    }

                    if (hit.collider.CompareTag("Food Storage"))
                    {
                        if (buildingSelectionIndicator != null)
                        {
                            buildingSelectionIndicator.SetActive(false);
                        }

                        #region Show UI
                        buildingUIDetails.SetActive(true);
                        windMillUI.SetActive(true);
                        buildingSelectionIndicator = hit.collider.transform.GetChild(1).gameObject;
                        buildingSelectionIndicator.SetActive(true);
                        #endregion

                        #region Hide UI
                        townCenterUI.SetActive(false);
                        houseUI.SetActive(false);
                        lumberCampUI.SetActive(false);
                        miningCartUI.SetActive(false);
                        barrackUI.SetActive(false);
                        artillaryUI.SetActive(false);
                        #endregion
                    }

                    if (hit.collider.CompareTag("Barrack"))
                    {
                        // These control UI clicking to train a troop
                        //Gunnner
                        produceMilitary = hit.collider.GetComponent<Barrack_Producing>();
                        produceGunnerButton.onClick.RemoveAllListeners();
                        produceGunnerButton.onClick.AddListener(produceMilitary.AddGunnerQue);

                        //Landsknecht
                        produceLandsknetchButton.onClick.RemoveAllListeners();
                        produceLandsknetchButton.onClick.AddListener(produceMilitary.AddLandsknetchQue);

                        //Captain
                        produceCaptainButton.onClick.RemoveAllListeners();
                        produceCaptainButton.onClick.AddListener(produceMilitary.AddCaptainQue);

                        if (buildingSelectionIndicator != null)
                        {
                            buildingSelectionIndicator.SetActive(false);
                        }

                        #region Show UI
                        buildingUIDetails.SetActive(true);
                        barrackUI.SetActive(true);
                        buildingSelectionIndicator = hit.collider.transform.GetChild(1).gameObject;
                        buildingSelectionIndicator.SetActive(true);
                        #endregion

                        #region Hide UI
                        townCenterUI.SetActive(false);
                        houseUI.SetActive(false);
                        lumberCampUI.SetActive(false);
                        miningCartUI.SetActive(false);
                        windMillUI.SetActive(false);
                        artillaryUI.SetActive(false);
                        #endregion
                    }

                    if (hit.collider.CompareTag("Artillary"))
                    {
                        if (buildingSelectionIndicator != null)
                        {
                            buildingSelectionIndicator.SetActive(false);
                        }

                        #region Show UI
                        buildingUIDetails.SetActive(true);
                        artillaryUI.SetActive(true);
                        buildingSelectionIndicator = hit.collider.transform.GetChild(1).gameObject;
                        buildingSelectionIndicator.SetActive(true);
                        #endregion

                        #region Hide UI
                        townCenterUI.SetActive(false);
                        houseUI.SetActive(false);
                        lumberCampUI.SetActive(false);
                        miningCartUI.SetActive(false);
                        windMillUI.SetActive(false);
                        barrackUI.SetActive(false);
                        
                        #endregion
                    }
                }
            }
            else if( Physics.Raycast(ray ,out hit, Mathf.Infinity, Ground)
                     && !EventSystem.current.IsPointerOverGameObject())
            {
                townCenterUI.SetActive(false);
                houseUI.SetActive(false);
                lumberCampUI.SetActive(false);
                miningCartUI.SetActive(false);
                windMillUI.SetActive(false);
                barrackUI.SetActive(false);
                artillaryUI.SetActive(false);

                if(buildingSelectionIndicator != null)
                {
                    buildingSelectionIndicator.SetActive(false);
                    buildingSelectionIndicator = null;
                }
            }
        }
    }
    public void RemoveObject()
    {
        #region Remove Building
        if (selectedObject.CompareTag("House"))
        {
            listOfHouse.DeleteHouseFromList(selectedObject);
            listOfHouse.currentHouseCapacity = listOfHouse.currentHouseCapacity - listOfHouse.houseCapacityAssign;
            Destroy(selectedObject);
            buildingUIDetails.SetActive(false);
            previewSystem.StopPreview();
            selectedObject = null;
        }
        
        if (selectedObject.CompareTag("Town Center")
            || selectedObject.CompareTag("Wood Storage")
            || selectedObject.CompareTag("Gold Stone Storage")
            || selectedObject.CompareTag("Food Storage")
            || selectedObject.CompareTag("Barrack")
            || selectedObject.CompareTag("Artillary"))
        {
            Destroy(selectedObject);
            buildingUIDetails.SetActive(false);
            previewSystem.StopPreview();
            selectedObject = null;
        }
        #endregion
    }
}
