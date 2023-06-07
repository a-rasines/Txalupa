using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class ColliderEvents : MonoBehaviour {
    // Start is called before the first frame update
    public bool addRigidBody = true;
    void Start() {
        if(GetComponent<Rigidbody>() == null && addRigidBody) {
            gameObject.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        CollisionEnterEvent += Unused;
        CollisionExitEvent += Unused;
        TriggerEnterEvent += Unused;
        TriggerExitEvent += Unused;

    }
    private void Unused(object o) { }
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
