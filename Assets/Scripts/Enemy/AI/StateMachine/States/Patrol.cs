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
    public Patrol(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, EnemyAudioManager enemyAudioManager) : base(npc, agent, anim, player, enemyAudioManager)
    {
        name = EState.PATROL;
        waypointManager = GameObject.FindGameObjectWithTag("WaypointManager").GetComponent<WaypointManager>();
        waypointManager.Init();
    }

    public override void Enter()
    {
        currentWaypoint = waypointManager.CalibrateAndGetNearestWaypoint(this.npc.transform);
        agent.speed = 5;
        agent.isStopped = false;
        anim.SetBool("onWalk", true);
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        enemyAudioManager.PlayMooSound();

        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, anim, player, enemyAudioManager);
            base.Exit();
        }

        if (IsPlayerBehind() && Random.Range(0f, 1f) < .10f)
        {
            nextState = new WaitBeforePursue(npc, agent, anim, player, enemyAudioManager);
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
        agent.speed = 0;
        agent.isStopped = true;
        base.Exit();
    }
}
