using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float borderThickness;
    public float cameraCurrentSpeed;
    public float distance;
    public Vector3 targetPosition;
    public bool isPushingBack;

    private void Start()
    {
        targetPosition = transform.position;
    }
    void LateUpdate()
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W) || (Input.mousePosition.y >= Screen.height - borderThickness) && isPushingBack == false)
        {
            direction = direction + transform.forward;
        }
        if(Input.GetKey(KeyCode.S) || (Input.mousePosition.y <= borderThickness) && isPushingBack == false) 
        {
            direction = direction - transform.forward;
        }
        if(Input.GetKey(KeyCode.D) || (Input.mousePosition.x >= Screen.width - borderThickness) && isPushingBack == false)
        {
            direction= direction + transform.right;
        }
        if(Input.GetKey(KeyCode.A) || (Input.mousePosition.x <= borderThickness) && isPushingBack == false)
        {
            direction = direction - transform.right;
        }

        if (direction != Vector3.zero && isPushingBack == false) 
        {
            direction.Normalize(); 
            targetPosition = transform.position + direction * distance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
