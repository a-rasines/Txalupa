using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAway : State
{

    private Vector3 exitPos;
    public RunAway(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, GameObject _raft, Vector3 exitPos)
        : base(_npc, _agent, _anim, _player, _raft)
    {
        this.exitPos = exitPos;
        name = STATE.RUNAWAY;
    }

    public override void Enter()
    {
        anim.SetTrigger("Swim_Regular");
        agent.isStopped = false;
        agent.speed = 6;
        agent.SetDestination(exitPos);
        base.Enter();
    }

    public override void Update()
    {
        if (agent.remainingDistance < 1.0f)
        {
            nextState = new Patrol(npc, agent, anim, player, raft);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
