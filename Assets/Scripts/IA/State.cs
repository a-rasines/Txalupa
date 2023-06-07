using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State { 
    public enum STATE
    { 
        IDLE,
        PATROL,
        ATTACKSHIP,
        ATTACKPLAYER,
        RUNAWAY
    };

    public enum EVENT
    {
        ENTER,
        UPDATE,
        EXIT
    };

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected State nextState;
    protected GameObject raft;
    protected UnityEngine.AI.NavMeshAgent agent;

    float atackDist = 2.0f;

    public State(GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Animator _anim, Transform _player, GameObject _raft)
    {
        npc = _npc;
        agent = _agent;
        anim = _anim;
        player = _player;
        raft = _raft;
        stage = EVENT.ENTER;
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
        if (player.localPosition.y < -0.3f)
        {

            return true;
        }

        return false;
    }

    public bool CanAttackPlayer()
    {

        Vector3 direction = player.position - npc.transform.position;
        if (direction.magnitude < atackDist)
        {
            return true;
        }

        return false;
    }
    public bool IsSharkDead()
    {
        if (npc.GetComponent<SharkBehaviour>().vida <= 0)
        {
            return true;
        }
        return false;
    }
}