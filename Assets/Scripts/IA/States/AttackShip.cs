using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackShip : State
{

    float rotationSpeed = 2.0f;
    //AudioSource attack;
    TrozosBalsa objetivo;
    private bool damageFloor = true;
    private float cooldown = 5f;
    private bool attacked = false;
    private Vector3 originalRotation;
    public AttackShip(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, GameObject _raft)
        : base(_npc, _agent, _anim, _player, _raft)
    {
        originalRotation = npc.transform.parent.eulerAngles;
        name = STATE.ATTACKSHIP;
        //shoot = _npc.GetComponent<AudioSource>();
        ColliderEvents events = npc.GetComponent<ColliderEvents>();
        events.CollisionEnterEvent += Colision;
    }

    public override void Enter()
    {
        objetivo = SelectTrozo().GetComponent<TrozosBalsa>();
        anim.SetTrigger("Swim_Regular");
        agent.isStopped = true;
        //shoot.Play();
        base.Enter();
    }

    public override void Update()
    {
        npc.transform.parent.position = Vector3.MoveTowards(npc.transform.parent.position, objetivo.transform.position, 0.05f);
        npc.transform.parent.LookAt(objetivo.transform);
        npc.transform.parent.eulerAngles = new Vector3(originalRotation.x, npc.transform.parent.eulerAngles.y, originalRotation.z);
        Debug.Log(Vector3.Distance(npc.transform.position, objetivo.transform.position));
        if (Vector3.Distance(npc.transform.position, objetivo.transform.position) <=  1.5f)
        {
            anim.SetTrigger("HoldBite");
            damageFloor = false;
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                cooldown = 5f;
                objetivo.vida--;
                if (objetivo.vida < 0)
                {
                    anim.SetTrigger("Swim_Regular");
                    nextState = new Patrol(npc, agent, anim, player, raft);
                    objetivo.GetComponent<TrozosBalsa>().destruirTrozo();
                    objetivo = null;
                    stage = EVENT.EXIT;
                }
            }

        }
        else
        {
            anim.SetTrigger("Swim_Regular");
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
                anim.SetTrigger("Swim_Regular");
                nextState = new RunAway(npc, agent, anim, player, raft);
                stage = EVENT.EXIT;
            }
            //shoot.Stop();
        }
    }

    public override void Exit()
    {

        anim.ResetTrigger("Swim_Calm");
        base.Exit();
    }
    private GameObject SelectTrozo()
    {
        GameObject objetivo = raft.transform.GetChild(0).gameObject;
        foreach(Transform tsb in raft.transform)
        {
            if(Vector3.Distance(npc.transform.position, objetivo.transform.position)>Vector3.Distance(npc.transform.position, tsb.position))
            {
                objetivo = tsb.gameObject;
            }
        }
        return objetivo;
    }
    public void Colision(Collision col)
    {
        if(col.gameObject.tag == "Arma")
        {
            attacked = true;
        }
    }
}
