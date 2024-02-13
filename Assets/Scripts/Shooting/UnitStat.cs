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
    public CapturePointByEnemy capturePointByEnemy;
    public HouseList population;

    public int unitHP;
    public string unitName;
    public int unitDamage;
    public float unitAttackSpeed;
    public int unitMeleeArmor;
    public int unitRangedArmor;
    public int unitPopulation;

    private void Awake()
    {
        statList = unitDatabase.unitDetails;
        capturePointByEnemy = GameObject.FindGameObjectWithTag("CapturedPoint").GetComponent<CapturePointByEnemy>();
        population = GameObject.FindGameObjectWithTag("PopulationController").GetComponent<HouseList>();
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
            unitPopulation = statList[unitDatabaseIndex].population;    
            
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
           unitPopulation = statList[unitDatabaseIndex].population;
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
            unitPopulation = statList[unitDatabaseIndex].population;
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
            unitPopulation = statList[unitDatabaseIndex].population;
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
            unitPopulation = statList[unitDatabaseIndex].population;
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
            unitPopulation = statList[unitDatabaseIndex].population;
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
           unitPopulation = statList[unitDatabaseIndex].population;
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
            unitPopulation = statList[unitDatabaseIndex].population;
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
            unitPopulation = statList[unitDatabaseIndex].population;
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
            unitPopulation = statList[unitDatabaseIndex].population;
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
            unitPopulation = statList[unitDatabaseIndex].population;
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
            unitPopulation = statList[unitDatabaseIndex].population;
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
            if(unitDatabaseIndex == 5    
               || unitDatabaseIndex == 6
               || unitDatabaseIndex == 7
               || unitDatabaseIndex == 8)  // Check this becauser when Enemy was killed the the area of capture point
                                           // it will be deleted immediatly and OnTriggerWExit won't detect
                                           // So we need to make it check that this enemy was trigger the OnTriggerExit and stop count down the time 
            {
                GameObject TargetObject = unitDatabase.unitDetails[unitDatabaseIndex].unitPrefab;
                Collider colliderOfGameObject = TargetObject.GetComponent<Collider>();
                capturePointByEnemy.OnTriggerExit(colliderOfGameObject);
                Destroy(gameObject);
            }
            else
            {
              population.PopulationChanges(-1 * unitPopulation);
              Destroy(gameObject);
            }
            
        }


        
   
    }
}
