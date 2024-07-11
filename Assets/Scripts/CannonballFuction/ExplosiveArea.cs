using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Analytics;

public class ExplosiveArea : MonoBehaviour
{
    public UnitDatabaseSO unitDatabase;
    bool isExplode;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OttomanRecruit")
           || other.CompareTag("OttomanGunnerRecruit")
           || other.CompareTag("MeleeJanissary")
           || other.CompareTag("RangedJanissary")
           || other.CompareTag("OttomanCannon")
           && other != null
           && isExplode == false)
        {
           isExplode = true;
           DamageCalculation(other);
        }
    }

    private void DamageCalculation(Collider other)
    {
       UnitStat recieverStat = other.GetComponent<UnitStat>();
       recieverStat.unitHP = recieverStat.unitHP - (unitDatabase.unitDetails[4].damage - recieverStat.unitRangedArmor);

    }
}
