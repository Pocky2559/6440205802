using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TownCenterUpgradeDatabase : ScriptableObject
{
    public List<TownCenterUpgradeInfo> townCenterUpgrades;
}

[Serializable]
public class TownCenterUpgradeInfo
{
    public string upgradeType;
    public string upgradeInfo;
    public int reduceTrainingTime;
    public int upgradeCost;
}
