using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kartaune_IdelState : KartauneBaseState
{
    public override void EnterState(KartauneStateController kartaune)
    {
        //Kartaune Enter Idel State
        kartaune.transform.parent.rotation = kartaune.originRotation;
    }
    public override void UpdaterState(KartauneStateController kartaune)
    {
        //Kartaune is Idleing
        Debug.Log("Kartaune Idleing");
    }
    public override void OnTriggerStay(KartauneStateController kartaune, Collider coll)
    {
        if(coll.CompareTag("OttomanRecruit")
           || coll.CompareTag("OttomanGunnerRecruit")
           || coll.CompareTag("MeleeJanissary")
           || coll.CompareTag("RangedJanissary")
           || coll.CompareTag("OttomanCannon"))
        {
            kartaune.targetEnemy = coll.gameObject;
            kartaune.SwitchState(kartaune.kartaune_ShootingState);
        }
    }
    public override void OnTriggerExit(KartauneStateController kartaune, Collider coll)
    {
        // Debug
    }
}
