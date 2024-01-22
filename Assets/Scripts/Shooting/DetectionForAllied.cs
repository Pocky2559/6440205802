using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectionForAllied : MonoBehaviour
{
    private bool isDetection;

    public GameObject target;
   
    public Transform Allied;
    public UnitMovement unitMovement;

    public UnitStat attackSpeed;

    public ShootRay shootRay;
    public bool isSelectedEnemyTarget;
    public Transform targetEnemyPosition;

    private void Start()
    {
        attackSpeed = GetComponentInParent<UnitStat>();
        StartCoroutine(ShootWithCooldown());
    }

    private IEnumerator ShootWithCooldown()
    {
            while(true)
            {
                yield return new WaitForSeconds(attackSpeed.unitAttackSpeed);
                if (isDetection == true || isSelectedEnemyTarget == true)
                {
                    shootRay.StartShooting();
                    Debug.Log("Shooting is cool down");
                }
            }
    }

    private void Update()
    {
        if(target != null)
        {
            if (isDetection == true || isSelectedEnemyTarget == true)
            {
                Allied.LookAt(target.transform);
            }

            if (target.activeSelf == false)
            {
                isSelectedEnemyTarget = false;
                isDetection = false;
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy") && isSelectedEnemyTarget == false)
        {
            Debug.Log("Enemy Spotted");
            isDetection = true;
            target = other.gameObject;
        }

        if(isSelectedEnemyTarget== true) 
        {
            try
            {
                target = targetEnemyPosition.gameObject;
            }
            catch // after selected gameObject enemy was destroy isSelectedEnemyTarget will be false to cancle selection
            { 
                isSelectedEnemyTarget = false;
                Debug.Log("isSelectedEnemyTarget = false");
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        isDetection= false;
        target = null;
    }
}
