using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Start : State
{
    private float timeOut;
    private float timer;

    public Start(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, EnemyAudioManager enemyAudioManager) : base(npc, agent, anim, player, enemyAudioManager)
    {
        name = EState.START;
        timeOut = Random.Range(8f, 10f);
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        timer += Time.deltaTime;

        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, anim, player, enemyAudioManager);
            base.Exit();
        }

        if (timer > timeOut)
        {
            nextState = new Patrol(npc, agent, anim, player, enemyAudioManager);
            base.Exit();
        }
    }

    public override void Exit()
    {

    }
}
