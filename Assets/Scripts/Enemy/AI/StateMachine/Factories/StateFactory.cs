using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class StateFactory
{

    public static State CreateState(EState state, GameObject npc, NavMeshAgent agent, Animator anim, Transform player, EnemyAudioManager enemyAudioManager)
    {
        switch (state)
        {
            case EState.PATROL:
                return new Patrol(npc, agent, anim, player, enemyAudioManager);
            case EState.PURSUE:
                return new Patrol(npc, agent, anim, player, enemyAudioManager);
            default:
                return new State(npc, agent, anim, player, enemyAudioManager);

        }
    }

    public static State CreateState(EState state, GameObject npc, NavMeshAgent agent, Animator anim, Transform player, EnemyAudioManager enemyAudioManager, Vector3 playerLastKnownPosition)
    {
        switch (state)
        {
            case EState.WANDER:
                return new Wander(npc, agent, anim, player, enemyAudioManager, playerLastKnownPosition);
            default:
                return new Wander(npc, agent, anim, player, enemyAudioManager, playerLastKnownPosition);

        }
    }

}
