using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.AI;

public class VillagerStateController : MonoBehaviour
{
    public NavMeshAgent Villager;
    public GameObject resourcesStatusAssign;
    public GameObject unitSelectionAssign;
    public GameObject targetResources;
    public GameObject selectedStoringPoint;
    public GameObject upgradeStatusAssign;
    public LayerMask resorcesLayerMask;
    public LayerMask groundLayerMask;
    public int gatheringAmount;
    public int woodCarryingCapacity;
    public int goldStoneCarryingCapacity;
    public int foodCarryingCapacity;
    public string currentCarryingResource;
    public bool isStoringManual; 
    public Vector3 selectedPosition;
    public ResourcesStatus resourcesStatus; 
    public UnitStat unitStat;
    public UnitSelection unitSelection;
    public VillagerGatheringCapacity villagerGatheringCapacity;
    public GatheringWaypointForTree gatheringWaypointForTree;
    public GatheringWaypointForGold gatheringWaypointForGold;
    public GatheringWaypointForFood gatheringWaypointForFood;
    public GatheringWaypointForStone gatheringWaypointForStone;
    
    //Upgrade 
    public EconomicUpgradeDatabase economicUpgradeDatabase;
    public UpgradeStatus upgradeStatus;

    //States
    public VillagerBaseState currentState;
    public Villager_IdelState vil_IdelState = new();
    public Villager_MovingState vil_MovingState = new();
    public Villager_GatheringState vil_GatheringState = new();
    public Villager_StoringState vil_StoringState = new();

    private void Awake()
    {
        //create game object that contain component
        unitSelectionAssign = GameObject.FindGameObjectWithTag("UnitSelection");
        resourcesStatusAssign = GameObject.FindGameObjectWithTag("ResourcesStatus");
        upgradeStatusAssign = GameObject.FindGameObjectWithTag("UpgradeStatus");

        //Assign compoent
        resourcesStatus = resourcesStatusAssign.GetComponent<ResourcesStatus>();
        unitSelection = unitSelectionAssign.GetComponent<UnitSelection>();
        upgradeStatus = upgradeStatusAssign.GetComponent<UpgradeStatus>();
        Villager = GetComponent<NavMeshAgent>();
        unitStat = GetComponent<UnitStat>();

        //Assign Rsources Gathering Capacity
        woodCarryingCapacity = villagerGatheringCapacity.villagerCapacity[0].woodCapacity;
        goldStoneCarryingCapacity = villagerGatheringCapacity.villagerCapacity[0].goldStoneCapacity;
        foodCarryingCapacity = villagerGatheringCapacity.villagerCapacity[0].foodCapacity;
    }

    private void Start()
    {
        currentState = vil_IdelState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
        Debug.Log("Current State = " + currentState);
        Debug.Log("isStoringManual = " + isStoringManual);
    }

    public void SwitchState(VillagerBaseState statName)
    {
        currentState = statName;
        currentState.EnterState(this);
    }
}
