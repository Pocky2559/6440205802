using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VillagerGatheringCapacity : ScriptableObject
{
    public List<VillagerCapacityDetails> villagerCapacity;
}

[Serializable]
public class VillagerCapacityDetails
{
    public int woodCapacity;
    public int goldStoneCapacity;
    public int foodCapacity;
}
