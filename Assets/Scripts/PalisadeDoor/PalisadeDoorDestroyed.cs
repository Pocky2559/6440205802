using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalisadeDoorDestroyed : MonoBehaviour
{
    [SerializeField] private UnitStat unitStat;

    private void Awake()
    {
        unitStat = GameObject.FindGameObjectWithTag("PalisadeGate").GetComponent<UnitStat>();
    }
    private void Update()
    {
       if(unitStat.unitHP <= 0)
       {
          Destroy(unitStat.transform.gameObject);
       }    
    }
}
