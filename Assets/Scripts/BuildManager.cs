using System;
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
        if (model == null) {
            enabled = false;
            return;
        }
        model = Instantiate(model);
        Destroy(model.GetComponent<TrozosBalsa>());
        MeshRenderer renderer = model.GetComponent<MeshRenderer>();
        if(renderer == null ) {
            renderer = model.GetComponentInChildren<MeshRenderer>();
        }
        print(renderer);
        Material[] materials = new Material[renderer.materials.Length];
        for (int i = 0; i < renderer.materials.Length; i++) {
            materials[i] = buildableMaterial;
        }
        renderer.materials = materials;


    }
    private void HoverChange() {
        RaycastHit rh;
        if (Physics.Raycast(transform.position + transform.forward, transform.eulerAngles, out rh)) {
            hovered = rh.transform.gameObject;
        } else
            return;
        float offsetX = rh.transform.position.x - raftBase.transform.position.x;
        offsetX -= offsetX % 1.5f;
        float offsetZ = rh.transform.position.z - raftBase.transform.position.z;
        offsetZ -= offsetZ % 1.5f; 
        model.transform.position = new Vector3(offsetX, model.transform.position.y, offsetZ);
        
    }
    
    // Update is called once per frame
    void Update() {
        HoverChange();       
    }
}
