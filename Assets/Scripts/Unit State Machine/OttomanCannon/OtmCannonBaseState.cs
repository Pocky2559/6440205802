using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OtmCannonBaseState
{
    public abstract void EnterState(OtmCannonStateController otmCannon);
    public abstract void UpdateState(OtmCannonStateController otmCannon);
    public abstract void OnTriggerStay(OtmCannonStateController otmCannon, Collider coll);
    public abstract void OnTriggerExit(OtmCannonStateController otmCannon, Collider coll);
}
