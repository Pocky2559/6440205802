using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildingCost : ScriptableObject
{
    public List<BuildingCostData> buildingCostsData;
}

[Serializable]
public class BuildingCostData
{
    public string buildingName;
    public int buildingID;
    public int woodRequire;
    public int stoneRequire;
}
