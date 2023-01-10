using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaivour : MonoBehaviour
{
    private int vida;
    private bool agua;
    private IATiburonV1 tiburon;
    void Start()
    {
        agua = false;
        vida = 100;
        if(tiburon == null)
        {
            tiburon = GameObject.Find("Shark_Old").GetComponent<IATiburonV1>(); ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (vida <= 0)
        {

        }
        if(agua)
        {
            tiburon.transform.position = Vector3.MoveTowards(tiburon.transform.position, this.transform.position, 0.2f);
            if (Vector3.Distance(this.transform.position, tiburon.transform.position)< 1.5f)
            {
                vida -= 20;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Water")
        {
            agua = true;
            tiburon.AttackThePlayer(true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Water")
        {
            agua = false;
            tiburon.AttackThePlayer(false);
        }
    }
}