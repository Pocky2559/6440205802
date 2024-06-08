using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStatus : MonoBehaviour //this script use to tell what upgrade you have already done.
{

    //Economic Building
    public bool isWoodGatheringSpeedUpgrade;
    public bool isWoodGatheringCapacityUpgrade;

    public bool isGoldStoneGatheringSpeedUpgrade;
    public bool isGoldStoneGatheringCapacityUpgrade;

    public bool isFoodGatheringSpeedUpgrade;
    public bool isFoodGatheringCapacityUpgrade;

    
    
    public Button woodGatheringSpeedButton;
    public Button woodGatheringCapacityButton;
    public Button goldAndStoneGatheringSpeedButton;
    public Button goldAndStoneGatheringCapacityButton;
    public Button foodGatheringSpeedButton;
    public Button foodGatheringCapacityButton;
    //

    //Town Center
    public bool isTownCenterUpgrade;
    public Button townCenterUpgradeButton;
    //

    //Barrack
    public bool isBarrackAttackUpgrade;
    public bool isBarrackDefenseUpgrade;
    public Button barrackUpgradeAttackButton;
    public Button barrackUpgradeDefenseButton;
    //

    //Artillery Shop
    public bool isArtilleryUpgrade;
    public Button artilleryUpgradeButton;
    //

    //Check Icons
    [SerializeField] private GameObject townCenterUpgradeCheckIcon;
    [SerializeField] private GameObject lumberUpgradeSpeedCheckIcon;
    [SerializeField] private GameObject lumberUpgradeCapacityCheckIcon;
    [SerializeField] private GameObject miningUpgradeSpeedCheckIcon;
    [SerializeField] private GameObject miningUpgradeCapacityCheckIcon;
    [SerializeField] private GameObject windMillUpgradeSpeedCheckIcon;
    [SerializeField] private GameObject windMillUpgradeCapacityCheckIcon;
    [SerializeField] private GameObject barrackUpgradeArmorCheckIcon;
    [SerializeField] private GameObject barrackUpgradeAttackCheckIcon;
    [SerializeField] private GameObject artilleryUpgradeCheckIcon;
 
    public ResourcesStatus resourcesStatus;
    public EconomicUpgradeDatabase economicUpgradeDatabase;
    public TownCenterUpgradeDatabase townCenterUpgradeDatabase;
    public BarrackUpgradeDatabase barrackUpgradeDatabase;
    public ArtilleryShopUpgradeDatabase artilleryShopUpgradeDatabase;
    public BuildingFunctionNotify buildingFunctionNotify;

    #region Economic Upgrade
    public void UpgradeWoodGatheringSpeed() // Click upgrade wood gathering speed
    {
        if(resourcesStatus.gold_Amount >= economicUpgradeDatabase.economicUpgrade[0].goldCostForUpgradeGatheringSpeed)
        {
            if(isWoodGatheringSpeedUpgrade == false)
            {
                isWoodGatheringSpeedUpgrade = true;
                resourcesStatus.gold_Amount = resourcesStatus.gold_Amount - economicUpgradeDatabase.economicUpgrade[0].goldCostForUpgradeGatheringSpeed;
                resourcesStatus.gold_Text.text = resourcesStatus.gold_Amount.ToString();
                woodGatheringSpeedButton.enabled = false;
                lumberUpgradeSpeedCheckIcon.SetActive(true);
            }
        }
        
        else if(resourcesStatus.gold_Amount < economicUpgradeDatabase.economicUpgrade[0].goldCostForUpgradeGatheringSpeed)
        {
            //Notify Not enough resources
            buildingFunctionNotify.NotifyNotEnoughResources();
        }
    }

    public void UpgradeWoodGatheringCapacity() // Click upgrade wood gathering capacity
    {
        if(resourcesStatus.gold_Amount >= economicUpgradeDatabase.economicUpgrade[0].goldCostForUpgradeGatheringCapacity)
        {
            if (isWoodGatheringCapacityUpgrade == false)
            {
                isWoodGatheringCapacityUpgrade = true;
                resourcesStatus.gold_Amount = resourcesStatus.gold_Amount - economicUpgradeDatabase.economicUpgrade[0].goldCostForUpgradeGatheringSpeed;
                resourcesStatus.gold_Text.text = resourcesStatus.gold_Amount.ToString();
                woodGatheringCapacityButton.enabled = false;
                lumberUpgradeCapacityCheckIcon.SetActive(true);
            }
        }

        else if (resourcesStatus.gold_Amount < economicUpgradeDatabase.economicUpgrade[0].goldCostForUpgradeGatheringCapacity)
        {
            //Notify Not enough resources
            buildingFunctionNotify.NotifyNotEnoughResources();
        }
    }

    public void UpgradeGoldStoneGatheringSpeed() // Click upgrade gold stone gathering speed
    {
        if (resourcesStatus.gold_Amount >= economicUpgradeDatabase.economicUpgrade[1].goldCostForUpgradeGatheringSpeed)
        {
            if (isGoldStoneGatheringSpeedUpgrade == false)
            {
                isGoldStoneGatheringSpeedUpgrade = true;
                resourcesStatus.gold_Amount = resourcesStatus.gold_Amount - economicUpgradeDatabase.economicUpgrade[1].goldCostForUpgradeGatheringSpeed;
                resourcesStatus.gold_Text.text = resourcesStatus.gold_Amount.ToString();
                goldAndStoneGatheringSpeedButton.enabled = false;
                miningUpgradeSpeedCheckIcon.SetActive(true);
            }          
        }

        else if (resourcesStatus.gold_Amount < economicUpgradeDatabase.economicUpgrade[1].goldCostForUpgradeGatheringSpeed)
        {
            //Notify Not enough resources
            buildingFunctionNotify.NotifyNotEnoughResources();
        }
    }

    public void UpgradeGoldStoneGatheringCapacity() // Click upgrade gold stone gathering capacity
    {
        if (resourcesStatus.gold_Amount >= economicUpgradeDatabase.economicUpgrade[1].goldCostForUpgradeGatheringCapacity)
        {
            if (isGoldStoneGatheringCapacityUpgrade == false)
            {
                isGoldStoneGatheringCapacityUpgrade = true;
                resourcesStatus.gold_Amount = resourcesStatus.gold_Amount - economicUpgradeDatabase.economicUpgrade[1].goldCostForUpgradeGatheringCapacity;
                resourcesStatus.gold_Text.text = resourcesStatus.gold_Amount.ToString();
                goldAndStoneGatheringCapacityButton.enabled = false;
                miningUpgradeCapacityCheckIcon.SetActive(true);
            }         
        }

        if (resourcesStatus.gold_Amount < economicUpgradeDatabase.economicUpgrade[1].goldCostForUpgradeGatheringCapacity)
        {
            //Notify Not enough resources
            buildingFunctionNotify.NotifyNotEnoughResources();
        }
    }

    public void UpgradeFoodGatheringSpeed() // Click upgrade food gathering speed
    {
        if (resourcesStatus.gold_Amount >= economicUpgradeDatabase.economicUpgrade[2].goldCostForUpgradeGatheringSpeed)
        {
            if (isFoodGatheringSpeedUpgrade == false)
            {
                isFoodGatheringSpeedUpgrade = true;
                resourcesStatus.gold_Amount = resourcesStatus.gold_Amount - economicUpgradeDatabase.economicUpgrade[2].goldCostForUpgradeGatheringSpeed;
                resourcesStatus.gold_Text.text = resourcesStatus.gold_Amount.ToString();
                foodGatheringSpeedButton.enabled = false;
                windMillUpgradeSpeedCheckIcon.SetActive(true);
            }
        }

        if (resourcesStatus.gold_Amount < economicUpgradeDatabase.economicUpgrade[2].goldCostForUpgradeGatheringSpeed)
        {
            //Notify Not enough resources
            buildingFunctionNotify.NotifyNotEnoughResources();
        }
    }

    public void UpgradeFoodGatheringCapacity() // Click upgrad food gathering capacity
    {
        if (resourcesStatus.gold_Amount >= economicUpgradeDatabase.economicUpgrade[2].goldCostForUpgradeGatheringCapacity)
        {
            if (isFoodGatheringCapacityUpgrade == false)
            {
                isFoodGatheringCapacityUpgrade = true;
                resourcesStatus.gold_Amount = resourcesStatus.gold_Amount - economicUpgradeDatabase.economicUpgrade[2].goldCostForUpgradeGatheringCapacity;
                resourcesStatus.gold_Text.text = resourcesStatus.gold_Amount.ToString();
                foodGatheringCapacityButton.enabled = false;
                windMillUpgradeCapacityCheckIcon.SetActive(true);
            }
        }

        if (resourcesStatus.gold_Amount < economicUpgradeDatabase.economicUpgrade[2].goldCostForUpgradeGatheringCapacity)
        {
            //Notify Not enough resources
            buildingFunctionNotify.NotifyNotEnoughResources();
        }
    }
    #endregion

    #region Town Center Upgrade
    public void UpgradeTownCenter()
    {
        if (resourcesStatus.gold_Amount >= townCenterUpgradeDatabase.townCenterUpgrades[0].upgradeCost)
        {
            if(isTownCenterUpgrade == false)
            {
                isTownCenterUpgrade = true;
                resourcesStatus.gold_Amount = resourcesStatus.gold_Amount - townCenterUpgradeDatabase.townCenterUpgrades[0].upgradeCost;
                resourcesStatus.gold_Text.text = resourcesStatus.gold_Amount.ToString();
                townCenterUpgradeButton.enabled = false;
                townCenterUpgradeCheckIcon.SetActive(true);
            }
        }

        else if (resourcesStatus.gold_Amount < townCenterUpgradeDatabase.townCenterUpgrades[0].upgradeCost)
        {
            //Notify Not enough resources
            buildingFunctionNotify.NotifyNotEnoughResources();
        }
    }
    #endregion

    #region Barrack Upgrade
    public void UpgradeBarrackAttackPower()
    {
        if (resourcesStatus.gold_Amount >= barrackUpgradeDatabase.upgradeAttackGoldCost)
        {
            if (isBarrackAttackUpgrade == false)
            {
                resourcesStatus.gold_Amount = resourcesStatus.gold_Amount - barrackUpgradeDatabase.upgradeAttackGoldCost;
                resourcesStatus.gold_Text.text = resourcesStatus.gold_Amount.ToString();
                isBarrackAttackUpgrade = true;
                barrackUpgradeAttackButton.enabled = false;
                barrackUpgradeAttackCheckIcon.SetActive(true);
            }
        }
        else if(resourcesStatus.gold_Amount < barrackUpgradeDatabase.upgradeAttackGoldCost)
        {
            //Notify Not enough resources
            buildingFunctionNotify.NotifyNotEnoughResources();
        }
    }

    public void UpgradeBarrackDefense()
    {
        if (resourcesStatus.gold_Amount >= barrackUpgradeDatabase.upgradeDefenseGoldCost)
        {
            if(isBarrackDefenseUpgrade == false)
            {
                resourcesStatus.gold_Amount = resourcesStatus.gold_Amount - barrackUpgradeDatabase.upgradeDefenseGoldCost;
                resourcesStatus.gold_Text.text = resourcesStatus.gold_Amount.ToString();
                isBarrackDefenseUpgrade = true;
                barrackUpgradeDefenseButton.enabled = false;
                barrackUpgradeArmorCheckIcon.SetActive(true);
            }
        }
        else if ((resourcesStatus.gold_Amount < barrackUpgradeDatabase.upgradeDefenseGoldCost))
        {
            //Notify Not enough resources
            buildingFunctionNotify.NotifyNotEnoughResources();
        }
    }
    #endregion

    #region Artillery Shop Upgrade
    public void UpgradeArtillery()
    {
        if(resourcesStatus.gold_Amount >= artilleryShopUpgradeDatabase.upgradeAttackCost)
        {
            if(isArtilleryUpgrade == false)
            {
               resourcesStatus.gold_Amount = resourcesStatus.gold_Amount - artilleryShopUpgradeDatabase.upgradeAttackCost;
               resourcesStatus.gold_Text.text = resourcesStatus.gold_Amount.ToString();
               isArtilleryUpgrade = true;
               artilleryUpgradeButton.enabled = false;
                artilleryUpgradeCheckIcon.SetActive(true);
            }
        }
        else if(resourcesStatus.gold_Amount < artilleryShopUpgradeDatabase.upgradeAttackCost)
        {
            //Notify Not enough resources
            buildingFunctionNotify.NotifyNotEnoughResources();
        }
    }
    #endregion
}
