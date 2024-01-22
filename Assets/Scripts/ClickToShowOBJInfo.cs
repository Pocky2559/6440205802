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

    public Text nameText;

    public GameObject selectedObject;
    public GameObject buildingUIDetails;
    public Vector3 selectedObjectPosition;

    public PreviewSystem previewSystem;
    public ObjectData data;
    public TownCenter_ProduceVillager produceVillager;

    /// Building Details UI
    public GameObject townCenterUI;
    public GameObject houseUI;
    public GameObject lumberCampUI;
    public GameObject miningCartUI;
    public GameObject windMillUI;
    public GameObject barrackUI;
    public GameObject artillaryUI;

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
                        produceVillager = hit.collider.GetComponent<TownCenter_ProduceVillager>();
                        produceVillagerButton.onClick.RemoveAllListeners();
                        produceVillagerButton.onClick.AddListener(produceVillager.AddVillagerQue);

                        #region Show UI
                        buildingUIDetails.SetActive(true);
                        townCenterUI.SetActive(true);
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
                        #region Show UI
                        buildingUIDetails.SetActive(true);
                        houseUI.SetActive(true);
                        
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
                        #region Show UI
                        buildingUIDetails.SetActive(true);
                        lumberCampUI.SetActive(true);
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
                        #region Show UI
                        buildingUIDetails.SetActive(true);
                        miningCartUI.SetActive(true);
                        
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
                        #region Show UI
                        buildingUIDetails.SetActive(true);
                        windMillUI.SetActive(true);
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
                        #region Show UI
                        buildingUIDetails.SetActive(true);
                        barrackUI.SetActive(true);
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
                        #region Show UI
                        buildingUIDetails.SetActive(true);
                        artillaryUI.SetActive(true);
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
        }
    }
    private void RemoveObject()
    {
        if(selectedObject != null) 
        {
            Destroy(selectedObject);
            buildingUIDetails.SetActive(false);
            previewSystem.StopPreview();
            selectedObject = null;
        }
    }
}
