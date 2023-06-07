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

        anim.SetTrigger("isShooting");
        agent.isStopped = true;
        //shoot.Play();
        base.Enter();
    }

    public override void Update()
    {

        Vector3 direction = player.position - npc.transform.position;
        direction.y = 0.0f;
        npc.transform.rotation =
            Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        if(Vector3.Distance(npc.transform.position, player.transform.position) < 1f)
        {
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
            nextState = new RunAway(npc, agent, anim, player, raft);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {

        anim.ResetTrigger("isShooting");
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
