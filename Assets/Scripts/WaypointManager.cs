using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
// public struct Link
// {
//     public enum direction { UNI, BI };
//     public GameObject node1;
//     public GameObject node2;
//     public direction dir;
// }

public class WaypointManager : MonoBehaviour
{
    // public GameObject[] waypoints;
    // public Link[] links;
    // public Graph graph;

    // private IsDebug isDebugging;

    private GameObject[] waypoints;
    private int currentWaypointIndex;

    public void Init()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        currentWaypointIndex = -1;
    }


    public Transform GetNextWaypoint()
    {
        currentWaypointIndex++;

        if (waypoints.Length == currentWaypointIndex)
        {
            currentWaypointIndex = 0;
        }

        return waypoints[currentWaypointIndex].transform;
    }

    public Transform CalibrateAndGetNearestWaypoint(Transform currentTransform)
    {
        float closestDistance = Mathf.Infinity;
        int closestWaypointIndex = 0;
        Transform closestWaypoint = waypoints[closestWaypointIndex].transform;


        for (int i = 0; i < waypoints.Length; i++)
        {
            float distance = Vector3.Distance(waypoints[i].transform.position, currentTransform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestWaypoint = waypoints[i].transform;
                closestWaypointIndex = i;
            }
        }

        currentWaypointIndex = closestWaypointIndex;

        return closestWaypoint;
    }
}
