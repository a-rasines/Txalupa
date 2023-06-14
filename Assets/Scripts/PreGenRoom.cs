using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Representa una habitación a la espera de comprobar que puede generarse
*/
public class PreGenRoom : MonoBehaviour {
    // Start is called before the first frame update
    public GameObject room;
    public GameObject origin;
    [Header("Debug")]
    public Vector3 colliderPosition;
    private bool a = false;
    void Start() {
        colliderPosition = room.GetComponent<Collider>().bounds.center;
        transform.position += new Vector3(colliderPosition.x, 0, colliderPosition.z);
    }
    private void OnCollisionEnter(Collision collision) {
        //gameObject.name = "Collision with " + collision.gameObject.name;this.enabled = false;
        Destroy(gameObject);
    }
    // Update is called once per frame
    void FixedUpdate() {
        if(!a)
            a = true;
        else {
            ProcRoom pr = room.GetComponent<ProcRoom>();
            if(pr.maxSpawns <= 1)
                ProcRoom.rooms.Remove(room);
            if(pr.maxSpawns > 0) {
                pr.maxSpawns--;
                pr = Instantiate(room, transform.position - new Vector3(colliderPosition.x, 0, colliderPosition.z), Quaternion.identity).GetComponent<ProcRoom>();
                pr.enabled = true;
                pr.origin = origin;
                pr.pregenerationPosition = transform.position;
                pr.colliderPosition = colliderPosition;
                pr.finalPosition = pr.transform.position;
                
            }
            //gameObject.name = "Out of spawns";this.enabled = false;
            Destroy(gameObject);
        }
    }
}
