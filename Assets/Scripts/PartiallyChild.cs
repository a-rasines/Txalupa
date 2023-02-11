using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PartiallyChild : MonoBehaviour {

    public Transform parent;

    public bool3 direction;
    public bool3 exactDirection;
    public bool3 position;
    public bool3 exactPosition;
    private Vector3 dir;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start() {
        Recalculate();
        
    }
    public void Recalculate() {
        Vector3 v1 = transform.eulerAngles;
        Vector3 v2 = parent.eulerAngles;
        dir = new Vector3(exactDirection.x ? 0 : (v1.x - v2.x), exactDirection.y ? 0 : (v1.y - v2.y), exactDirection.z ? 0 : (v1.z - v2.z));
        v1 = transform.position;
        v2 = parent.position;
        pos = new Vector3(exactPosition.x ? 0 : (v1.x - v2.x), exactPosition.y ? 0 : (v1.y - v2.y), exactPosition.z ? 0 : (v1.z - v2.z));
    }
    // Update is called once per frame
    void Update() {
        transform.eulerAngles = new Vector3(direction.x?parent.eulerAngles.x:transform.eulerAngles.x, direction.y ? parent.eulerAngles.y : transform.eulerAngles.y, direction.z ? parent.eulerAngles.z : transform.eulerAngles.z) + pos;
        transform.position = new Vector3(position.x ? parent.position.x : transform.position.x, position.y ? parent.position.y : transform.position.y, position.z ? parent.position.z : transform.position.z) + pos;

    }
}
