using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMovement : MonoBehaviour {
    // Start is called before the first frame update
    private Vector2 direction;
    private Vector2 end;
    private float centerHeight;
    private float waveHeight;
    public void Init(Vector2 direction, Vector2 end) {
        this.direction = direction;
        this.end = end;
    }

    void Start() {
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
}
