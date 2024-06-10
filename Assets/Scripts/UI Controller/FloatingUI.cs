using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingUI : MonoBehaviour
{
    public Transform targetFloatingUI;
    public Vector3 uiOffsetPosition;
    private Vector3 position;

    private void Update()
    {
        if (targetFloatingUI!= null)
        {
           position = Camera.main.WorldToScreenPoint(targetFloatingUI.position + uiOffsetPosition);
        }

        if (transform.position != position)
        {
            transform.position = position;
        }
    }
}
