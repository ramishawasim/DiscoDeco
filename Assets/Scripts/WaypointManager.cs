using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Link
{
    public enum direction { UNI, BI };
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}

public class WaypointManager : MonoBehaviour
{
    public GameObject[] waypoints;
    public Link[] links;
    public Graph graph;

    private IsDebug isDebugging;


    // Start is called before the first frame update
    void Start()
    {

        // isDebugging = GameObject.FindGameObjectWithTag("Debug").GetComponent<IsDebug>();

        graph = new Graph();

        if (waypoints.Length > 0)
        {
            foreach (GameObject waypoint in waypoints)
            {
                graph.AddNode(waypoint);
            }

            foreach (Link link in links)
            {
                graph.AddEdge(link.node1, link.node2);
                graph.AddEdge(link.node2, link.node1);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (isDebugging.getState())
        // {
        graph.debugDraw();
        // }
    }
}
