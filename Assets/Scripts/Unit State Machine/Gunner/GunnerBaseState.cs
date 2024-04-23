using System.Collections;
using UnityEngine;

public abstract class GunnerBaseState 
{
    public abstract void EnterState(GunnerStateController gunner);
    public abstract void UpdateState(GunnerStateController gunner);
    public abstract void OnTriggerStay(GunnerStateController gunner, Collider coll);
    public abstract void OnTriggerExit(GunnerStateController gunner, Collider coll);
    public abstract void ExitState(GunnerStateController gunner);

}
