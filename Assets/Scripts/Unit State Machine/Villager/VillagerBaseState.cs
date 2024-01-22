using System.Collections;
using UnityEngine;

public abstract class VillagerBaseState
{
    public abstract void EnterState(VillagerStateController villager);
    public abstract void UpdateState(VillagerStateController villager);
}
