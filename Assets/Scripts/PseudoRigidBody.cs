using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PseudoRigidBody : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

        
    }

    // Update is called once per frame
    void Update() {
        if (transform.localPosition != Vector3.zero) {
            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
        
    }
}
