using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Break : State
{
    private EState stateFrom;

    private float timer;
    private float endTime;

    private Vector3 playerLastKnownPosition = Vector3.zero;

    public Break(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, EnemyAudioManager enemyAudioManager, EState stateFrom) : base(npc, agent, anim, player, enemyAudioManager)
    {
        name = EState.BREAK;
        this.stateFrom = stateFrom;
        endTime = Random.Range(3f, 5f);
    }

    public Break(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, EnemyAudioManager enemyAudioManager, EState stateFrom, Vector3 playerLastKnownPosition) : base(npc, agent, anim, player, enemyAudioManager)
    {
        name = EState.BREAK;
        this.stateFrom = stateFrom;
        endTime = Random.Range(3f, 5f);
        this.playerLastKnownPosition = playerLastKnownPosition;
    }

    public override void Enter()
    {
        anim.SetTrigger("onPunch");
        base.Enter();
        agent.isStopped = true;
        agent.speed = 0;
    }

    public override void Update()
    {
        base.Update();

        Debug.Log("Breaking");

        enemyAudioManager.PlayMooSound();

        timer += Time.deltaTime;

        if (timer >= endTime)
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
