using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildManager : MonoBehaviour {
    // Start is called before the first frame update
    public bool isWaterBuildable;
    public GameObject raftBase;
    public GameObject model;
    GameObject hovered;
    public Material buildableMaterial;
    void OnEnable() {
        if (model == null)
            enabled = false;
        MeshRenderer renderer = model.GetComponent<MeshRenderer>();
        if(renderer == null ) {
            renderer = gameObject.GetComponentInChildren<MeshRenderer>();
        }
        for(int i = 0; i < renderer.materials.Length; i++) {
            renderer.materials[i] = buildableMaterial;
        }
        
    }
    /*
    public void HoverChange() {
        RaycastHit rh;
        if (Physics.Raycast(transform.position, Camera.main.transform.eulerAngles, out rh)) {
            hovered = rh.transform.gameObject;
        } else
            return;
        if (!model.IsPrefabInstance()) {
            float offsetX = rh.transform.position.x - raftBase.transform.position.x;
            offsetX -= offsetX % 1.5f;
            float offsetZ = rh.transform.position.z - raftBase.transform.position.z;
            offsetZ -= offsetZ % 1.5f;
            model = Instantiate(model, new Vector3(offsetX, isWaterBuildable?0:rh.transform.position.y, offsetZ), Quaternion.identity);
        }
    }
    */
    // Update is called once per frame
    void Update() {

        
    }
}