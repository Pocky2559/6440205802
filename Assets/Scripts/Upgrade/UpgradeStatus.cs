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

    public ResourcesStatus resourcesStatus;
    public EconomicUpgradeDatabase economicUpgradeDatabase;
    public TownCenterUpgradeDatabase townCenterUpgradeDatabase;
    public BarrackUpgradeDatabase barrackUpgradeDatabase;
    public ArtilleryShopUpgradeDatabase artilleryShopUpgradeDatabase;
    private void Awake()
    {
        
    }

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
            }
            else
            {
                Debug.Log("You have already upgrade");
            }
        }
        
        if(resourcesStatus.gold_Amount < economicUpgradeDatabase.economicUpgrade[0].goldCostForUpgradeGatheringSpeed)
        {
            Debug.Log("Not Enough Gold");
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
            }
            else
            {
                Debug.Log("You have already upgrade");
            }
        }

        if (resourcesStatus.gold_Amount < economicUpgradeDatabase.economicUpgrade[0].goldCostForUpgradeGatheringCapacity)
        {
            Debug.Log("Not Enough Gold");
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
            }
            else
            {
                Debug.Log("You have already upgrade");
            }
        }

        if (resourcesStatus.gold_Amount < economicUpgradeDatabase.economicUpgrade[1].goldCostForUpgradeGatheringSpeed)
        {
            Debug.Log("Not Enough Gold");
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
            }
            else
            {
                Debug.Log("You have already upgrade");
            }
        }

        if (resourcesStatus.gold_Amount < economicUpgradeDatabase.economicUpgrade[1].goldCostForUpgradeGatheringCapacity)
        {
            Debug.Log("Not Enough Gold");
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
            }
            else
            {
                Debug.Log("You have already upgrade");
            }
        }

        if (resourcesStatus.gold_Amount < economicUpgradeDatabase.economicUpgrade[2].goldCostForUpgradeGatheringSpeed)
        {
            Debug.Log("Not Enough Gold");
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
            }
            else
            {
                Debug.Log("You have already upgrade");
            }
        }

        if (resourcesStatus.gold_Amount < economicUpgradeDatabase.economicUpgrade[2].goldCostForUpgradeGatheringCapacity)
        {
            Debug.Log("Not Enough Gold");
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
            }

            else
            {
                Debug.Log("You have already upgrade");
            }
        }

        if (resourcesStatus.gold_Amount < townCenterUpgradeDatabase.townCenterUpgrades[0].upgradeCost)
        {
            Debug.Log("Not enough gold");
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
            }
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
            }
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
            }
        }
    }
    #endregion
}
