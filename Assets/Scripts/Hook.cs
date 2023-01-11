using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Hook : MonoBehaviour {
    // Start is called before the first frame update
    public Transform start;
    public Transform end;
    public MeshFilter rope;
    private Vector3 startPosition;
    private Vector3 startRotation;
    private Vector3 endPosition;
    private Vector3 endRotation;
    public int vertex = 8;
    public float thickness = 0.5f;
    private bool thrown = false;
    private float startTime = 0;
    void Start() {
        startPosition = start.position;
        startRotation = start.eulerAngles;
        endPosition = end.position;
        endRotation = end.eulerAngles;
    }
    private void OnTriggerEnter(Collider other) {
        if (thrown && other.gameObject.tag == "Grabable")
            other.transform.parent = transform;
    }
    public void OnGrab() {
        thrown = false;
        rope.mesh = null;
        GetComponent<Collider>().isTrigger = true;
    }
    public void OnGrabEnd() {
        GetComponent<Collider>().isTrigger = false;
    }
    public void OnActivate() {
        if (!thrown) {
            startTime = Time.time;
        } else {

        }
    }
    public void OnActivateEnd() {
        if (!thrown) {
            float time = Mathf.Min(3, Time.time - startTime);
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(transform.forward * time * 1000 );
            GetComponent<XRGrabInteractable>().enabled = false;
            StartCoroutine(ResetGrabable());
            thrown = true;
            CreateRope();
        }

    }

    private IEnumerator ResetGrabable() {
        yield return new WaitForSeconds(1);
        GetComponent<XRGrabInteractable>().enabled = true;
    }
    int[] triangles = new int[0];
    float offsetY = -0.17f; //Funciona con este valor y no se por qué
    private void CreateRope() {
        startPosition = start.position;
        startRotation = start.eulerAngles;
        endPosition = end.position;
        endRotation = end.eulerAngles;
        float step = 2 * Mathf.PI / vertex;
        Vector3[] vertices = new Vector3[vertex * 2 + 2];
        vertices[0] = startPosition - new Vector3(transform.position.x, transform.position.y + offsetY, transform.position.z);
        for (int i = 1; i < vertex + 1; i++) {//Start
            //(Sp + cos(a), Sp + sen(a), S+cos(Sr))
            vertices[i] = new Vector3(startPosition.x - transform.position.x + Mathf.Cos(step * (i - 1)) * thickness, startPosition.y - transform.position.y - offsetY + Mathf.Sin(step * (i - 1)) * thickness, startPosition.z - transform.position.z + Mathf.Cos(startRotation.y) * Mathf.Cos(step * (i - 1)) * thickness) ;
        }
        for (int i = vertex + 1; i < 2 * vertex + 1; i++) {//End
            vertices[i] = new Vector3(endPosition.x - transform.position.x + Mathf.Cos(step * (i - 1)) * thickness, endPosition.y - transform.position.y - offsetY + Mathf.Sin(step * (i - 1)) * thickness, endPosition.z - transform.position.z + Mathf.Cos(endRotation.y) * Mathf.Cos(step * (i - 1)) * thickness);
        }
        vertices[2 * vertex + 1] = endPosition - new Vector3(transform.position.x, transform.position.y + offsetY, transform.position.z);
        rope.mesh.vertices = vertices;
        rope.mesh.RecalculateNormals();
        if (triangles.Length != 12 * vertex) {//3 * vertices + 6 * vertices + 3 * vertices
            triangles = new int[12 * vertex];
            int trianglesIndex = 0;
            //Cara 1
            for(int i = 1; i < vertex; i++) {
                triangles[trianglesIndex++] = i;
                triangles[trianglesIndex++] = 0;
                triangles[trianglesIndex++] = i + 1;
            }
            triangles[trianglesIndex++] = 0;
            triangles[trianglesIndex++] = 1;
            triangles[trianglesIndex++] = vertex;
            
            //Lados
            for(int i = 1; i < vertex; i++) {
                triangles[trianglesIndex++] = i;
                triangles[trianglesIndex++] = i + 1;
                triangles[trianglesIndex++] = i + vertex + 1;
                /*
                 * 123--
                 * 132
                 * 213
                 * 231
                 * 312
                 * 321
                 */
                triangles[trianglesIndex++] = i;
                triangles[trianglesIndex++] = i + vertex + 1;
                triangles[trianglesIndex++] = i + vertex;
            }
            triangles[trianglesIndex++] = vertex;
            triangles[trianglesIndex++] = 1;
            triangles[trianglesIndex++] = vertex + 1;

            triangles[trianglesIndex++] = vertex + 1;
            triangles[trianglesIndex++] = vertex * 2;
            triangles[trianglesIndex++] = vertex;
            //Cara 2
            for (int i = vertex + 1; i < 2 * vertex; i++) {
                triangles[trianglesIndex++] = 2 * vertex + 1;
                triangles[trianglesIndex++] = i;
                triangles[trianglesIndex++] = i + 1;
            }
            triangles[trianglesIndex++] = vertex + 1;
            triangles[trianglesIndex++] = 2 * vertex + 1;
            triangles[trianglesIndex++] = 2 * vertex;
            rope.mesh.triangles = triangles;
        }
    }
    // Update is called once per frame
    void Update() {
        if(thrown && (start.position != startPosition || start.eulerAngles != startRotation || endPosition != end.position || endRotation != end.eulerAngles)) {
            CreateRope();
        }

    }
}
