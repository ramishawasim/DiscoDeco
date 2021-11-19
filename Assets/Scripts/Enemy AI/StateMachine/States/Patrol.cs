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
        name = EState.PATROL;
        agent.speed = 5;
        agent.isStopped = false;
        waypointManager = GameObject.FindGameObjectWithTag("WaypointManager").GetComponent<WaypointManager>();
        waypointManager.Init();
    }

    public override void Enter()
    {
        currentWaypoint = waypointManager.CalibrateAndGetNearestWaypoint(this.npc.transform);
        anim.SetBool("onWalk", true);
        base.Enter();
    }

    public override void Update()
    {
        base.Update();


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

        if (IsFacingDoor() && IsDoorBlocked())
        {
            nextState = new Break(npc, agent, anim, player, name);
            base.Exit();
        }

        if (IsFacingDoor() && IsDoorBlocked() && GetDistanceFromDoor() < 2f)
        {
            nextState = new Break(npc, agent, anim, player, name);
            base.Exit();
        }


        if (agent.remainingDistance < 1)
        {
            currentWaypoint = waypointManager.GetNextWaypoint();
        }

        agent.SetDestination(currentWaypoint.transform.position);
    }

    public override void Exit()
    {
        // anim.SetBool("onWalk", false);
        base.Exit();
    }
}
