using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{

    int currentIndex = -1;

    public Patrol(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, GameObject raft)
        : base(_npc, _agent, _anim, _player, raft)
    {

        name = STATE.PATROL;
        agent.speed = 2.0f;
        agent.isStopped = false;

    }

    public override void Enter()
    {

        float lastDistance = Mathf.Infinity;

        for (int i = 0; i < GameEnvironment.Singleton.Checkpoints.Count; ++i)
        {

            GameObject thisWP = GameEnvironment.Singleton.Checkpoints[i];
            float distance = Vector3.Distance(npc.transform.position, thisWP.transform.position);
            if (distance < lastDistance)
            {

                currentIndex = i - 1;
                lastDistance = distance;
            }
        }
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
        anim.SetTrigger("isWalking");
        base.Enter();
    }

    public override void Update()
    {

        if (CanSeePlayer())
        {

            nextState = new AttackPlayer(npc, agent, anim, player, raft);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {

        anim.ResetTrigger("isWalking");
        base.Exit();
    }
}
