using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DanceWithPlayer : State
{

    private float delay;
    private float timeStartedDancing;

    public DanceWithPlayer(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, EnemyAudioManager enemyAudioManager) : base(npc, agent, anim, player, enemyAudioManager)
    {
        name = EState.DANCE_WITH_PLAYER;
    }

    public override void Enter()
    {
        base.Enter();
        anim.SetTrigger("onDance");
        delay = Random.Range(0.10f, 0.15f);
        timeStartedDancing = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (PlayerPrefs.GetInt("isDancing") == 0 && Time.time - timeStartedDancing > delay)
        {
            nextState = new Pursue(npc, agent, anim, player, enemyAudioManager);
            base.Exit();
        }
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetBool("onDance", false);
    }
}
