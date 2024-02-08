using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OttomanGunnerRecruitBaseState 
{
    public abstract void EnterState(OttomanGunnerRecruitStateController otmGunner);
    public abstract void UpdateState(OttomanGunnerRecruitStateController otmGunner);
    public abstract void OnTriggerStay(OttomanGunnerRecruitStateController otmGunner, Collider coll);
    public abstract void OnTriggerExit(OttomanGunnerRecruitStateController otmGunner, Collider coll);

}
