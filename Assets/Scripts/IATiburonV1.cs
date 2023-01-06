using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATiburonV1 : MonoBehaviour
{
    [SerializeField] GameObject tF;
    private bool agua; //cambiar para que detecte si el jugador toca agua.
    private bool attackShip;
    private bool attackPlayer;
    private int health;
    private bool alive;
    // Start is called before the first frame update
    void Start()
    {
        agua = false;
        health = 100;
        alive = true;
        attackShip = false;
        attackPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            alive = false;
            StartCoroutine(CanRevive());
        }
        if (attackShip == false)
        {
            StartCoroutine(CanAttackShip());
        }
        if (alive)
        {
            if (!agua)
            {
                tF.transform.Rotate(0, 15 * Time.deltaTime, 0);
            }
            else
            {
                if (attackPlayer)
                {
                    AttackPlayer();
                    attackPlayer = false;
                }
            }
        }
    }
    private void ReciveDamage(int damage)
    {
        health -= damage;
    }
    private void AttackPlayer()
    {

    }
    private IEnumerator CanRevive()
    {
        yield return new WaitForSeconds(120f);
        health = 100;
    }
    private IEnumerator CanAttackPlayer()
    {
        yield return new WaitForSeconds(25f);
        attackPlayer = true;
    }
    private IEnumerator CanAttackShip()
    {
        yield return new WaitForSeconds(60f);
        attackShip = true;
    }
}
