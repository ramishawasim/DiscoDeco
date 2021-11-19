using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class StateFactory
{

    public static State CreateState(EState state, GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
    {
        switch (state)
        {
            case EState.PATROL:
                return new Patrol(npc, agent, anim, player);
            case EState.PURSUE:
                return new Patrol(npc, agent, anim, player);
            default:
                return new State(npc, agent, anim, player);

        }
    }

    public static State CreateState(EState state, GameObject npc, NavMeshAgent agent, Animator anim, Transform player, Vector3 playerLastKnownPosition)
    {
        switch (state)
        {
            case EState.WANDER:
                return new Wander(npc, agent, anim, player, playerLastKnownPosition);
            default:
                return new Wander(npc, agent, anim, player, playerLastKnownPosition);

        }
    }

}
