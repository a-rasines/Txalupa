using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class BuildManager : MonoBehaviour {
    // Start is called before the first frame update
    public ItemType.BuildConstrain buildConstrain;
    public GameObject raftBase;
    public GameObject model;
    public Material buildableMaterial;
    public Inventory inventory;
    void OnEnable() {
        if (model == null) {
            enabled = false;
            return;
        }
        model = Instantiate(model);
        model.layer = LayerMask.NameToLayer("Ignore Raycast");
        model.transform.position = new Vector3(0, -3, 0);
        Destroy(model.GetComponent<TrozosBalsa>());
        XRSimpleInteractable interactable= model.AddComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(new UnityAction<SelectEnterEventArgs>(OnInteraction));
        interactable.interactionLayers = InteractionLayerMask.GetMask("UI");
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
    public void OnDisable() {
        Destroy(model);
    }
    public void OnInteraction(SelectEnterEventArgs _) {
        ItemType type = ItemTypes.Of(model);
        GameObject built = Instantiate(type.GetModel());
        built.transform.position = model.transform.position;
        built.transform.parent = raftBase.transform;
        built.transform.localPosition = new Vector3((float)Math.Round(built.transform.localPosition.x / 1.5f) * 1.5f, (float)Math.Round(built.transform.localPosition.y / 1.5f) * 1.5f, (float)Math.Round(built.transform.localPosition.z / 1.5f) * 1.5f);
        built.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        inventory.RemoveFromInventory(type, 1);
        if(inventory.GetAmountOf(type) <= 0) {
            Destroy(model);
            enabled = false;
        }
    }
    private void HoverChange() {
        RaycastHit rh;
        if (!Physics.Raycast(transform.position, transform.forward, out rh, 10, LayerMask.GetMask("Water", "Default")))
            return;
        Collider[] colliders = Physics.OverlapSphere(rh.point, 0.5f);
        foreach(Collider c in colliders)
            if(c.gameObject != model) {
                model.transform.position = new Vector3(0, -3, 0);
                return;
            }
        if ((rh.collider.gameObject.layer == LayerMask.NameToLayer("Water")) != (buildConstrain == ItemType.BuildConstrain.WaterBuildable)) {
            model.transform.position = new Vector3(0, -3, 0);
            return;
        }
        Vector3 offset = new Vector3(rh.point.x % 1.5f - raftBase.transform.position.x % 1.5f, 0, rh.point.z % 1.5f - raftBase.transform.position.z % 1.5f);
        if (offset.x > 0.75)
            offset.x = offset.x - 1.5f;
        else if (offset.x < -0.75)
            offset.x = offset.x + 1.5f;
        if (offset.z > 0.75)
            offset.z = offset.z - 1.5f;
        else if (offset.z < -0.75)
            offset.z = offset.z + 1.5f;
        model.transform.position = new Vector3(rh.point.x - offset.x, (buildConstrain == ItemType.BuildConstrain.WaterBuildable) ? raftBase.transform.position.y:rh.point.y, rh.point.z - offset.z);
        
    }
    
    // Update is called once per frame
    void Update() {
        HoverChange();       
    }
}
