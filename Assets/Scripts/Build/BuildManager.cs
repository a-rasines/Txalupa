using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class BuildManager : MonoBehaviour {
    public ItemType.BuildConstrain buildConstrain;
    public GameObject raftBase;
    public GameObject model;
    public Material buildableMaterial;
    public Material invalidMaterial;
    public Inventory inventory;
    private MeshRenderer modelRenderer;
    private int collisions = 0;
    void OnEnable() {
        if (model == null) {
            enabled = false;
            return;
        }
        model = Instantiate(model);
        model.layer = LayerMask.NameToLayer("Ignore Raycast");
        model.transform.position = new Vector3(0, -3, 0);
        Destroy(model.GetComponent<TrozosBalsa>());
        model.transform.parent = raftBase.transform;

        ColliderEvents ce = model.AddComponent<ColliderEvents>();
        ce.CollisionEnterEvent += CollisionEnter;
        ce.TriggerEnterEvent += CollisionEnter;
        ce.CollisionExitEvent += CollisionExit;
        ce.TriggerExitEvent += CollisionExit;

        XRSimpleInteractable interactable= model.AddComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(new UnityAction<SelectEnterEventArgs>(OnInteraction));
        interactable.interactionLayers = InteractionLayerMask.GetMask("UI");
        modelRenderer = model.GetComponent<MeshRenderer>();
        if(modelRenderer == null ) {
            modelRenderer = model.GetComponentInChildren<MeshRenderer>();
        }
        Material[] materials = new Material[modelRenderer.materials.Length];
        for (int i = 0; i < modelRenderer.materials.Length; i++) {
            materials[i] = buildableMaterial;
        }
        modelRenderer.materials = materials;


    }
    public void OnDisable() {
        Destroy(model);
    }
    private void CollisionEnter(object collision) {//Invalidate construction
        GameObject go = (GameObject)collision.GetType().GetProperty("gameObject").GetValue(collision);
        if(collisions == 0) {
            Material[] materials = new Material[modelRenderer.materials.Length];
            for (int i = 0; i < modelRenderer.materials.Length; i++) {
                materials[i] = invalidMaterial;
            }
            modelRenderer.materials = materials;
        }
        if (go != model)
            collisions++;
        
    }
    private void CollisionExit(object collision) {//Validate construction?
        GameObject go = (GameObject)collision.GetType().GetProperty("gameObject").GetValue(collision);
        if (go != model)
            collisions--;
        if (collisions == 0) {
            Material[] materials = new Material[modelRenderer.materials.Length];
            for (int i = 0; i < modelRenderer.materials.Length; i++) {
                materials[i] = buildableMaterial;
            }
            modelRenderer.materials = materials;
        }
    }
    public void OnInteraction(SelectEnterEventArgs _) {//Build
        if (collisions != 0)
            return;
        ItemType type = ItemTypes.Of(model);
        GameObject built = Instantiate(type.GetModel());
        built.transform.parent = type.buildConstrain == ItemType.BuildConstrain.WaterBuildable?raftBase.transform.Find("--- Floors ---") : raftBase.transform;

        built.transform.position = model.transform.position;
        built.transform.localRotation = model.transform.localRotation;
        inventory.RemoveFromInventory(type, 1);
        if(inventory.GetAmountOf(type) <= 0) {
            Destroy(model);
            enabled = false;
        }
    }
    public void OnPrimaryButton() {//Rotate
        if(!enabled) return;
        switch(buildConstrain) {
            case ItemType.BuildConstrain.WaterBuildable:
                model.transform.Rotate(0, 90, 0);
                break;
            case ItemType.BuildConstrain.GridBuildable:
                model.transform.Rotate(0, 180, 0);
                break;
            default: break;
        }
    }
    private void HoverChange() {
        RaycastHit rh;
        if (!Physics.Raycast(transform.position, transform.forward, out rh, 10, LayerMask.GetMask("WaterRaycast", "Default")))
            return;
        if ((rh.collider.gameObject.layer == LayerMask.NameToLayer("WaterRaycast")) != (buildConstrain == ItemType.BuildConstrain.WaterBuildable)) {
            model.transform.position = new Vector3(0, -3, 0);
            return;
        }
        switch (buildConstrain) {
            case ItemType.BuildConstrain.WaterBuildable:
                WaterBuildableBuild(rh);
                break;
            case ItemType.BuildConstrain.GridBuildable:
                GridBuildableBuild(rh);
                break;
            case ItemType.BuildConstrain.WallBuildable:
                WallBuildableBuild(rh);
                break;
            default:
                break;
        }
    }
    private void WaterBuildableBuild(RaycastHit rh) {
        Vector3 offset = new Vector3(rh.point.x % 1.5f - raftBase.transform.position.x % 1.5f, 0, rh.point.z % 1.5f - raftBase.transform.position.z % 1.5f);
        if (offset.x > 0.75)
            offset.x = offset.x - 1.5f;
        else if (offset.x < -0.75)
            offset.x = offset.x + 1.5f;
        if (offset.z > 0.75)
            offset.z = offset.z - 1.5f;
        else if (offset.z < -0.75)
            offset.z = offset.z + 1.5f;
        model.transform.localPosition = new Vector3(
            x: rh.point.x - offset.x - model.transform.parent.position.x,
            y: raftBase.transform.position.y - model.transform.parent.position.y,
            z: rh.point.z - offset.z - model.transform.parent.position.z);
    }
    private void WallBuildableBuild(RaycastHit rh) {

    }
    private void GridBuildableBuild(RaycastHit rh) {
        float x = rh.point.x % 1.5f - raftBase.transform.position.x % 1.5f;
        float z = rh.point.z % 1.5f - raftBase.transform.position.z % 1.5f;
        /*     x
         * |-------|
         * |\     /|
         * | \ 1 / |
         * |  \ /  |
         * | 2 X 3 |z
         * |  / \  |
         * | / 4 \ |
         * |/     \|
         * |-------|
         */
        if(z > 0.75 && Math.Abs(x-0.75) <= z -0.75) { //1
            x += 0.75f;
            model.transform.rotation = Quaternion.Euler(x: model.transform.eulerAngles.x, y: 0, z: transform.eulerAngles.z);
        } else if(x < 0.75 && Math.Abs(z - 0.75) <= x) {//2
            z -= 0.75f;
            model.transform.rotation = Quaternion.Euler(x:model.transform.eulerAngles.x, y: 90, z: transform.eulerAngles.z);
        }else if(x > 0.75 && Math.Abs(z - 0.75) <= x - 0.75) {//3
            z += 0.75f;
            model.transform.rotation = Quaternion.Euler(x: model.transform.eulerAngles.x, y: 90, z: transform.eulerAngles.z);
        } else {//4 o medio
            x -= 0.75f;
            model.transform.rotation = Quaternion.Euler(x: model.transform.eulerAngles.x, y: 0, z: transform.eulerAngles.z);
        }
        model.transform.localPosition = new Vector3(
            x: rh.point.x - x,// - model.transform.parent.position.x, 
            y: rh.point.y,
            z: rh.point.z - z);// - model.transform.parent.position.z);
    }
    
    // Update is called once per frame
    void Update() {
        HoverChange();       
    }
}
