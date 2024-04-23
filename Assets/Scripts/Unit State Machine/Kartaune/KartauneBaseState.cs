using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KartauneBaseState 
{
    public abstract void EnterState(KartauneStateController kartaune);
    public abstract void UpdaterState(KartauneStateController kartaune);
    public abstract void OnTriggerStay(KartauneStateController kartaune, Collider coll);
    public abstract void OnTriggerExit(KartauneStateController kartaune, Collider coll);
    public abstract void ExitState(KartauneStateController kartaune);
}
