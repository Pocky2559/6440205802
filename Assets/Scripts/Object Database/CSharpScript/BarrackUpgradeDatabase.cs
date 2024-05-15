using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class BarrackUpgradeDatabase : ScriptableObject
{
    public int upgradeAttackValue;
    public int upgradeDefenseValue;
    public int upgradeAttackGoldCost;
    public int upgradeDefenseGoldCost;
}



