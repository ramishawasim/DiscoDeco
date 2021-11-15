using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    GameObject player;
    WaypointManager waypointManager;

    GameObject waypoint1;
    GameObject waypoint2;
    GameObject waypoint3;
    GameObject waypoint4;

    int currentWaypoint = 1;

    public float viewRadius;
    public float viewAngle;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        waypointManager = GameObject.FindGameObjectWithTag("WaypointManager").GetComponent<WaypointManager>();
        player = GameObject.FindGameObjectWithTag("Player");

        waypoint1 = GameObject.Find("WayPoint 1");
        waypoint2 = GameObject.Find("WayPoint 2");
        waypoint3 = GameObject.Find("WayPoint 3");
        waypoint4 = GameObject.Find("WayPoint 4");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject waypointToFollow = null;

        if (currentWaypoint == 1)
        {
            waypointToFollow = waypoint1;
        }
        else if (currentWaypoint == 2)
        {
            waypointToFollow = waypoint2;
        }
        else if (currentWaypoint == 3)
        {
            waypointToFollow = waypoint3;
        }
        else if (currentWaypoint == 4)
        {
            waypointToFollow = waypoint4;
        }

        agent.SetDestination(waypointToFollow.transform.position);

        if (Vector3.Distance(waypointToFollow.transform.position, this.transform.position) < 5)
        {
            currentWaypoint++;
        }

        if (currentWaypoint == 5)
        {
            currentWaypoint = 1;
        }
    }
}
