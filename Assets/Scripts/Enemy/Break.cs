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

    public Break(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, EState stateFrom) : base(npc, agent, anim, player)
    {
        name = EState.BREAK;
        this.stateFrom = stateFrom;
        endTime = Random.Range(3f, 5f);
    }

    public Break(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, EState stateFrom, Vector3 playerLastKnownPosition) : base(npc, agent, anim, player)
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
        Debug.Log("Breaking");

        timer += Time.deltaTime;

        if (timer >= endTime)
        {
            BreakChairBlockingDoor();
            OpenDoor();
            if (playerLastKnownPosition == Vector3.zero)
            {
                nextState = StateFactory.CreateState(stateFrom, npc, agent, anim, player);
            }
            else
            {
                nextState = StateFactory.CreateState(stateFrom, npc, agent, anim, player, playerLastKnownPosition);
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
