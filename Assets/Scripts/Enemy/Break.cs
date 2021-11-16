using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Break : State
{
    private EState stateFrom;

    private float timer;
    private float endTime;

    public Break(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, EState stateFrom) : base(npc, agent, anim, player)
    {
        name = EState.BREAK;
        this.stateFrom = stateFrom;
        endTime = Random.Range(3f, 5f);
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
        Debug.Log("Breaking");

        timer += Time.deltaTime;

        if (timer >= endTime)
        {
            BreakChairBlockingDoor();
            OpenDoor();
            nextState = StateFactory.CreateState(stateFrom, npc, agent, anim, player);
            base.Exit();
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("onPunch");
        base.Exit();
    }
}
