using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    private float cooldown;
    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, GameObject raft)
        : base(_npc, _agent, _anim, _player, raft)
    {
        cooldown = 60f;
        name = STATE.IDLE;
    }
    public override void Enter()
    {
        anim.SetTrigger("Dead");
        base.Enter();
    }

    public override void Update()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0f)
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
