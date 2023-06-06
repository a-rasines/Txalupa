using Fusion.Assistants;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class ProcRoom : MonoBehaviour {
    public static List<GameObject> rooms = null;
    // Start is called before the first frame update
    public Transform eastConnection, westConnection, northConnection, southConnection;
    public List<GameObject> _rooms;
    public int maxSpawns;

    [Header("Debug")]
    public GameObject origin;
    public Vector3 pregenerationPosition;
    public Vector3 colliderPosition;
    public Vector3 finalPosition;
    void Start() {
        if(rooms == null && _rooms.Count != 0) {
            rooms = new List<GameObject>();
            foreach(GameObject room in _rooms) {
                GameObject r = Instantiate(room);                  //
                r.transform.position = new Vector3(0, -1000000, 0);// Odio Unity
                rooms.Add(r);                                      //
            }
        }
            
        Connect(eastConnection , delegate (ProcRoom p) { return p.westConnection; });
        Connect(westConnection , delegate (ProcRoom p) { return p.eastConnection; });
        Connect(northConnection, delegate (ProcRoom p) { return p.southConnection;});
        Connect(southConnection, delegate (ProcRoom p) { return p.northConnection;});
    }
    private delegate Transform GetOthersTranform(ProcRoom p);
    private void Connect(Transform origin, GetOthersTranform end) {
        if(origin == null || rooms == null) return;
        List<GameObject> remaining = new List<GameObject>(rooms);
        Transform selected= null;
        ProcRoom other = null;
        while(selected == null) {
            if (remaining.Count == 0)
                return;
            other = remaining[Random.Range(0, remaining.Count)].GetComponent<ProcRoom>();
            selected = end(other);
            remaining.Remove(other.gameObject);
        }
        Transform repr = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        Collider col = other.GetComponent<Collider>();
        repr.position = origin.position - selected.localPosition;// + new Vector3(col.bounds.center.x, 0, col.bounds.center.z); <-- Si lo elimino por alguna razon reaparece
        other.gameObject.SetActive(true);
        repr.localScale = col.bounds.extents*2;
        PreGenRoom pgr = repr.gameObject.AddComponent<PreGenRoom>();
        repr.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        repr.gameObject.layer = other.gameObject.layer;
        pgr.origin = gameObject;
        pgr.room = other.gameObject;
    }
    // Update is called once per frame
    void Update() {

        
    }
}
