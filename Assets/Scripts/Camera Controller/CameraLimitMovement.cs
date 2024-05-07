using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimitMovement : MonoBehaviour
{
    public CameraController cameraController;
    public GameObject mainCamera;
    public Collider top;
    public Collider bottom;
    public Collider left;
    public Collider right;

    private void Update()
    {
        Vector3 cameraPosition = mainCamera.transform.position;

        if(top.bounds.Contains(cameraPosition))
        {
           mainCamera.transform.position += new Vector3(0,0,-cameraController.cameraCurrentSpeed); //the value should be same as speed of camera
        }

        if(bottom.bounds.Contains(cameraPosition))
        {
           mainCamera.transform.position += new Vector3(0, 0, cameraController.cameraCurrentSpeed); //the value should be same as speed of camera
        }

        if(left.bounds.Contains(cameraPosition)) 
        {
            mainCamera.transform.position += new Vector3(-cameraController.cameraCurrentSpeed, 0, 0);
        }

        if (right.bounds.Contains(cameraPosition))
        {
            mainCamera.transform.position += new Vector3(cameraController.cameraCurrentSpeed, 0, 0);
        }

    }
}
