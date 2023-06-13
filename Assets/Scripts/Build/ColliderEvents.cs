using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class ColliderEvents : MonoBehaviour {
    public List<Collider> collisions = new List<Collider>();
    public bool addRigidBody = true;
    void Start() {
        collisions.Clear();
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
        collisions.Add(collision.collider);
        CollisionEnterEvent.Invoke(collision);
    }
    private void OnCollisionExit(Collision collision) {
        collisions.Remove(collision.collider);
        CollisionExitEvent.Invoke(collision);
    }
    private void OnTriggerEnter(Collider col) {
        collisions.Add(col);
        TriggerEnterEvent.Invoke(col);
    }
    private void OnTriggerExit(Collider col) {
        collisions.Remove(col);
        TriggerExitEvent.Invoke(col);
    }
}
