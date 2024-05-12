using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    void Start()
    {
        UnitSelection.Instance.unitList.Add(this.gameObject);
    }

    void OnDestroy()
    {
        UnitSelection.Instance.unitList.Remove(this.gameObject);
    }
}
