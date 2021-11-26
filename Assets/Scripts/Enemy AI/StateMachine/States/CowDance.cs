using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CowDance : State
{
    public CowDance(GameObject npc, NavMeshAgent agent, Animator anim, Transform player) : base(npc, agent, anim, player)
    {
        name = EState.DANCE;
    }

    public override void Enter()
    {
        Debug.Log("dancing cow start");
        // anim.SetBool("onDance",true);
        anim.SetTrigger("onDance");
        base.Enter();
    }

    public override void Update()
    {
        Debug.Log("dancing cow");
        base.Update();

        // player.gameObject.GetComponent<Reset>().Die();

        // nextState = new Patrol(npc, agent, anim, player);
        
        // base.Exit();
    }

    public override void Exit()
    {
        anim.SetBool("onDance",false);
        base.Exit();
    }
}
