using JetBrains.Annotations;
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
    [Header("LayerMask")]
    [SerializeField] private LayerMask Building;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private LayerMask npc;

    [Header("Buttons")]
    [SerializeField] private Button removeButtonTownCenter;
    [SerializeField] private Button removeButtonHouse;
    [SerializeField] private Button removeButtonLumberCamp;
    [SerializeField] private Button removeButtonMiningCart;
    [SerializeField] private Button removeButtonWindMill;
    [SerializeField] private Button removeButtonBarrack;
    [SerializeField] private Button removeButtonArtillary;

    [SerializeField] private Button produceVillagerButton;
    [SerializeField] private Button produceGunnerButton;
    [SerializeField] private Button produceLandsknetchButton;
    [SerializeField] private Button produceCaptainButton;

    [Header("General Attribute")]
    private Text nameText;
    private GameObject selectedObject;
    public Vector3 selectedObjectPosition;
    public GameObject selectedGameObj;

    [Header("Required Components")]
    [SerializeField] private PreviewSystem previewSystem;
    [SerializeField] private ObjectData data;
    [SerializeField] private UnitDatabaseSO unitDatabase;
    [SerializeField] private TownCenter_ProduceVillager produceVillager;
    [SerializeField] private Barrack_Producing produceMilitary;
    [SerializeField] private HouseList listOfHouse;
    [SerializeField] private SoundEffectController soundController;

    [Header("Building UI")]
    [SerializeField] private GameObject buildingUIDetails;
    [SerializeField] private GameObject townCenterUI;
    [SerializeField] private Transform townCenterVilQueUI;
    [SerializeField] private GameObject houseUI;
    [SerializeField] private GameObject lumberCampUI;
    [SerializeField] private GameObject miningCartUI;
    [SerializeField] private GameObject windMillUI;
    [SerializeField] private GameObject barrackUI;
    [SerializeField] private GameObject artillaryUI;
    [SerializeField] private GameObject buildingSelectionIndicator;
    [SerializeField] private Image villagerQueImage;

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
                    //selectedObjectPosition = hit.transform.position;
                    
                    data = selectedObject.GetComponent<ObjectData>();

                    if (hit.collider.CompareTag("Town Center"))
                    {
                        if(buildingSelectionIndicator != null)
                        {
                            buildingSelectionIndicator.SetActive(false);
                        }

                        if (produceVillager != null)
                        {
                            produceVillager.queIconInstantiateTarget.gameObject.SetActive(false); //Hide Que UI
                        }

                        if (produceMilitary != null) //Hide Que UI
                        {
                            produceMilitary.queIconInstantiateTarget.gameObject.SetActive(false);
                        }

                        produceVillager = hit.collider.GetComponent<TownCenter_ProduceVillager>();
                        soundController = hit.collider.GetComponentInChildren<SoundEffectController>();
                        soundController.PlayBuildingOrUnitSelectionSound();
                        produceVillagerButton.onClick.RemoveAllListeners();
                        produceVillagerButton.onClick.AddListener(produceVillager.AddVillagerQue);
                        buildingSelectionIndicator = hit.collider.transform.GetChild(1).gameObject;
                        selectedGameObj = selectedObject.GetComponentInChildren<PositionToSpawnUnit>().gameObject;

                        produceVillager.queIconInstantiateTarget.gameObject.SetActive(true); //Show Que UI

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

                        if (produceVillager != null)
                        {
                            produceVillager.queIconInstantiateTarget.gameObject.SetActive(false); //Hide Que UI
                        }

                        if (produceMilitary != null) //Hide Que UI
                        {
                            produceMilitary.queIconInstantiateTarget.gameObject.SetActive(false);
                        }

                        soundController = hit.collider.GetComponentInChildren<SoundEffectController>();
                        soundController.PlayBuildingOrUnitSelectionSound();

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

                        if (produceVillager != null)
                        {
                            produceVillager.queIconInstantiateTarget.gameObject.SetActive(false); //Hide Que UI
                        }

                        if (produceMilitary != null) //Hide Que UI
                        {
                            produceMilitary.queIconInstantiateTarget.gameObject.SetActive(false);
                        }

                        soundController = hit.collider.GetComponentInChildren<SoundEffectController>();
                        soundController.PlayBuildingOrUnitSelectionSound();

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

                        if (produceVillager != null)
                        {
                            produceVillager.queIconInstantiateTarget.gameObject.SetActive(false); //Hide Que UI
                        }

                        if (produceMilitary != null) //Hide Que UI
                        {
                            produceMilitary.queIconInstantiateTarget.gameObject.SetActive(false);
                        }

                        soundController = hit.collider.GetComponentInChildren<SoundEffectController>();
                        soundController.PlayBuildingOrUnitSelectionSound();

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

                        if (produceVillager != null)
                        {
                            produceVillager.queIconInstantiateTarget.gameObject.SetActive(false); //Hide Que UI
                        }

                        if (produceMilitary != null) //Hide Que UI
                        {
                            produceMilitary.queIconInstantiateTarget.gameObject.SetActive(false);
                        }

                        soundController = hit.collider.GetComponentInChildren<SoundEffectController>();
                        soundController.PlayBuildingOrUnitSelectionSound();

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
                        if(produceMilitary!= null) //Hide Que UI
                        {
                            produceMilitary.queIconInstantiateTarget.gameObject.SetActive(false);
                        }

                        soundController = hit.collider.GetComponentInChildren<SoundEffectController>();
                        soundController.PlayBuildingOrUnitSelectionSound();

                        //selectedObjectPosition = selectedObject.GetComponentInChildren<PositionToSpawnUnit>().transform.position;
                        selectedGameObj = selectedObject.GetComponentInChildren<PositionToSpawnUnit>().gameObject;

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

                        //Que Icon
                        produceMilitary.queIconInstantiateTarget.gameObject.SetActive(true);

                        if (buildingSelectionIndicator != null)
                        {
                            buildingSelectionIndicator.SetActive(false);
                        }

                        if (produceVillager != null)
                        {
                            produceVillager.queIconInstantiateTarget.gameObject.SetActive(false); //Hide Que UI
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

                        if (produceVillager != null)
                        {
                            produceVillager.queIconInstantiateTarget.gameObject.SetActive(false); //Hide Que UI
                        }

                        if (produceMilitary != null) //Hide Que UI
                        {
                            produceMilitary.queIconInstantiateTarget.gameObject.SetActive(false);
                        }

                        soundController = hit.collider.GetComponentInChildren<SoundEffectController>();
                        soundController.PlayBuildingOrUnitSelectionSound();

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

                if (produceVillager != null)
                {
                    produceVillager.queIconInstantiateTarget.gameObject.SetActive(false); //Hide Que UI
                }

                if (produceMilitary != null) //Hide Que UI
                {
                    produceMilitary.queIconInstantiateTarget.gameObject.SetActive(false);
                }
            }
        }
    }

    private void RemoveObject()
    {
        #region Remove Building
        if (selectedObject.CompareTag("House"))
        {
            listOfHouse.DeleteHouseFromList(selectedObject);
            listOfHouse.currentPopulationCapacity = listOfHouse.currentPopulationCapacity - listOfHouse.houseCapacityAssign;
            buildingUIDetails.SetActive(false);
            previewSystem.StopPreview();
            StartToPlayDeleteBuildingSound();
            Destroy(selectedObject, 0.1f);
        }
        
        if (selectedObject.CompareTag("Town Center")
            || selectedObject.CompareTag("Wood Storage")
            || selectedObject.CompareTag("Gold Stone Storage")
            || selectedObject.CompareTag("Food Storage")
            || selectedObject.CompareTag("Barrack")
            || selectedObject.CompareTag("Artillary"))
        {
            buildingUIDetails.SetActive(false);
            previewSystem.StopPreview();
            StartToPlayDeleteBuildingSound();
            Destroy(selectedObject, 0.1f);
        }
        #endregion
    }

    private void StartToPlayDeleteBuildingSound()
    {
        SoundEffectController soundEffectController = selectedObject.GetComponentInChildren<SoundEffectController>();
        GameObject deleteSound = Instantiate(soundEffectController.gameObject);
        AudioSource soundEffect = deleteSound.GetComponent<AudioSource>();
        soundEffect.enabled = true;
        soundEffectController.PlayDeleteBuildingSound();
        Destroy(deleteSound,3f);
    }
}
