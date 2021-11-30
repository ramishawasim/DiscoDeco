using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : State
{
    private Vector3 playerLastKnownPosition;
    private bool reachedLastKnownLocation;

    private Vector3 wanderTarget = Vector3.zero;

    float wanderRadius = 10;
    float wanderDistance = 20;
    float WanderJitter = 1;

    float timeOut;
    float timer;
    public Wander(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, EnemyAudioManager enemyAudioManager, Vector3 playerLastKnownPosition) : base(npc, agent, anim, player, enemyAudioManager)
    {
        name = EState.WANDER;
        this.playerLastKnownPosition = playerLastKnownPosition;

        timeOut = Random.Range(2f, 5f);
    }

    public override void Enter()
    {
        anim.SetBool("onWalk", true);
        base.Enter();
        agent.isStopped = false;
        agent.speed = 6.0f;
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("Wandering");

        if (IsFacingDoor() && GetDistanceFromDoor() < 2f)
        {
            if (IsDoorBlocked())
            {
                nextState = new Break(npc, agent, anim, player, enemyAudioManager, name);
                Exit();
            }
            else
            {
                OpenDoor();
            }
        }

        enemyAudioManager.PlayMooSound();

        if (player.tag == "Hide")
        {
            nextState = new Patrol(npc, agent, anim, player, enemyAudioManager);
            base.Exit();
        }
        else
        {
            if (CanSeePlayer())
            {
                nextState = new Pursue(npc, agent, anim, player, enemyAudioManager);
                base.Exit();
            }

            if (IsFacingDoor() && GetDistanceFromDoor() < 2f && IsDoorBlocked())
            {
                nextState = new Break(npc, agent, anim, player, enemyAudioManager, name, playerLastKnownPosition);
                Exit();
            }


            if (timer >= timeOut)
            {
                nextState = new Patrol(npc, agent, anim, player, enemyAudioManager);
                base.Exit();
            }

            if (!CanSeePlayer())
            {
                if (!reachedLastKnownLocation)
                {
                    agent.SetDestination(playerLastKnownPosition);

                    if (Vector3.Distance(npc.transform.position, playerLastKnownPosition) < 5f)
                    {
                        reachedLastKnownLocation = true;
                    }
                }
                else
                {
                    timer += Time.deltaTime;

                    Vector3 targetWorld = player.position;

                    agent.SetDestination(targetWorld);
                }
            }
        }
    }

    public override void Exit()
    {
        // anim.SetBool("onWalk", false);
        agent.speed = 0;
        agent.isStopped = true;
        base.Exit();
    }
}
