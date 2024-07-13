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
    [SerializeField] private float distance;
    Vector3 pushBackPosition;

    private void Start()
    {
        pushBackPosition = cameraController.transform.position;
    }
    private void Update()
    {
        Vector3 cameraPosition = mainCamera.transform.position;

        if(top.bounds.Contains(cameraPosition))
        {
            pushBackPosition = cameraController.transform.position + (transform.forward * -1) * distance;
            cameraController.isPushingBack = true;
            cameraController.targetPosition = pushBackPosition;
        }
        
        

        else if(bottom.bounds.Contains(cameraPosition))
        {
            pushBackPosition = cameraController.transform.position + transform.forward * distance;
            cameraController.isPushingBack = true;
            cameraController.targetPosition = pushBackPosition;
        }
       


        else if(left.bounds.Contains(cameraPosition)) 
        {
            pushBackPosition = cameraController.transform.position + transform.right * distance;
            cameraController.isPushingBack = true;
            cameraController.targetPosition = pushBackPosition;
        }
       


        else if (right.bounds.Contains(cameraPosition))
        {
            pushBackPosition = cameraController.transform.position + (transform.right * -1) * distance;
            cameraController.isPushingBack = true;
            cameraController.targetPosition = pushBackPosition;
        }

        else
        {
            cameraController.isPushingBack = false;
        }
    }
}
