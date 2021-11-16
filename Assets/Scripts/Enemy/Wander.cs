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

        timeOut = Random.Range(5f, 10f);
    }

    public override void Enter()
    {
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

                Vector3 targetWorld = new Vector3(player.position.x + Random.Range(-1f, 1f), player.position.y, player.position.z + Random.Range(-1f, 1f));

                NavMeshPath navMeshPath = new NavMeshPath();

                agent.CalculatePath(targetWorld, navMeshPath);

                if (navMeshPath.status == NavMeshPathStatus.PathPartial)
                {
                    agent.SetDestination(targetWorld);
                }
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
