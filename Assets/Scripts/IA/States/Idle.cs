using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, GameObject raft)
        : base(_npc, _agent, _anim, _player, raft)
    {

        name = STATE.IDLE;
    }
    public override void Enter()
    {

        anim.SetTrigger("isIdle");
        base.Enter();
    }

    public override void Update()
    {

        if (CanSeePlayer())
        {

            nextState = new AttackPlayer(npc, agent, anim, player, raft);
            stage = EVENT.EXIT;
        }
        else if (Random.Range(0, 100) < 20) //20% of the time 
        {
            nextState = new AttackShip(npc, agent, anim, player, raft);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {

        anim.ResetTrigger("isIdle");
        base.Exit();
    }
}
