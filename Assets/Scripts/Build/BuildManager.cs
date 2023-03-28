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
    private GameObject clone;
    public Material buildableMaterial;
    public Inventory inventory;
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
        clone = Instantiate(model);
        clone.layer = LayerMask.NameToLayer("Ignore Raycast");
        clone.transform.parent = raftBase.transform;
        MeshRenderer renderer = clone.GetComponent<MeshRenderer>();
        if(renderer == null)
            renderer= clone.GetComponentInChildren<MeshRenderer>();
        renderer.enabled = false;

        ColliderEvents ce = clone.AddComponent<ColliderEvents>();
        ce.CollisionEnterEvent += CollisionEnter;
        ce.TriggerEnterEvent += CollisionEnter;
        ce.CollisionExitEvent += CollisionExit;
        ce.TriggerExitEvent += CollisionExit;

        XRSimpleInteractable interactable= model.AddComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(new UnityAction<SelectEnterEventArgs>(OnInteraction));
        interactable.interactionLayers = InteractionLayerMask.GetMask("UI");
        renderer = model.GetComponent<MeshRenderer>();
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
    private void CollisionEnter(object collision) {
        GameObject go = (GameObject)collision.GetType().GetProperty("gameObject").GetValue(collision);
        if (go != model)
            collisions++;
        
    }
    private void CollisionExit(object collision) {
        GameObject go = (GameObject)collision.GetType().GetProperty("gameObject").GetValue(collision);
        if (go != model)
            collisions--;
    }
    public void OnInteraction(SelectEnterEventArgs _) {
        if (collisions != 0)
            return;
        ItemType type = ItemTypes.Of(model);
        GameObject built = Instantiate(type.GetModel());
        built.transform.parent = type.buildConstrain == ItemType.BuildConstrain.WaterBuildable?raftBase.transform.Find("--- Floors ---") : raftBase.transform;
        built.transform.localPosition = new Vector3(
            model.transform.position.x - built.transform.parent.position.x + (float)Math.Round(built.transform.localPosition.x / 1.5f) * 1.5f,
            model.transform.position.y - built.transform.parent.position.y + (float)Math.Round(built.transform.localPosition.y / 1.5f) * 1.5f,
            model.transform.position.z - built.transform.parent.position.z + (float)Math.Round(built.transform.localPosition.z / 1.5f) * 1.5f);
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
        if ((rh.collider.gameObject.layer == LayerMask.NameToLayer("Water")) != (buildConstrain == ItemType.BuildConstrain.WaterBuildable)) {
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
        clone.transform.position = model.transform.position;
        clone.transform.localPosition = new Vector3(
            model.transform.position.x - clone.transform.parent.position.x + (float)Math.Round(clone.transform.localPosition.x / 1.5f) * 1.5f,
            model.transform.position.y - clone.transform.parent.position.y + (float)Math.Round(clone.transform.localPosition.y / 1.5f) * 1.5f,
            model.transform.position.z - clone.transform.parent.position.z + (float)Math.Round(clone.transform.localPosition.z / 1.5f) * 1.5f);
        clone.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
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
        model.transform.position = new Vector3(rh.point.x - offset.x, raftBase.transform.position.y, rh.point.z - offset.z);
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
        }else if(x < 0.75 && Math.Abs(z - 0.75) <= x) {//2
            z -= 0.75f;
        }else if(x > 0.75 && Math.Abs(z - 0.75) <= x - 0.75) {//3
            z += 0.75f; 
        }else {//4 o medio
            x -= 0.75f;
        }
    }
    
    // Update is called once per frame
    void Update() {
        HoverChange();       
    }
}
