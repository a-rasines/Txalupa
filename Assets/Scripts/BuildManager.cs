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
        model.layer = LayerMask.NameToLayer("Ignore Raycast");
        model.transform.position = new Vector3(0, -3, 0);
        Destroy(model.GetComponent<TrozosBalsa>());
        MeshRenderer renderer = model.GetComponent<MeshRenderer>();
        if(renderer == null ) {
            renderer = model.GetComponentInChildren<MeshRenderer>();
        }
        Material[] materials = new Material[renderer.materials.Length];
        for (int i = 0; i < renderer.materials.Length; i++) {
            materials[i] = buildableMaterial;
        }
        renderer.materials = materials;


    }
    private void HoverChange() {
        RaycastHit rh;
        if (Physics.Raycast(transform.position, transform.forward, out rh, 10, LayerMask.GetMask("Water", "Default"))) {
            hovered = rh.transform.gameObject;
        }else
            return;
        model.transform.position = new Vector3(rh.point.x - rh.point.x % 1.5f + raftBase.transform.position.x % 1.5f, isWaterBuildable?raftBase.transform.position.y:rh.point.y, rh.point.z - rh.point.z % 1.5f + raftBase.transform.position.z);
        
    }
    
    // Update is called once per frame
    void Update() {
        HoverChange();       
    }
}
