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
    public Wander(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, Vector3 playerLastKnownPosition) : base(npc, agent, anim, player)
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

        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, anim, player);
            base.Exit();
        }

        if (IsFacingDoor() && IsDoorBlocked() && GetDistanceFromDoor() < 2f)
        {
            nextState = new Break(npc, agent, anim, player, name);
            stage = EVENT.EXIT;
        }

        if (timer >= timeOut)
        {
            nextState = new Patrol(npc, agent, anim, player);
            stage = EVENT.EXIT;
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

                // wanderTarget += new Vector3(Random.Range(-1f, 1f) * WanderJitter, 0, Random.Range(-1f, 1f) * WanderJitter);

                // wanderTarget.Normalize();
                // wanderTarget *= wanderRadius;

                // Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
                // Vector3 targetWorld = npc.transform.InverseTransformDirection(targetLocal);

                // Vector3 targetWorld = new Vector3(player.position.x + Random.Range(-1f, 1f), player.position.y, player.position.z + Random.Range(-3f, 3f));

                Vector3 targetWorld = player.position;

                // NavMeshPath navMeshPath = new NavMeshPath();

                // agent.CalculatePath(targetWorld, navMeshPath);

                // if (navMeshPath.status == NavMeshPathStatus.PathPartial)
                // {
                agent.SetDestination(targetWorld);
                // }
            }
        }
    }

    public override void Exit()
    {
        // anim.SetBool("onWalk", false);
        base.Exit();
    }
}
