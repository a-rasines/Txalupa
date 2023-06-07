using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AttackPlayer : State
{
    float rotationSpeed = 2.0f;
    //AudioSource shoot;
    private bool attacked = false;
    public AttackPlayer(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, GameObject _raft)
        : base(_npc, _agent, _anim, _player, _raft)
    {
        name = STATE.ATTACKPLAYER;
        //shoot = _npc.GetComponent<AudioSource>();
        ColliderEvents events = npc.GetComponent<ColliderEvents>();
        events.CollisionEnterEvent += Colision;
    }

    public override void Enter()
    {
        anim.SetTrigger("Swim_Regular");
        agent.isStopped = true;
        //shoot.Play();
        base.Enter();
    }

    public override void Update()
    {
        float directionx = player.transform.position.x - npc.transform.parent.position.x;
        float directionz = player.transform.position.z - npc.transform.parent.position.z;
        Vector3 direction = new Vector3(directionx, npc.transform.position.y, directionz);
        npc.transform.parent.rotation = Quaternion.Slerp(npc.transform.parent.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        if(Vector3.Distance(npc.transform.position, player.transform.position) < 1f)
        {
            anim.SetTrigger("Bite");
            player.GetComponent<PlayerBehaivour>().Daño(25);
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
            npc.GetComponent<SharkBehaviour>().Daño();
            if (IsSharkDead())
            {
                nextState = new Idle(npc, agent, anim, player, raft);
                stage = EVENT.EXIT;
            }
            else
            {
                nextState = new RunAway(npc, agent, anim, player, raft);
                anim.SetTrigger("Swim_Regular");
                stage = EVENT.EXIT;
            }
        }
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
