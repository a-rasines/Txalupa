using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftTurbulence : MonoBehaviour
{
    void Start(){
        
    }
    private float rotation = 0;
    public float speed = 1;
    public float strenght = 1;
    public float x = 1;
    public float z = 1;
    void Update(){
        rotation += speed * Time.deltaTime;
        float height = Mathf.Sin(rotation) * strenght / 2;
        print(height);
        float angle = height==0? 0 : Mathf.Atan((float)(height / 0.5)/*Cambiar por el tamaño de la balsa*/) * Mathf.Rad2Deg;
        transform.rotation= Quaternion.Euler(x * angle, 0, z * angle);

    }
}
