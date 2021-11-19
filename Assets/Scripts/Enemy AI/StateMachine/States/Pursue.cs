using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pursue : State
{
    private Vector3 playerLastKnownPosition;
    public Pursue(GameObject npc, NavMeshAgent agent, Animator anim, Transform player) : base(npc, agent, anim, player)
    {
        name = EState.PURSUE;
        agent.speed = 6.75f;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        anim.SetBool("onWalk", true);
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("Pursing");

        if (!IsPlayerBehind() || !CanSeePlayer())
        {
            playerLastKnownPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        }

        agent.SetDestination(player.position);

        if (player.tag == "Hide")
        {
            Debug.Log("ok");
            nextState = new Patrol(npc, agent, anim, player);
            base.Exit();
        }
        else if (agent.hasPath)
        {
            if (Vector3.Distance(npc.transform.position, player.position) < 1f)
            {
                nextState = new Attack(npc, agent, anim, player);
                base.Exit();
            }
            else if (IsFacingDoor() && IsDoorBlocked() && GetDistanceFromDoor() < 2f)
            {
                nextState = new Break(npc, agent, anim, player, name);
                base.Exit();
            }
            else if ((!IsPlayerBehind() && !CanSeePlayer()))
            {
                Debug.Log("jump");
                nextState = new Wander(npc, agent, anim, player, playerLastKnownPosition);
                base.Exit();
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
