using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftTurbulence : MonoBehaviour
{
    void Start(){
        calculateMotion();
    }
    private void calculateMotion() {
        childCount = transform.childCount;
        for (int i = 0; i < childCount; i++) {
            Transform t = transform.GetChild(i);
            if (t.localPosition.x + t.lossyScale.x / 2 > maxX) {
                maxX = t.localPosition.x + t.lossyScale.x / 2;
            } else if (t.localPosition.x - t.lossyScale.x / 2 < minX) {
                minX = t.localPosition.x - t.lossyScale.x / 2;
            }
            if (t.localPosition.y + t.lossyScale.y / 2 > maxY) {
                maxY = t.localPosition.y + t.lossyScale.y / 2;
            } else if (t.localPosition.y - t.lossyScale.y / 2 < minY) {
                minY = t.localPosition.y - t.lossyScale.y / 2;
            }
        }
    }
    private float rotation = 0;
    public float speed = 1;
    public float strenght = 1;
    public float x = 1;
    public float z = 1;
    private float maxX;
    private float maxY;
    private float minX;
    private float minY;
    private int childCount;
    void Update(){
        if (childCount != transform.childCount) calculateMotion();
        rotation += speed * Time.deltaTime;
        float height = Mathf.Sin(rotation) * strenght / 2;
        print(""+height + " (" + maxX + ":"+maxY+") ("+minX+":"+minY+")");
        float angle = height==0 || maxX == minX ? 0 : Mathf.Atan((float)(height / Mathf.Sqrt(Mathf.Pow(maxY - minY,2) + Mathf.Pow(maxX - minX, 2)))/*Cambiar por el tamaño de la balsa*/) * Mathf.Rad2Deg;
        transform.rotation= Quaternion.Euler(x * angle, 0, z * angle);

    }
}
