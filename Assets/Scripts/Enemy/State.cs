using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State : MonoBehaviour
{
    public enum STATE
    {
        IDLE, PATROL, PURSUE
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected State nextState;
    protected NavMeshAgent agent;

    private FieldOfView fov;
    private BackFieldOfView bfov;

    float visDist = 10f;
    float visAngle = 30f;
    float shootDist = 7f;

    public State(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
    {
        this.npc = npc;
        this.agent = agent;
        this.anim = anim;
        this.player = player;

        fov = this.npc.GetComponent<FieldOfView>();
        bfov = this.npc.GetComponent<BackFieldOfView>();
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }

        return this;
    }

    public bool CanSeePlayer()
    {
        return fov.canSeePlayer;
    }

    public bool IsPlayerBehind()
    {
        return bfov.canSeePlayer;
    }
}
