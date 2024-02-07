using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LandsknetchBaseState
{ 
    public abstract void EnterState(LandsknetchStateController landsknetch);
    public abstract void UpdaterState(LandsknetchStateController landsknetch);
    public abstract void OnTriggerStay(LandsknetchStateController landsknetch, Collider coll);
    public abstract void OnTriggerExit(LandsknetchStateController landsknetch, Collider coll);
}
