using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Slot : MonoBehaviour
{
    public AbstractInventory inv;
    public Image icon;
    public int position;
    protected GameObject pulled;
    public ActionBasedController nonLaserHand;
    private ColliderEvents handCollisions;
    private void Start() {
        inv.OnItemChanged += OnItemChange;
        inv.RegisterSlot(position);
        handCollisions = nonLaserHand.GetComponent<ColliderEvents>();
    }
    private void OnItemChange(int position, ItemType it, int quantity) {
        if (this.position != position)
            return;
        if(!(it is null)) {
            icon.sprite = it.icon;
            icon.color = Color.white;
        } else {
            icon.sprite = null;
            icon.color = new Color(0, 0, 0, 0);
        }

    }
    /**                 Relocated, now in OnInteraction -> if (handCollisions.collisions.Count != 0)
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.Equals(pulled))
            return;
        if(inv.AddToInventory(position, other.gameObject)) {
            Destroy(other.gameObject);
        }
    }
    */
    public void OnInteraction() {
        Inventory.Stack s = inv.GetSlot(position);
        if (handCollisions.collisions.Count != 0) {//Store new item
            Collider o = handCollisions.collisions[0];
            if (inv.AddToInventory(position, o.gameObject)) {
                handCollisions.collisions.Remove(o);
                Destroy(o.gameObject);
            }
            return;
        }

        // Get item

        if (s.q == 0) return;
        pulled = Instantiate(s.it.GetModel(), Camera.main.transform.position + Camera.main.transform.forward, Quaternion.Euler(0, 0, 0));
        pulled.AddComponent<ItemStack>().quantity = s.q;
        pulled.layer = LayerMask.NameToLayer("Grabable_Collider");
        Collider collider = pulled.GetComponent<Collider>();
        foreach (Transform t in pulled.transform)
            if (t.GetComponent<MeshRenderer>() != null) {
                Collider c = t.GetComponent<Collider>();
                if (c == null)
                    t.AddComponent<BoxCollider>().isTrigger = true;
                else
                    c.isTrigger = true;
                t.gameObject.layer = LayerMask.NameToLayer("Grabable");
            }
        if (pulled.GetComponent<Rigidbody>() == null && pulled.GetComponentInChildren<Rigidbody>() == null)
            pulled.AddComponent<Rigidbody>();
        if (pulled.GetComponentInChildren<XRGrabInteractable>() == null && pulled.GetComponentInChildren<XRGrabInteractable>() == null) {
            XRGrabInteractable t = pulled.AddComponent<XRGrabInteractable>();
            t.interactionLayers = InteractionLayerMask.NameToLayer("Grab") | ~InteractionLayerMask.NameToLayer("Default");
            t.colliders.Remove(collider);
        }
        Destroy(pulled.GetComponent<TrashMovement>());
        Destroy(pulled.GetComponent<TrozosBalsa>());
        inv.RemoveFromInventory(position);
    }
}
