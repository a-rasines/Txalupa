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
    public AttackShip(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, GameObject _raft)
        : base(_npc, _agent, _anim, _player, _raft)
    {
        name = STATE.ATTACKSHIP;
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

        npc.transform.position = Vector3.MoveTowards(npc.transform.position, objetivo.transform.position, 0.05f);
        npc.transform.LookAt(objetivo.transform);
        if (Vector3.Distance(npc.transform.position, objetivo.transform.position) <= 0.75f)
        {
            damageFloor = false;
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                cooldown = 5f;
                objetivo.vida--;
                if (objetivo.vida < 0)
                {
                    nextState = new Patrol(npc, agent, anim, player, raft);
                    objetivo.GetComponent<TrozosBalsa>().destruirTrozo();
                }
            }
            
        }
        if (attacked)
        {
            nextState = new RunAway(npc, agent, anim, player, raft);
            //shoot.Stop();
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {

        anim.ResetTrigger("isShooting");
        base.Exit();
    }
    private void SelectTrozo()
    {
        bool select = false;
        while (!select)
        {
            int n = Random.Range(0, raft.transform.childCount);
            TrozosBalsa tb = null;
            while (tb == null)
            {
                n = Random.Range(0, raft.transform.childCount);
                tb = raft.transform.GetChild(n).GetComponent<TrozosBalsa>();
            }
            if (tb.norte != null || tb.sur != null || tb.este != null || tb.oeste != null)//NullPointer
            {
                objetivo = tb;
            }
            select = true;
        }
    }
    public void Colision(Collision col)
    {
        if(col.gameObject.tag == "Arma")
        {
            attacked = true;
        }
    }
}
