using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    public RectTransform greenHealthBar;
    public UnitStat unitStat;
    private float unitFullHP;
    private float unitCurrentHP;
    private float valueInRectPerHP;
    private float valueOfCalculatedRect;
    [SerializeField] private GameObject hideShowTargetGameObject;
    [SerializeField] private float rectRightSize;

    private void Start()
    {
        unitStat = GetComponentInParent<UnitStat>();
        unitFullHP = unitStat.unitHP;
        valueInRectPerHP = rectRightSize / unitFullHP;
    }

    public void Update()
    { 
        if(unitStat.unitHP == unitFullHP)
        {
           hideShowTargetGameObject.SetActive(false);
        }
        else
        {
           hideShowTargetGameObject.SetActive(true);
           valueOfCalculatedRect = rectRightSize - (unitStat.unitHP * valueInRectPerHP);
           greenHealthBar.offsetMax = new Vector2(-(valueOfCalculatedRect), 0);
        }
    }
}
