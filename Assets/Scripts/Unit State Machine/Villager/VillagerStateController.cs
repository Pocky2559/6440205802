using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

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
    public HouseList population;
    public GatheringWaypointForTree gatheringWaypointForTree;
    public GatheringWaypointForGold gatheringWaypointForGold;
    public GatheringWaypointForFood gatheringWaypointForFood;
    public GatheringWaypointForStone gatheringWaypointForStone;
    public Animator villagerAnimator;
    public GameObject basket;
    public RigBuilder rigBuilder;
    public Collider villagerCollider;
    
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
        population = GameObject.FindGameObjectWithTag("PopulationController").GetComponent<HouseList>();
        Villager = GetComponent<NavMeshAgent>();
        unitStat = GetComponent<UnitStat>();
        villagerAnimator = GetComponent<Animator>();
        villagerCollider = GetComponent<Collider>();    

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
    }

    public void SwitchState(VillagerBaseState statName)
    {
        currentState = statName;
        currentState.EnterState(this);
    }
}
