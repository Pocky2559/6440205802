using UnityEngine;
using UnityEngine.EventSystems;

public class UnitClick : MonoBehaviour
{
    private Camera myCam;
    //public GameObject groundMarker;

    public LayerMask clickable;
    public LayerMask ground;
    void Start()
    {
        myCam = Camera.main;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, clickable) )
            {
                Debug.Log(hit.collider.gameObject);
                //if we hit a clickable object
                //normal click and shift click
                if(Input.GetKey(KeyCode.LeftShift) )
                {
                    //Shift clicked
                    UnitSelection.Instance.ShiftClickSelect(hit.collider.gameObject);
                }
                else
                {
                    //normal clicked
                    UnitSelection.Instance.ClickSelect(hit.collider.gameObject);
                }
            }
            else
            {
                //if we didn't && we're not shift clicking
                if(!Input.GetKey(KeyCode.LeftShift))
                {
                    if (EventSystem.current.IsPointerOverGameObject()) // If we click on UI, we don't want it to deselect unit.
                    {
                        return;
                    }
                    else 
                    {
                        UnitSelection.Instance.DeselectAll();
                    }
                }
               
            }
        }

        /*if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {

                Vector3 markerPosition = hit.point;
                markerPosition.y = 1;
                groundMarker.transform.position = markerPosition;
                groundMarker.SetActive(false);
                groundMarker.SetActive(true);
                if(Time.time == 2)
                {
                    groundMarker.SetActive(false);
                }
            }
        }*/
    }
}
