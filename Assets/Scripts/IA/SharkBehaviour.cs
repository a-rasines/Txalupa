using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBehaviour : MonoBehaviour
{
    public int vida;
    void Start()
    {
        vida = 50;
    }

    // Update is called once per frame
    public void Daño()
    {
        vida -= 10;
    }
    void Update()
    {
           
    }
}
