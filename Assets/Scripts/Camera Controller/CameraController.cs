using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    public float cameraCurrentSpeed;
    void Start()
    {  
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || (Input.mousePosition.y < 1080 && Input.mousePosition.y > 1020))
        {
            transform.position = transform.position + transform.forward * speed;
            cameraCurrentSpeed = speed;
        }
        if(Input.GetKey(KeyCode.S) || (Input.mousePosition.y > 0 && Input.mousePosition.y < 60)) 
        {
            transform.position = transform.position + (transform.forward * -1) * speed;
            cameraCurrentSpeed = speed;
        }
        if(Input.GetKey(KeyCode.D) || (Input.mousePosition.x < Screen.width && Input.mousePosition.x > 1855))
        {
            transform.position = transform.position + transform.right * speed;
            cameraCurrentSpeed = speed;
        }
        if(Input.GetKey(KeyCode.A) || (Input.mousePosition.x > 0 && Input.mousePosition.x < 60))
        {
            transform.position = transform.position + (transform.right * -1 ) * speed;
            cameraCurrentSpeed = speed;
        }

        // Pan cameraDemo with mouse //
        //    // Left
        //  if(Input.mousePosition.x > 0 && Input.mousePosition.x < 60) 
        //    {
        //     transform.position = transform.position + (transform.right * -1) * speed;
             
        //    }

        //    // Right
        //  if(Input.mousePosition. x < Screen.width && Input.mousePosition.x > 1855)
        //    {
        //     transform.position = transform.position + transform.right * speed;
             
        //    }

        //    // Up
        //  if(Input.mousePosition.y < 1080 && Input.mousePosition.y > 1020)
        //    {
        //     transform.position = transform.position + transform.forward* speed;
        //    }

        //    // Down
        //  if(Input.mousePosition.y > 0 && Input.mousePosition.y < 60)
        //{
        //    transform.position = transform.position + (transform.forward * -1) * speed;
        //}
        
    }
}
