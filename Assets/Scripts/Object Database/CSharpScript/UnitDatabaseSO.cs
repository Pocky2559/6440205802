using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UnitDatabaseSO : ScriptableObject
{
    public List<UnitDetails> unitDetails;
}

[Serializable]
public class UnitDetails
{
    public int unitID;
    public GameObject unitPrefab;
    public string unitName;
    public int HP;
    public int damage;
    public int meleeArmor;
    public int rangedArmor;
    public float attackSpeed;
    public int foodCost;
    public int goldCost;
    public float trainingTime;
}
