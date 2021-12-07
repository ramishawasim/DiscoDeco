using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pursue : State
{
    private Vector3 playerLastKnownPosition;
    public Pursue(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, EnemyAudioManager enemyAudioManager) : base(npc, agent, anim, player, enemyAudioManager)
    {
        name = EState.PURSUE;
    }

    public override void Enter()
    {
        anim.SetBool("onWalk", true);
        agent.speed = 6.4f;
        agent.isStopped = false;
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        enemyAudioManager.PlayMooSound();

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

        if (!IsPlayerBehind() || !CanSeePlayer())
        {
            playerLastKnownPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        }

        if (CanDance())
        {
            nextState = new DanceWithPlayer(npc, agent, anim, player, enemyAudioManager);
            base.Exit();
        }

        agent.SetDestination(player.position);

        if (player.tag == "Hide")
        {
            nextState = new Patrol(npc, agent, anim, player, enemyAudioManager);
            base.Exit();
        }
        else if (agent.hasPath)
        {
            if (Vector3.Distance(npc.transform.position, player.position) < 1f)
            {
                nextState = new Attack(npc, agent, anim, player, enemyAudioManager);
                base.Exit();
            }
            else if ((!IsPlayerBehind() && !CanSeePlayer()))
            {
                nextState = new Wander(npc, agent, anim, player, enemyAudioManager, playerLastKnownPosition);
                base.Exit();
            }
        }
    }

    public override void Exit()
    {
        anim.SetBool("onWalk", false);
        agent.speed = 0;
        agent.isStopped = true;
        base.Exit();
    }
}
