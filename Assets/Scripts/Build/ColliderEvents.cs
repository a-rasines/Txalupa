using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class ColliderEvents : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        if(GetComponent<Rigidbody>() == null) {
            gameObject.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        
    }
    public delegate void CollisionEvent(Collision col);
    public event CollisionEvent CollisionEnterEvent;
    public event CollisionEvent CollisionExitEvent;

    public delegate void TriggerEvent(Collider col);
    public event TriggerEvent TriggerEnterEvent;
    public event TriggerEvent TriggerExitEvent;

    private void OnCollisionEnter(Collision collision) {
        CollisionEnterEvent.Invoke(collision);
    }
    private void OnCollisionExit(Collision collision) {
        CollisionExitEvent.Invoke(collision);
    }
    private void OnTriggerEnter(Collider col) {
        TriggerEnterEvent.Invoke(col);
    }
    private void OnTriggerExit(Collider col) {
        TriggerExitEvent.Invoke(col);
    }
    
    // Update is called once per frame
    void Update() {

        
    }
}