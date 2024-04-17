using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingUI : MonoBehaviour
{
    public Transform targetFloatingUI;
    public Vector3 uiOffsetPosition;

    private void Update()
    {
        Vector3 position = Camera.main.WorldToScreenPoint(targetFloatingUI.position + uiOffsetPosition);

        if(transform.position != position)
        {
            transform.position = position;
        }
    }
}
