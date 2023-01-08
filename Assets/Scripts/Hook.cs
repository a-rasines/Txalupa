using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Hook : MonoBehaviour {
    // Start is called before the first frame update
    private bool thrown = false;
    private float startTime = 0;
    void Start() {

        
    }
    private void OnTriggerEnter(Collider other) {
        if (thrown && other.gameObject.tag == "Grabable")
            other.transform.parent = transform;
    }
    public void OnGrab() {
        thrown = false;
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
            rb.AddForce(transform.forward * time * 100 );
            GetComponent<XRGrabInteractable>().enabled = false;
            StartCoroutine(ResetGrabable());
            thrown = true;
        }

    }

    private IEnumerator ResetGrabable() {
        yield return new WaitForSeconds(1);
        GetComponent<XRGrabInteractable>().enabled = true;
    }

    // Update is called once per frame
    void Update() {


    }
}
