using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public AbstractInventory inv;
    public Image icon;
    public int position;
    protected GameObject pulled;
    private void Start() {
        inv.OnItemChanged += OnItemChange;
        inv.RegisterSlot(position);
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
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.Equals(pulled))
            return;
        if(inv.AddToInventory(position, other.gameObject)) {
            Destroy(other.gameObject);
        }
    }
    public void OnInteraction() {
        Inventory.Stack s = inv.GetSlot(position);
        if (s.q == 0) return;
        pulled = Instantiate(s.it.GetModel(), Camera.main.transform.position + Camera.main.transform.forward, Quaternion.Euler(0, 0, 0));
        pulled.AddComponent<ItemStack>().quantity = s.q;
        Destroy(pulled.GetComponent<TrashMovement>());
        Destroy(pulled.GetComponent<TrozosBalsa>());
        pulled.layer = 0;
        inv.RemoveFromInventory(position);
    }
}