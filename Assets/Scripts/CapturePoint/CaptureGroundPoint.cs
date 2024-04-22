using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureGroundPoint : MonoBehaviour
{
    public Dictionary<GameObject, bool> waypoints;
    public List<GameObject> waypointsList;
    private void Start()
    {
        waypoints = new Dictionary<GameObject, bool>();

        for (int i = 0; i < waypointsList.Count; i++)
        {
            waypoints.Add(waypointsList[i], true);
        }
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("OttomanCannon") == null)
        {
            foreach(GameObject waypoint in waypointsList)
            {
                waypoints[waypoint] = true;
            }
        }
    }

    public void WaypointStatus(GameObject waypointObject, bool isAvailable)
    {
        waypoints[waypointObject] = isAvailable;
    }
}
