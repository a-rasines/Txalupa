using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCalc : MonoBehaviour
{
    [SerializeField]
    Transform[] startPosition;

    private Quaternion startRotation;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Cube"))
        {
            Debug.Log("Funciona");
            Counter.score++;
            this.transform.position = startPosition[Random.Range(0, startPosition.Length)].position;
        }
    }
}
