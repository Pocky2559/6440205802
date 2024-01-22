using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTurbine : MonoBehaviour
{
    public Vector3 rotation;
    private void Update()
    {
        this.transform.Rotate(1 * Time.deltaTime * rotation);
    }
}
