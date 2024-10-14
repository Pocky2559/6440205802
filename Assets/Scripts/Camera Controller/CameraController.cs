using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float borderThickness;
    [SerializeField] private float distance;
    public Vector3 targetPosition;
    public bool isPushingBack;
    public bool isStopPanning;

    [Header("Camera Zooming")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float targetFOV;
    [SerializeField] private float farestFOV;
    [SerializeField] private float closestFOV;
    [SerializeField] private float zoomingSpeed;
    [SerializeField] private int zoomingScale;

    private void Start()
    {
        targetPosition = transform.position;
        targetFOV = mainCamera.fieldOfView;
    }
    void FixedUpdate()
    {
        Vector3 direction = Vector3.zero;
        if ((Input.GetKey(KeyCode.W) || (Input.mousePosition.y >= Screen.height - borderThickness)) && isPushingBack == false && isStopPanning == false)
        {
            direction = direction + transform.forward;
        }
        if((Input.GetKey(KeyCode.S) || (Input.mousePosition.y <= borderThickness)) && isPushingBack == false && isStopPanning == false) 
        {
            direction = direction - transform.forward;
        }
        if((Input.GetKey(KeyCode.D) || (Input.mousePosition.x >= Screen.width - borderThickness)) && isPushingBack == false && isStopPanning == false)
        {
            direction= direction + transform.right;
        }
        if((Input.GetKey(KeyCode.A) || (Input.mousePosition.x <= borderThickness)) && isPushingBack == false && isStopPanning == false)
        {
            direction = direction - transform.right;
        }

        if (Input.mouseScrollDelta.y > 0 && mainCamera.fieldOfView >= closestFOV) //Zoom In
        {
            targetFOV = mainCamera.fieldOfView - zoomingScale;
        }

        if (Input.mouseScrollDelta.y < 0 && mainCamera.fieldOfView <= farestFOV) //Zoom Out
        {
            targetFOV = mainCamera.fieldOfView + zoomingScale;
        }

        if (direction != Vector3.zero && isPushingBack == false && isStopPanning == false) 
        {
            direction.Normalize(); 
            targetPosition = transform.position + direction * distance;
        }

        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, zoomingSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
