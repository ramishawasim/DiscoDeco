using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach to Empty GameObject to set WayPoints for Enemy Movement
//MoveTowards waypoints IN ORDER
//Set Enemies that reset the level by attaching the "Enemies" tag to them
public class Waypoints : MonoBehaviour
{
    public GameObject[] waypoints;
    public float speed = 5f;

    int current = 0;
    float WPradius = 1;
	
	void Update () {
		if(Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            current++;
            if (current >= waypoints.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);

    }
}
