using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public EState name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected State nextState;
    protected NavMeshAgent agent;
    public EnemyAudioManager enemyAudioManager;

    private FieldOfView fov;
    private BackFieldOfView bfov;
    private DoorStateHandler doorStateHandler;




    public State(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, EnemyAudioManager enemyAudioManager)
    {
        Debug.Log(name);
        this.npc = npc;
        this.agent = agent;
        this.anim = anim;
        this.player = player;
        this.enemyAudioManager = enemyAudioManager;

        fov = this.npc.GetComponent<FieldOfView>();
        bfov = this.npc.GetComponent<BackFieldOfView>();
        doorStateHandler = this.npc.GetComponent<DoorStateHandler>();

        // enemyAudioManager = this.npc.GetComponent<EnemyAudioManager>(); 
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update()
    {
        stage = EVENT.UPDATE;

        if (IsFacingDoor() && GetDistanceFromDoor() < 2f)
        {
            if (IsDoorBlocked())
            {
                switch (name)
                {
                    case EState.WANDER:
                        break;
                    default:
                        nextState = new Break(npc, agent, anim, player, enemyAudioManager, name);
                        Exit();
                        break;

                }
            }
            else
            {
                OpenDoor();
            }
        }
    }
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

    public bool IsFacingDoor()
    {
        return doorStateHandler.IsFacingDoor();
    }

    public bool IsDoorBlocked()
    {
        return doorStateHandler.IsDoorBlocked();
    }

    public void OpenDoor()
    {
        doorStateHandler.OpenDoor();
    }

    public void BreakChairBlockingDoor()
    {
        doorStateHandler.BreakChairBlockingDoor();
    }

    public float GetDistanceFromDoor()
    {
        return doorStateHandler.DistanceFromDoor();
    }
}
