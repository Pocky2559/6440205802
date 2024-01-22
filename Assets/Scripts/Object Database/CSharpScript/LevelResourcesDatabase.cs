using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelResourcesDatabase : ScriptableObject
{
    public List<LevelData> levelData;
}

[Serializable]
public class LevelData
{
    public string levelName;
    public int foodAmount;
    public int woodAmount;
    public int goldAmount;
    public int stoneAmount;
}
