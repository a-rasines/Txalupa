using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGrab : MonoBehaviour
{
    /*bool inInventory = false;
    private GameObject parent = null;
    public void Update(){
        if (transform.parent == null && parent != null) {
            OnParentRemoved(parent);
            parent = null;
        }else if (transform.parent == null) {
        }else if(transform.parent.gameObject != parent) {
            if (parent == null)
                OnParentSet(transform.parent.gameObject);
            else
                OnParentChanged(parent, transform.parent.gameObject);
            parent = transform.parent.gameObject;
        }
        if (inInventory && !GetComponent<Rigidbody>().isKinematic) {
            transform.parent.GetComponentInParent<Slot>().InsertItem(gameObject);
        } else if (!inInventory && GetComponent<Rigidbody>().useGravity)
            GetComponent<Rigidbody>().isKinematic = false;
    }
    private void OnParentRemoved(GameObject oldParent) {
        //print("Parent removed: " + oldParent);
        inInventory = false;        
    }
    private void OnParentChanged(GameObject oldParent, GameObject newParent) {
        //print("Parent changed: " + oldParent + " -> " + newParent);
        inInventory = newParent.GetComponentInParent<Slot>() != null;
    }

    private void OnParentSet(GameObject newParent) {
        //print("Parent set: " + newParent);
        inInventory = newParent.GetComponentInParent<Slot>() != null;
    }*/
}
