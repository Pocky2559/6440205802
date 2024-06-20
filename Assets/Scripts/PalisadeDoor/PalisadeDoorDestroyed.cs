using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalisadeDoorDestroyed : MonoBehaviour
{
    [SerializeField] private UnitStat unitStat;
    [SerializeField] private GateWallExplode gateWallExplode;

    private void Awake()
    {
        unitStat = GameObject.FindGameObjectWithTag("PalisadeGate").GetComponent<UnitStat>();
        gateWallExplode = GameObject.FindGameObjectWithTag("ParticleController").GetComponent<GateWallExplode>();
    }
    private void Update()
    {
       if(unitStat.unitHP <= 0)
       {
          Destroy(unitStat.transform.gameObject);
          gateWallExplode.StartPlayParticle(gameObject.transform.position);
       }    
    }
}
