using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class UnitStat : MonoBehaviour
{
    public UnitDatabaseSO unitDatabase;
    public List<UnitDetails> statList;
    public int unitDatabaseIndex;

    public int unitHP;
    public string unitName;
    public int unitDamage;
    public float unitAttackSpeed;
    public int unitMeleeArmor;
    public int unitRangedArmor;


    private void Start()
    {
        statList = unitDatabase.unitDetails;
        AssignDataForThisUnit();
    }
    private void AssignDataForThisUnit()
    {
        // Player's units
        if (gameObject.CompareTag("Villager"))
        {
            unitDatabaseIndex = 0;
            unitHP = statList[unitDatabaseIndex].HP;
            unitDamage = statList[unitDatabaseIndex].damage;
            unitName = statList[unitDatabaseIndex].unitName;
            unitAttackSpeed = statList[unitDatabaseIndex].attackSpeed;
            unitMeleeArmor = statList[unitDatabaseIndex].meleeArmor;
            unitRangedArmor = statList[unitDatabaseIndex].rangedArmor;
        }

        if (gameObject.CompareTag("Gunner"))
        {
           unitDatabaseIndex = 1;
           unitHP = statList[unitDatabaseIndex].HP;
           unitDamage = statList[unitDatabaseIndex].damage;
           unitName = statList[unitDatabaseIndex].unitName;
           unitAttackSpeed = statList[unitDatabaseIndex].attackSpeed;
           unitMeleeArmor = statList[unitDatabaseIndex].meleeArmor;
           unitRangedArmor = statList[unitDatabaseIndex].rangedArmor;
        }

        if (gameObject.CompareTag("Landsknecht"))
        {
            unitDatabaseIndex = 2;
            unitHP = statList[unitDatabaseIndex].HP;
            unitDamage = statList[unitDatabaseIndex].damage;
            unitName = statList[unitDatabaseIndex].unitName;
            unitAttackSpeed = statList[unitDatabaseIndex].attackSpeed;
            unitMeleeArmor = statList[unitDatabaseIndex].meleeArmor;
            unitRangedArmor = statList[unitDatabaseIndex].rangedArmor;
        }

        if (gameObject.CompareTag("Landsknecht"))
        {
            unitDatabaseIndex = 2;
            unitHP = statList[unitDatabaseIndex].HP;
            unitDamage = statList[unitDatabaseIndex].damage;
            unitName = statList[unitDatabaseIndex].unitName;
            unitAttackSpeed = statList[unitDatabaseIndex].attackSpeed;
            unitMeleeArmor = statList[unitDatabaseIndex].meleeArmor;
            unitRangedArmor = statList[unitDatabaseIndex].rangedArmor;
        }

        if (gameObject.CompareTag("Captain"))
        {
            unitDatabaseIndex = 3;
            unitHP = statList[unitDatabaseIndex].HP;
            unitDamage = statList[unitDatabaseIndex].damage;
            unitName = statList[unitDatabaseIndex].unitName;
            unitAttackSpeed = statList[unitDatabaseIndex].attackSpeed;
            unitMeleeArmor = statList[unitDatabaseIndex].meleeArmor;
            unitRangedArmor = statList[unitDatabaseIndex].rangedArmor;
        }

        if (gameObject.CompareTag("Kartouwe"))
        {
            unitDatabaseIndex = 4;
            unitHP = statList[unitDatabaseIndex].HP;
            unitDamage = statList[unitDatabaseIndex].damage;
            unitName = statList[unitDatabaseIndex].unitName;
            unitAttackSpeed = statList[unitDatabaseIndex].attackSpeed;
            unitMeleeArmor = statList[unitDatabaseIndex].meleeArmor;
            unitRangedArmor = statList[unitDatabaseIndex].rangedArmor;
        }


        //Enemy's units
        if (gameObject.CompareTag("OttomanRecruit"))
        {
           unitDatabaseIndex = 5;
           unitHP = statList[unitDatabaseIndex].HP;
           unitDamage = statList[unitDatabaseIndex].damage;
           unitName = statList[unitDatabaseIndex].unitName;
           unitAttackSpeed = statList[unitDatabaseIndex].attackSpeed;
           unitMeleeArmor = statList[unitDatabaseIndex].meleeArmor;
           unitRangedArmor = statList[unitDatabaseIndex].rangedArmor;
        }

        if (gameObject.CompareTag("OttomanGunnerRecruit"))
        {
            unitDatabaseIndex = 6;
            unitHP = statList[unitDatabaseIndex].HP;
            unitDamage = statList[unitDatabaseIndex].damage;
            unitName = statList[unitDatabaseIndex].unitName;
            unitAttackSpeed = statList[unitDatabaseIndex].attackSpeed;
            unitMeleeArmor = statList[unitDatabaseIndex].meleeArmor;
            unitRangedArmor = statList[unitDatabaseIndex].rangedArmor;
        }

        if (gameObject.CompareTag("MeleeJanissary"))
        {
            unitDatabaseIndex = 7;
            unitHP = statList[unitDatabaseIndex].HP;
            unitDamage = statList[unitDatabaseIndex].damage;
            unitName = statList[unitDatabaseIndex].unitName;
            unitAttackSpeed = statList[unitDatabaseIndex].attackSpeed;
            unitMeleeArmor = statList[unitDatabaseIndex].meleeArmor;
            unitRangedArmor = statList[unitDatabaseIndex].rangedArmor;
        }


        if (gameObject.CompareTag("RangedJanissary"))
        {
            unitDatabaseIndex = 8;
            unitHP = statList[unitDatabaseIndex].HP;
            unitDamage = statList[unitDatabaseIndex].damage;
            unitName = statList[unitDatabaseIndex].unitName;
            unitAttackSpeed = statList[unitDatabaseIndex].attackSpeed;
            unitMeleeArmor = statList[unitDatabaseIndex].meleeArmor;
            unitRangedArmor = statList[unitDatabaseIndex].rangedArmor;
        }


        if (gameObject.CompareTag("OttomanCannon"))
        {
            unitDatabaseIndex = 9;
            unitHP = statList[unitDatabaseIndex].HP;
            unitDamage = statList[unitDatabaseIndex].damage;
            unitName = statList[unitDatabaseIndex].unitName;
            unitAttackSpeed = statList[unitDatabaseIndex].attackSpeed;
            unitMeleeArmor = statList[unitDatabaseIndex].meleeArmor;
            unitRangedArmor = statList[unitDatabaseIndex].rangedArmor;
        }


        //Building
        if (gameObject.CompareTag("PalisadeGate"))
        {
            unitDatabaseIndex = 10;
            unitHP = statList[unitDatabaseIndex].HP;
            unitDamage = statList[unitDatabaseIndex].damage;
            unitName = statList[unitDatabaseIndex].unitName;
            unitAttackSpeed = statList[unitDatabaseIndex].attackSpeed;
            unitMeleeArmor = statList[unitDatabaseIndex].meleeArmor;
            unitRangedArmor = statList[unitDatabaseIndex].rangedArmor;
        }


        
    }

    private void Update()
    {
        UnitDeath();
    }

    private void UnitDeath()
    {
        if (unitHP <= 1)
        {
            Destroy(gameObject);
        }
        
   
    }
}
