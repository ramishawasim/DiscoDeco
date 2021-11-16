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


        // Debug.Log(Vector3.Distance(npc.transform.position, player.position));


        if (!IsPlayerBehind() || !CanSeePlayer() /*&& PlayerPrefs.GetInt("isHiding") == 0*/)
        {
            playerLastKnownPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        }

        agent.SetDestination(player.position);

        if (agent.hasPath)
        {
            if (Vector3.Distance(npc.transform.position, player.position) < 1f)
            {
                nextState = new Attack(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
            else if (IsFacingDoor() && IsDoorBlocked() && GetDistanceFromDoor() < 2f)
            {
                nextState = new Break(npc, agent, anim, player, name);
                stage = EVENT.EXIT;
            }
            else if ((!IsPlayerBehind() || !CanSeePlayer()) && player.tag != "Hide")
            {
                nextState = new Wander(npc, agent, anim, player, playerLastKnownPosition);
                stage = EVENT.EXIT;
            }
            else
            {
                nextState = new Patrol(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        // anim.SetBool("onWalk", false);
        // agent.isStopped = true;
        // agent.speed = 0;
        base.Exit();
    }
}
