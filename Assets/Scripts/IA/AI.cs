using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    State currentState;

    public Transform player;
    [SerializeField] GameObject raft;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        currentState = new Patrol(gameObject, agent, anim, player, raft);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }
}
