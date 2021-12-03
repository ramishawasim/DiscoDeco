using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Break : State
{
    private EState stateFrom;

    private float startedBreaking, delay;

    private Vector3 playerLastKnownPosition = Vector3.zero;

    public Break(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, EnemyAudioManager enemyAudioManager, EState stateFrom) : base(npc, agent, anim, player, enemyAudioManager)
    {
        name = EState.BREAK;
        this.stateFrom = stateFrom;
    }

    public Break(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, EnemyAudioManager enemyAudioManager, EState stateFrom, Vector3 playerLastKnownPosition) : base(npc, agent, anim, player, enemyAudioManager)
    {
        name = EState.BREAK;
        this.stateFrom = stateFrom;
        this.playerLastKnownPosition = playerLastKnownPosition;
    }

    public override void Enter()
    {
        anim.SetTrigger("onPunch");
        base.Enter();
        agent.isStopped = true;
        agent.speed = 0;
        startedBreaking = Time.time;
        delay = Random.Range(3f, 5f);
    }

    public override void Update()
    {
        base.Update();

        enemyAudioManager.PlayMooSound();

        PlayBreakingSound();

        if (Time.time - startedBreaking > delay)
        {
            BreakChairBlockingDoor();
            OpenDoor();
            if (playerLastKnownPosition == Vector3.zero)
            {
                nextState = StateFactory.CreateState(stateFrom, npc, agent, anim, player, enemyAudioManager);
            }
            else
            {
                nextState = StateFactory.CreateState(stateFrom, npc, agent, anim, player, enemyAudioManager, playerLastKnownPosition);
            }

            base.Exit();
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("onPunch");
        base.Exit();
    }
}
