using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateHealth : MonoBehaviour
{
    [SerializeField] private TMP_Text gateHealthDetail;
    [SerializeField] private UnitStat unitStat;
    [SerializeField] private UnitDatabaseSO unitDatabase;

    private void Awake()
    {
        unitStat = GameObject.FindGameObjectWithTag("PalisadeGate").GetComponent<UnitStat>();
    }

    private void Update()
    {
        if(unitStat.unitHP > 0)
        {
            gateHealthDetail.text = string.Format("{0} / {1}", (unitStat.unitHP).ToString(), (unitDatabase.unitDetails[10].HP).ToString());
        }
        else
        {
            gateHealthDetail.text = string.Format("{0} / {1}", "   0", (unitDatabase.unitDetails[10].HP).ToString());
        }
    }
}
