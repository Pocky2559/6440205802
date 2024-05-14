using UnityEngine;
using UnityEngine.EventSystems;

public class UnitClick : MonoBehaviour
{
    private Camera myCam;
    public GameObject groundMarker;
    public GameObject attackMarker;
    public GameObject seletedEnemy;
    public UnitSelection unitSelection;
    public float markerLifeTime;
    private float markerTime = 0f;
    public float markerOffset;

    public LayerMask clickable;
    public LayerMask ground;
    public LayerMask enemy;

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

        if (Input.GetMouseButtonDown(1) && unitSelection.unitSelected.Count > 0)
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground) )
            {
                Vector3 markerPosition = hit.point;
                markerPosition.y = markerPosition.y + markerOffset;
                groundMarker.transform.position = markerPosition;
                groundMarker.SetActive(true);
                attackMarker.SetActive(false);
                markerTime = Time.time;
            }
            
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, enemy))
            {
                Vector3 markerPosition = hit.transform.position;
                markerPosition.y = markerPosition.y + markerOffset;
                attackMarker.transform.position = markerPosition;
                seletedEnemy = hit.collider.gameObject;
                groundMarker.SetActive(false);
                attackMarker.SetActive(true);
                markerTime = Time.time;
            }
        }

        if((groundMarker.activeSelf == true || attackMarker.activeSelf == true) && Time.time > markerTime + markerLifeTime)
        {
            groundMarker.SetActive(false);
            attackMarker.SetActive(false);
            markerTime = Time.time;
        }

        if (attackMarker.activeSelf == true) //make attack marker follow selected enemy
        {
            Vector3 attackMarkerFollow = seletedEnemy.transform.position;
            attackMarkerFollow.y = attackMarkerFollow.y + markerOffset;
            attackMarker.transform.position = attackMarkerFollow;
        }

    }
}
