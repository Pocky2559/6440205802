using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveAreaEnemyCannonball : MonoBehaviour
{
    public UnitDatabaseSO unitDatabase;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Villager")
            || other.CompareTag("Landsknecht")
            || other.CompareTag("Gunner")
            || other.CompareTag("Captain")
            || other.CompareTag("Kartouwe")
            || other.CompareTag("PalisadeGate")
            && other != null)
        {
            DamageCalculation(other);
        }
    }

    private void DamageCalculation(Collider other)
    {
        UnitStat recieverStat = other.GetComponent<UnitStat>();
        recieverStat.unitHP = recieverStat.unitHP - (unitDatabase.unitDetails[4].damage - recieverStat.unitRangedArmor);

    }
}
