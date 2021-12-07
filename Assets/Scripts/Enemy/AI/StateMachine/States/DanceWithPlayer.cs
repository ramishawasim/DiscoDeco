using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DanceWithPlayer : State
{

    private float delay;
    private bool check;
    private float timePlayerStoppedDancing;

    public DanceWithPlayer(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, EnemyAudioManager enemyAudioManager) : base(npc, agent, anim, player, enemyAudioManager)
    {
        name = EState.DANCE_WITH_PLAYER;
    }

    public override void Enter()
    {
        base.Enter();
        anim.SetTrigger("onDance");
        delay = Random.Range(0.10f, 0.20f);
        check = false;
    }

    public override void Update()
    {
        base.Update();

        if (Vector3.Distance(npc.transform.position, player.position) < 1f)
        {
            nextState = new Attack(npc, agent, anim, player, enemyAudioManager);
            base.Exit();
        }

        if (PlayerPrefs.GetInt("isDancing") == 0)
        {
            if (!check)
            {
                check = true;
                timePlayerStoppedDancing = Time.time;
            }
            else if (check && Time.time - timePlayerStoppedDancing > delay)
            {
                nextState = new Pursue(npc, agent, anim, player, enemyAudioManager);
                base.Exit();
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetBool("onDance", false);
    }
}
