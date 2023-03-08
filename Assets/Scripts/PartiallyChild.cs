using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PartiallyChild : MonoBehaviour {

    public Transform parent;

    public bool3 direction;
    //public bool3 exactDirection;
    public Vector3 maxDirOffset;
    public bool realTimeDirection;
    public bool3 position;
    //public bool3 exactPosition;
    public Vector3 maxPosOffset;
    public bool realTimePosition;
    private Vector3 dir;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start() {
        RecalculatePosition();
        RecalculateDirecition();

    }
    public void RecalculateDirecition() {
        Vector3 v1 = transform.eulerAngles;
        Vector3 v2 = parent.eulerAngles;
        dir = new Vector3(
            direction.x ?
                maxDirOffset.x < 0 ?
                    (v1.x - v2.x)
                : Mathf.Clamp(v1.x - v2.x, -maxDirOffset.x, maxDirOffset.x)
            : 0,
            direction.y ?
                maxDirOffset.y < 0 ?
                    (v1.y - v2.y)
                : Mathf.Clamp(v1.y - v2.y, -maxDirOffset.y, maxDirOffset.y)
            : 0,
            direction.z ?
                maxDirOffset.z < 0 ?
                    (v1.z - v2.z)
                : Mathf.Clamp(v1.z - v2.z, -maxDirOffset.z, maxDirOffset.z)
            : 0
        //(exactDirection.x || !direction.x) ? 0 : (v1.x - v2.x), 
        //(exactDirection.y || !direction.y) ? 0 : (v1.y - v2.y), 
        //(exactDirection.z || !direction.z) ? 0 : (v1.z - v2.z)
        );
    }
    public void RecalculatePosition() {
        Vector3 v1 = transform.position;
        Vector3 v2 = parent.position;
        pos = new Vector3(
            position.x ?
                maxPosOffset.x < 0 ?
                    (v1.x - v2.x)
                : Mathf.Clamp(v1.x - v2.x, -maxPosOffset.x, maxPosOffset.x)
            : 0,
            position.y ?
                maxPosOffset.y < 0 ?
                    (v1.y - v2.y)
                : Mathf.Clamp(v1.y - v2.y, -maxPosOffset.y, maxPosOffset.y)
            : 0,
            position.z ?
                maxPosOffset.z < 0 ?
                    (v1.z - v2.z)
                : Mathf.Clamp(v1.z - v2.z, -maxPosOffset.z, maxPosOffset.z)
            : 0
            //(exactPosition.x || !position.x) ? 0 : (v1.x - v2.x), 
            //(exactPosition.y || !position.y) ? 0 : (v1.y - v2.y), 
            //(exactPosition.z || !position.z) ? 0 : (v1.z - v2.z)
        );
    }
    // Update is called once per frame
    void Update() {
        if (realTimeDirection)
            RecalculateDirecition();
        if (realTimePosition)
            RecalculatePosition();
        transform.eulerAngles = new Vector3(
            direction.x?parent.eulerAngles.x:transform.eulerAngles.x, 
            direction.y ? parent.eulerAngles.y : transform.eulerAngles.y, 
            direction.z ? parent.eulerAngles.z : transform.eulerAngles.z
            ) + pos;
        transform.position = new Vector3(
            position.x ? parent.position.x : transform.position.x, 
            position.y ? parent.position.y : transform.position.y, 
            position.z ? parent.position.z : transform.position.z
            ) + pos;

    }
}
