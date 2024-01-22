using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionForEnemy : MonoBehaviour
{
    private bool isDetection;
    private GameObject target;
    public Transform Enemy;

    // Update is called once per frame
    void Update()
    {
        if (isDetection == true)
        {
            Enemy.LookAt(target.transform);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Allied"))
        {
            isDetection = true;
            target = other.gameObject;
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        isDetection = false;
        target = null;
    }
}
