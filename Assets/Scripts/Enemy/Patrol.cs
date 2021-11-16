using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    WaypointManager waypointManager;
    Transform currentWaypoint;

    float passedTime = 0;

    private bool blocking;
    public Patrol(GameObject npc, NavMeshAgent agent, Animator anim, Transform player) : base(npc, agent, anim, player)
    {
        name = STATE.PATROL;
        agent.speed = 5;
        agent.isStopped = false;
        waypointManager = GameObject.FindGameObjectWithTag("WaypointManager").GetComponent<WaypointManager>();
        waypointManager.Init();
    }

    public override void Enter()
    {
        currentWaypoint = waypointManager.CalibrateAndGetNearestWaypoint(this.npc.transform);
        // TODO: animation enter
        base.Enter();
    }

    public override void Update()
    {

        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, anim, player);
            base.Exit();
        }

        if (IsPlayerBehind() && Random.Range(0f, 1f) < .10f)
        {
            nextState = new Pursue(npc, agent, anim, player);
            base.Exit();
        }

        if (agent.remainingDistance < 1)
        {
            currentWaypoint = waypointManager.GetNextWaypoint();
        }

        agent.SetDestination(currentWaypoint.transform.position);
        HandleDoor();
    }

    public override void Exit()
    {
        // TODO: Exit animation walking
        base.Exit();
    }

    private void HandleDoor()
    {
        RaycastHit hit;

        Debug.DrawRay(this.npc.transform.position + Vector3.up, this.npc.transform.TransformDirection(Vector3.forward) * 100, Color.yellow);

        if (Physics.Raycast(this.npc.transform.position, this.npc.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "Door")
            {
                Interactable door = hit.transform.gameObject.GetComponent<Interactable>();
                if (Vector3.Distance(this.npc.transform.transform.position, door.gameObject.transform.position) < 2f)
                {
                    door.openDoor();
                }

            }
        }
    }
}
