using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATiburonV1 : MonoBehaviour
{
    [SerializeField] GameObject tF;
    private bool attackShip;
    private bool waitingForShip;
    private bool attackPlayer;
    public bool attackedPlayer;
    public bool damageFloor;
    public bool attackingRaft;
    public bool keepAttackingRaft;
    private int health;
    private bool alive;
    private int initialHealth;
    private Vector3 initialPos;
    private Quaternion initialRot; //ENARA
    TrozosBalsa objetivo;
    [SerializeField] GameObject raft;
    private
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation; //ENARA
        health = 50;
        alive = true;
        attackShip = false;
        StartCoroutine(CanAttackShip());
        waitingForShip = false;
        attackedPlayer = false;
        attackPlayer = false;
        attackingRaft = false;
        damageFloor = true;
        keepAttackingRaft = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && alive)
        {
            alive = false;
            StartCoroutine(CanRevive());
        }
        else
        {
            if (alive)
            {
                if (attackShip && objetivo != null && !attackingRaft)
                {
                    transform.position = Vector3.MoveTowards(transform.position, objetivo.transform.position, 0.05f);
                    transform.LookAt(objetivo.transform); //ENARA
                    if (Vector3.Distance(transform.position, objetivo.transform.position) <= 0.75f) attackingRaft = true;
                }
                if (attackShip && !waitingForShip)
                {
                    if (!keepAttackingRaft)
                    {
                        AttackShip();
                        initialHealth = health;
                        keepAttackingRaft = true;
                    }
                    else
                    {
                        if (health <= initialHealth - 6)
                        {
                            keepAttackingRaft = false;
                            attackingRaft = false;
                            StartCoroutine(CanAttackShip());
                            objetivo = null;
                        }
                    }
                    if (objetivo != null && damageFloor && attackingRaft && keepAttackingRaft)
                    {
                        damageFloor = false;
                        StartCoroutine(CanDamageFloor());
                        objetivo.vida--;
                        if (objetivo.vida < 0)
                        {
                            attackingRaft = false;
                            Destroy(objetivo.gameObject);
                            keepAttackingRaft = false;
                            StartCoroutine(CanAttackShip());
                        }
                    }
                }
                else if (attackPlayer && !attackedPlayer)
                {
                    StartCoroutine(CanAttackPlayer());
                }
                else
                {
                    if (transform.position != initialPos)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, initialPos, 0.05f);
                        transform.rotation = Quaternion.Lerp(transform.rotation, initialRot, 0.05f); //ENARA
                    }
                    else
                    {
                        tF.transform.Rotate(0, 15 * Time.deltaTime, 0);
                        initialPos = transform.position;
                        initialRot = transform.rotation; //ENARA
                    }
                }
            }
        }
    }

    private void AttackShip()
    {
        bool select = false;
        while (!select)
        {
            int n = Random.Range(0, raft.transform.childCount);
            TrozosBalsa tb = raft.transform.GetChild(n).GetComponent<TrozosBalsa>();
            if (tb.norte != null || tb.sur != null || tb.este != null || tb.oeste != null)//NullPointer
            {
                objetivo = tb;
            }
            select = true;
        }
    }

    public void AttackThePlayer(bool fuera)
    {
        attackPlayer = fuera;
    }
    public void RecievePlayerAttack(int damage)
    {
        health -= damage;
    }
    private IEnumerator CanRevive()
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(120f);
        GetComponentInChildren<MeshRenderer>().enabled = true;
        health = 50;
        alive = true;
    }
    private IEnumerator CanAttackPlayer()
    {
        attackedPlayer = true;
        yield return new WaitForSeconds(25f);
        attackedPlayer = false;
    }
    private IEnumerator CanDamageFloor()
    {
        yield return new WaitForSeconds(10f);
        damageFloor = true;
    }
    private IEnumerator CanAttackShip()
    {
        waitingForShip = true;
        yield return new WaitForSeconds(15f);
        waitingForShip = false;
        attackShip = true;
    }
    private void OnCollisionEnter(Collision collision) {
        if(collision.transform.tag == "Arma") {
            health -= 3;
        }
    }
}