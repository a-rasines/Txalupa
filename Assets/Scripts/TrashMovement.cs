using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMovement : MonoBehaviour {
    // Start is called before the first frame update
    private Vector2 direction;
    private Vector2 end;
    private Rigidbody rb;
    private float waveHeight;
    public void Init(Vector2 direction, Vector2 end, float waveHeight) {
        this.direction = direction;
        this.end = end;
        this.waveHeight = waveHeight;
        y0 = float.MaxValue - waveHeight;
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update() {
        transform.Translate(direction.x*Time.deltaTime, 0, direction.y*Time.deltaTime);
        if (
            (direction.x == 0 || (direction.x < 0 && transform.position.x < end.x) || (direction.x > 0 && transform.position.x > end.x)) &&
            (direction.y == 0 || (direction.y < 0 && transform.position.y < end.y) || (direction.y > 0 && transform.position.y > end.y))
        )
            Destroy(gameObject);
    }
    private float y0;
    private int factor = 0;
    private void OnCollisionEnter(Collision collision) {
        if (rb.useGravity) {
            y0 = transform.position.y;
            rb.useGravity = false;
        }
        factor = 1;
    }
    private void FixedUpdate() {
        if(!rb.useGravity)GetComponent<Rigidbody>().AddForce(new Vector3(0, factor * waveHeight/2, 0));
        if (transform.position.y > y0 + waveHeight)
            factor = -1;
    }
}
