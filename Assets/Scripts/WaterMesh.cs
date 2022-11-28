using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMesh : MonoBehaviour
{
    // Start is called before the first frame update
    Mesh mesh;

    Vector3[] newVertices;
    Vector2[] newUV;
    int[] newTriangles;
    void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        float f = sizeZ * 2 / vertexStep;
        print(""+ sizeZ+ ":"+ vertexStep + " " + f + ":" + ((int)(sizeZ / vertexStep) * 2));
        newVertices = new Vector3[(int)f];
        newUV = new Vector2[newVertices.Length];
        newTriangles = new int[(newVertices.Length - 2) * 3];
        /**
         * 1---2
         * | / |
         * 3---4
         * | / |
         * 5---6
         * Triangles = {1, 2, 3}, {2, 3, 4}, {3, 4, 5}, {4, 5, 6} -> 12
         */
        int index = 0;
        for (float z = 0; z <= sizeZ && index < newVertices.Length; z += vertexStep) {
            print("" + index + ":" + Mathf.Sin(z*q) * maxHeight);
            newVertices[index] = new Vector3(0, Mathf.Sin(z *q) * maxHeight, z);
            newUV[index] = new Vector2(newVertices[index].x, newVertices[index].z);
            index++;
            newVertices[index] = new Vector3(sizeX, Mathf.Sin(z*q) * maxHeight, z);
            newUV[index] = new Vector2(newVertices[index].x, newVertices[index].z);
            index++;
        }
        bool a = false;
        for (int i = 0; i < newVertices.Length - 2; i++) {
            if (a) {
                newTriangles[i * 3] = i;
                newTriangles[i * 3 + 1] = i + 1;
                newTriangles[i * 3 + 2] = i + 2;
            } else {
                newTriangles[i * 3] = i + 2;
                newTriangles[i * 3 + 1] = i + 1;
                newTriangles[i * 3 + 2] = i;
            }
            a = !a;

        }
        mesh.Clear();
        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;
        mesh.RecalculateNormals();
    }
    public float maxHeight = 1;
    public float vertexStep = 0.1f;
    public float sizeX = 1;
    public float sizeZ = 1;
    public float q = 1;
    public int speed = 1;
    void Update(){
        Vector3[] copy = new Vector3[newVertices.Length];
        newVertices.CopyTo(copy, 0);
        for(int i = 0; i < newVertices.Length; i++) {
            newVertices[i].y = copy[(i + speed)%newVertices.Length].y;
        }
        //mesh.Clear();
        mesh.vertices = newVertices;
        //mesh.uv = newUV;
        //mesh.triangles = newTriangles;
        //mesh.RecalculateNormals();
    }
}
