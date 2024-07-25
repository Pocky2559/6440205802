using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EconomicUpgradeDatabase : ScriptableObject
{
    public List<EconomicUpgradeInfo> economicUpgrade;
}

[Serializable]
public class EconomicUpgradeInfo
{
    public string upgradeType;
    public float increaseGatheringSpeed;
    public int increaseGatheringCapacity;
    public int goldCostForUpgradeGatheringSpeed;
    public int goldCostForUpgradeGatheringCapacity;
}
