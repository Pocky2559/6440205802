using System.Collections;
using UnityEngine;

public abstract class OttomanRecruitBaseState 
{
    public abstract void EnterState(OttomanRecruitStateController otmRecruit);
    public abstract void UpdaterState(OttomanRecruitStateController otmRecruit);
    public abstract void OnTriggerStay(OttomanRecruitStateController otmRecruit, Collider coll);
    public abstract void OnTriggerExit(OttomanRecruitStateController otmRecruit, Collider coll);
}
