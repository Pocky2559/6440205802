using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffArea : MonoBehaviour
{
    public List<GameObject> buffedUnits;
    public UnitDatabaseSO unitDatabase;
    public int buffedArmor;
    public int buffedDamage;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Villager")
            ||other.CompareTag("Gunner")
            || other.CompareTag("Landsknecht")
            || other.CompareTag("Kartouwe"))
        {
            if (buffedUnits != null && buffedUnits.Contains(other.gameObject) == false )
            {
                buffedUnits.Add(other.gameObject);
                // Buff Stats
                UnitStat buff = other.GetComponentInParent<UnitStat>();
                buff.unitMeleeArmor = buff.unitMeleeArmor + buffedArmor;
                buff.unitRangedArmor = buff.unitRangedArmor + buffedArmor;
                buff.unitDamage = buff.unitDamage + buffedDamage;
            }
            else
            {
                // Nothing Happen
            }
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (buffedUnits.Contains(other.gameObject))
        {
            buffedUnits.Remove(other.gameObject);
            UnitStat buff = other.GetComponentInParent<UnitStat>();
            buff.unitMeleeArmor = buff.unitMeleeArmor - buffedArmor;
            buff.unitRangedArmor = buff.unitRangedArmor - buffedArmor;
            buff.unitDamage = buff.unitDamage - buffedDamage;
        }
    }
}
