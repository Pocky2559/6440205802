using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringWaypointForTree : MonoBehaviour
{
    public Dictionary<GameObject, bool> waypoints;
    public List<GameObject> waypointsList;
    
    private void Start()
    {
        waypoints= new Dictionary<GameObject, bool>();

        for (int i = 0; i < waypointsList.Count; i++)
        {
            waypoints.Add(waypointsList[i], true);
            Debug.Log("Waypoint 0  = " + waypointsList[i].name + " = " + waypoints[waypointsList[i]]);
        }
    }

    public void WaypointStatus(GameObject waypointObject, bool isAvailable)
    {
        waypoints[waypointObject] = isAvailable;
        Debug.Log("Waypoint 0  = " + waypointObject.name + " = " + isAvailable);
    }
}
