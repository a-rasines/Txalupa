using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftTurbulence : MonoBehaviour
{
    void Start(){
        calculateMotion();
    }
    private void calculateMotion() {
        float maxX = 0;
        float maxY = 0;
        float minX = 0;
        float minY = 0;
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
            sizeX = maxX - minX;
            hipotenusa = Mathf.Sqrt(Mathf.Pow(maxY - minY, 2) + Mathf.Pow(sizeX, 2));
        }
    }
    private float rotation = 0;
    public float speed = 1;
    public float strenght = 1;
    public float x = 1;
    public float z = 1;
    private int childCount;
    private float sizeX;
    private float hipotenusa;
    void Update(){
        if (childCount != transform.childCount) calculateMotion();
        rotation += speed * Time.deltaTime;
        rotation %= 2 * Mathf.PI;
        float height = Mathf.Sin(rotation) * strenght / 2;
        float angle = height==0 || sizeX == 0 ? 0 : Mathf.Atan((float)(height / hipotenusa)) * Mathf.Rad2Deg;
        transform.rotation= Quaternion.Euler(x * angle, 0, z * angle);

    }
}