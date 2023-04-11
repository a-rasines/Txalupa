using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class PartiallyChild : MonoBehaviour {

    public Transform parent;

    public bool3 direction;
    public bool upsideDownFix = false;
    //public bool3 exactDirection;
    public Vector3 maxDirOffset;
    public bool3 position;
    //public bool3 exactPosition;
    public Vector3 maxPosOffset;
    private Vector3 dir;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start() {
        dir = transform.eulerAngles - parent.eulerAngles;
        pos = transform.position - parent.position;
    }
    // Update is called once per frame
    void Update() {
        /*transform.eulerAngles = new Vector3(
            direction.x?parent.eulerAngles.x:transform.eulerAngles.x, 
            direction.y ? parent.eulerAngles.y : transform.eulerAngles.y, 
            direction.z ? parent.eulerAngles.z : transform.eulerAngles.z
            ) + pos;*/
        transform.position = new Vector3(
            position.x ?
                maxPosOffset.x < 0 ?
                parent.position.x + pos.x
                : transform.position.x - parent.position.x < -maxPosOffset.x || transform.position.x - parent.position.x > maxPosOffset.x?
                parent.position.x + Math.Clamp(transform.position.x - parent.position.x, -maxPosOffset.x, maxPosOffset.x)
                : transform.position.x
            : transform.position.x,
            position.y ?
                maxPosOffset.y < 0 ?
                parent.position.y + pos.y
                : transform.position.y - parent.position.y < -maxPosOffset.y || transform.position.y - parent.position.y > maxPosOffset.y ?
                parent.position.y + Math.Clamp(transform.position.y - parent.position.y, -maxPosOffset.y, maxPosOffset.y)
                : transform.position.y
            : transform.position.y,
            position.z ?
                maxPosOffset.z < 0 ?
                parent.position.z + pos.z
                : transform.position.z - parent.position.z < -maxPosOffset.z || transform.position.z - parent.position.z > maxPosOffset.z ?
                parent.position.z + Math.Clamp(transform.position.z - parent.position.z, -maxPosOffset.z, maxPosOffset.z)
                : transform.position.z
            : transform.position.z
            );
        print(parent.up + " " + parent.eulerAngles);
        transform.eulerAngles = new Vector3(
            direction.x ?
                maxDirOffset.x < 0 ?
                parent.eulerAngles.x + dir.x
                : transform.eulerAngles.x - parent.eulerAngles.x < -maxDirOffset.x || transform.eulerAngles.x - parent.eulerAngles.x > maxDirOffset.x ?
                parent.eulerAngles.x + Math.Clamp(Mathf.DeltaAngle(parent.eulerAngles.x, transform.eulerAngles.x), -maxDirOffset.x, maxDirOffset.x)
                : transform.eulerAngles.x
            : transform.eulerAngles.x,
            direction.y ?
                (maxDirOffset.y < 0 ?
                    parent.eulerAngles.y + dir.y
                    : transform.eulerAngles.y - parent.eulerAngles.y < -maxDirOffset.y || transform.eulerAngles.y - parent.eulerAngles.y > maxDirOffset.y ?
                        parent.eulerAngles.y + Math.Clamp(Mathf.DeltaAngle(parent.eulerAngles.y, transform.eulerAngles.y), -maxDirOffset.y, maxDirOffset.y)
                        : transform.eulerAngles.y) - (parent.up.y < 0 && upsideDownFix ? 180:0)
                : transform.eulerAngles.y,
            direction.z ?
                maxDirOffset.z < 0 ?
                parent.eulerAngles.z + dir.z
                : transform.eulerAngles.z - parent.eulerAngles.z < -maxDirOffset.z || transform.eulerAngles.z - parent.eulerAngles.z > maxDirOffset.z ?
                parent.eulerAngles.z + Math.Clamp(Mathf.DeltaAngle(parent.eulerAngles.z, transform.eulerAngles.z), -maxDirOffset.z, maxDirOffset.z)
                : transform.eulerAngles.z
            : transform.eulerAngles.z
        );

    }
}
