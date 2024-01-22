using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectTrigger : MonoBehaviour
{
    //public Collider checkCollider;
    //PlacementSystem placementSystem;
    [SerializeField] PreviewSystem previewSystem;
    private void Start()
    {
           
        GameObject gameobj = GameObject.FindGameObjectWithTag("PreviewSystem");
        previewSystem = gameobj.GetComponent<PreviewSystem>();
        
    }
    private void OnTriggerEnter(Collider other) // ignore Detection Area (Box Collider)
    {
        if (other.CompareTag("Detection Area"))
        {
            previewSystem.CanPlaceObjectStatus();
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
            //Debug.Log(other);
            previewSystem.CanPlaceObjectStatus();      
    }
    private void OnTriggerStay(Collider other)
    {
        if(!other.CompareTag("Detection Area"))
        {
            Debug.Log(other);
            previewSystem.CannotPlaceObjectStatus();
        }
    }
    
}
