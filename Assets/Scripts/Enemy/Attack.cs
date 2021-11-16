using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    public Attack(GameObject npc, NavMeshAgent agent, Animator anim, Transform player) : base(npc, agent, anim, player)
    {
        anim.SetTrigger("onSlash");
        name = EState.ATTACK;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        player.gameObject.GetComponent<Reset>().Die();

        nextState = new CowDance(npc, agent, anim, player);

        base.Exit();
    }

    public override void Exit()
    {
        anim.ResetTrigger("onSlash");
        base.Exit();
    }
}
