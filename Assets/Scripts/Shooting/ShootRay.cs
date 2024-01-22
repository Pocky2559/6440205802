using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRay : MonoBehaviour // Gun
{
    public GameObject Gun;
    public LayerMask targetLayerMask;
    public GameObject shootParticle;
    public GameObject hitParticle;
    public UnitStat attackerStat,recieverStat;
    private int attackDamage;
    private int recieverRangedArmor;
    
    public void StartShooting()
    {
        RaycastHit hit;

        if (Physics.Raycast(Gun.transform.position, -Gun.transform.right, out hit, 9.8f, targetLayerMask)) // we don't want ray to hit everything. 
        {
            Debug.DrawRay(Gun.transform.position, -Gun.transform.right * hit.distance, Color.red, 0.9f);

            GameObject shootPTC = Instantiate(shootParticle, Gun.transform.position,Quaternion.identity);
            GameObject hitPTC = Instantiate(hitParticle, hit.collider.transform.position, Quaternion.identity);
            Destroy(shootPTC, 1);
            Destroy(hitPTC, 2);

            AttackDamage();
            RecieveDamage(hit);
        }
    }

    public void AttackDamage()
    {
        attackerStat = GetComponentInParent<UnitStat>();
        attackDamage = attackerStat.unitDamage;
    }

    public void RecieveDamage(RaycastHit hit)
    {
        recieverStat = hit.collider.GetComponentInParent<UnitStat>();
        recieverStat.unitHP = recieverStat.unitHP - (attackDamage - recieverStat.unitRangedArmor); // decrease attack damage with ranged armor value
    }
}
