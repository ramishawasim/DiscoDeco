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

    public float viewRadius;
    public float viewAngle;

    private Vector3 currentWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        // agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        // waypointManager = GameObject.FindGameObjectWithTag("WaypointManager").GetComponent<WaypointManager>();
        // waypointManager.Init();
        // player = GameObject.FindGameObjectWithTag("Player");
        // currentWaypoint = waypointManager.GetCurrentWaypointLocation();

        // waypoint1 = GameObject.Find("WayPoint 1");
        // waypoint2 = GameObject.Find("WayPoint 2");
        // waypoint3 = GameObject.Find("WayPoint 3");
        // waypoint4 = GameObject.Find("WayPoint 4");
    }

    // Update is called once per frame
    void Update()
    {


        // agent.SetDestination(currentWaypoint);

        // if (Vector3.Distance(this.transform.position, currentWaypoint) < 5f)
        // {
        //     waypointManager.NextWaypoint();
        //     currentWaypoint = waypointManager.GetCurrentWaypointLocation();
        // }

        // IsFacingDoor();


        // agent.SetDestination(player.transform.position);

        // GameObject waypointToFollow = null;

        // if (currentWaypoint == 1)
        // {
        //     waypointToFollow = waypoint1;
        // }
        // else if (currentWaypoint == 2)
        // {
        //     waypointToFollow = waypoint2;
        // }
        // else if (currentWaypoint == 3)
        // {
        //     waypointToFollow = waypoint3;
        // }
        // else if (currentWaypoint == 4)
        // {
        //     waypointToFollow = waypoint4;
        // }

        // agent.SetDestination(waypointToFollow.transform.position);

        // if (Vector3.Distance(waypointToFollow.transform.position, this.transform.position) < 5)
        // {
        //     currentWaypoint++;
        // }

        // if (currentWaypoint == 5)
        // {
        //     currentWaypoint = 1;
        // }
    }

    void IsFacingDoor()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward) * 100, Color.yellow);

        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "Door")
            {
                Interactable door = hit.transform.gameObject.GetComponent<Interactable>();
                if (Vector3.Distance(transform.transform.position, door.gameObject.transform.position) < 2f)
                {
                    door.openDoor();
                }

            }
        }

        // return false;
    }

}
