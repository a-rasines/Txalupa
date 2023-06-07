using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{

    int currentIndex = -1;
    float seconds = 20f;
    public Patrol(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, GameObject raft)
        : base(_npc, _agent, _anim, _player, raft)
    {

        name = STATE.PATROL;
        agent.speed = 2.0f;
        agent.isStopped = false;

    }

    public override void Enter()
    {
        seconds = Random.Range((1*(npc.GetComponent<SharkBehaviour>().vida)*Random.Range(1,2)), 300);
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
        anim.Play("Armature|Swim_Calm");
        base.Enter();
    }

    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            if (currentIndex >= GameEnvironment.Singleton.Checkpoints.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
            agent.SetDestination(GameEnvironment.Singleton.Checkpoints[currentIndex].transform.position);
        }
        if (CanSeePlayer())
        {
            nextState = new AttackPlayer(npc, agent, anim, player, raft);
            stage = EVENT.EXIT;
        }
        if (seconds > 0)
        {
            seconds -= Time.deltaTime;
        }
        else 
        {
            nextState = new AttackShip(npc, agent, anim, player, raft);
            stage = EVENT.EXIT;
            seconds = Random.Range((1*(npc.GetComponent<SharkBehaviour>().vida)*Random.Range(1,2)), 120);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
