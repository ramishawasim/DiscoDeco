using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaitBeforePursue : State
{

    private float waitTime;
    private float timeEnteredState;

    public WaitBeforePursue(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, EnemyAudioManager enemyAudioManager) : base(npc, agent, anim, player, enemyAudioManager)
    {
        name = EState.WAIT_BEFORE_PURSUE;
        waitTime = Random.Range(1f, 2f);
        timeEnteredState = Time.time;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (Time.time - timeEnteredState > waitTime)
        {
            nextState = new Pursue(npc, agent, anim, player, enemyAudioManager);
            base.Exit();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
