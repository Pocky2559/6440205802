using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 0.09f;
    void Start()
    {  
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + transform.forward * speed;
        }
        if(Input.GetKey(KeyCode.S)) 
        {
            transform.position = transform.position + (transform.forward * -1) * speed;
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + transform.right * speed;
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + (transform.right * -1 ) * speed;
        }

        // Pan camera with mouse //
            // Left
          if(Input.mousePosition.x > 0 && Input.mousePosition.x < 60) 
            {
             transform.position = transform.position + (transform.right * -1) * speed;
             
            }

            // Right
          if(Input.mousePosition. x < Screen.width && Input.mousePosition.x > 1855)
            {
             transform.position = transform.position + transform.right * speed;
             
            }

            // Up
          if(Input.mousePosition.y < 1080 && Input.mousePosition.y > 1020)
            {
             transform.position = transform.position + transform.forward* speed;
            }

            // Down
          if(Input.mousePosition.y > 0 && Input.mousePosition.y < 60)
        {
            transform.position = transform.position + (transform.forward * -1) * speed;
        }
        
    }
}
