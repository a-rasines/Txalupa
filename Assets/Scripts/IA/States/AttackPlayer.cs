using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AttackPlayer : State
{
    float rotationSpeed = 2.0f;
    //AudioSource shoot;
    private bool attacked = false;
    private float attackTime = -1;
    public AttackPlayer(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, GameObject _raft)
        : base(_npc, _agent, _anim, _player, _raft)
    {
        name = STATE.ATTACKPLAYER;
        //shoot = _npc.GetComponent<AudioSource>();
        ColliderEvents events = npc.GetComponentInChildren<ColliderEvents>();
        events.CollisionEnterEvent += Colision;
    }
    private Vector3 runawayPos;
    public override void Enter()
    {
        anim.SetTrigger("Swim_Regular");
        agent.speed = 6;
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);
        runawayPos = player.transform.position - npc.transform.position;
        //shoot.Play();
        base.Enter();
    }

    public override void Update()
    {
        if (Vector3.Distance(npc.transform.position, player.transform.position) < 2.5f && attackTime == -1) {
            anim.SetTrigger("Bite");
            attackTime = 0;
        }
        else if(attackTime >= 2f) {
            player.GetComponent<PlayerBehaivour>().Da�o(25);
            nextState = new RunAway(npc, agent, anim, player, raft, runawayPos);
            stage = EVENT.EXIT;
        }
        if (!CanSeePlayer())
        {
            nextState = new Patrol(npc, agent, anim, player, raft);
            //shoot.Stop();
            stage = EVENT.EXIT;
        }

        if (attacked)
        {
            anim.SetTrigger("TakeDamage");
            npc.GetComponent<SharkBehaviour>().Da�o();
            if (IsSharkDead())
            {
                nextState = new Idle(npc, agent, anim, player, raft);
                stage = EVENT.EXIT;
            }
            else
            {
                nextState = new RunAway(npc, agent, anim, player, raft, runawayPos);
                anim.SetTrigger("Swim_Regular");
                stage = EVENT.EXIT;
            }
        }
        if (attackTime >= 0)
            attackTime += Time.deltaTime;
    }

    public override void Exit()
    {
        anim.ResetTrigger("Swim_Calm");
        base.Exit();
    }
    public void Colision(Collision col)
    {
        if (col.gameObject.tag == "Arma")
        {
            attacked = true;
        }
    }
}
