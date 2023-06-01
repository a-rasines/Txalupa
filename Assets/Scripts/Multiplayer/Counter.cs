using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public int hostCounter = 0;
    public int clientCounter = 0;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("object"))
        {
            if (other.CompareTag("Host")) //hay que cambiarlo
            {
                hostCounter++;
            }
            else if (other.CompareTag("Client")) //hay que cambiarlo
            {
                clientCounter++;
            }
        }
    }
}
