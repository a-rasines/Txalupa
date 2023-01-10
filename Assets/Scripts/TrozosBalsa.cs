using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrozosBalsa : MonoBehaviour
{
    public GameObject norte;
    public GameObject sur;
    public GameObject este;
    public GameObject oeste;
    public int vida = 3;
    void Start()
    {
        for(int i = 0; i<transform.parent.childCount; i++)
        {
            Vector3 pos = transform.parent.GetChild(i).position;
            if (transform.position.x == pos.x)
            {
                if ((transform.position.z - 1.5f) == pos.z)
                {
                    norte = transform.parent.GetChild(i).gameObject;
                    transform.parent.GetChild(i).GetComponent<TrozosBalsa>().sur = gameObject;
                }
                else if((transform.position.z + 1.5f) == pos.z)
                {
                    sur = transform.parent.GetChild(i).gameObject;
                    transform.parent.GetChild(i).GetComponent<TrozosBalsa>().norte = gameObject;
                }
            }
            else if(transform.position.z == pos.z)
            {
                if ((transform.position.x - 1.5f) == pos.x)
                {
                    oeste = transform.parent.GetChild(i).gameObject;
                    transform.parent.GetChild(i).GetComponent<TrozosBalsa>().este = gameObject;
                }
                else if((transform.position.x - 1.5f) == pos.x)
                {
                    este = transform.parent.GetChild(i).gameObject;
                    transform.parent.GetChild(i).GetComponent<TrozosBalsa>().oeste = gameObject;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
