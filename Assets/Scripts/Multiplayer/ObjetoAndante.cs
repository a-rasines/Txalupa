using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoAndante : MonoBehaviour
{
    [SerializeField] float velocidad;
    [SerializeField] float distancia;

    private float direccion = -1f;
    private float distanciaRecorrida = 0f;

    private void Update()
    {
        float distanciaMovimiento = velocidad * Time.deltaTime;

        transform.Translate(Vector3.right * distanciaMovimiento * direccion);

        distanciaRecorrida += distanciaMovimiento;

        if (distanciaRecorrida >= distancia)
        {
            direccion = -direccion;
            distanciaRecorrida = 0f;
        }
    }
}
