using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Inventory inv;
    public Image icon;
    public int position;
    private GameObject pulled;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.Equals(pulled))
            return;
        if(inv.AddToInventory(position, other.gameObject)) {
            icon.sprite = inv.GetSlot(position).it.icon;
            icon.color = Color.white;
            Destroy(other.gameObject);
        }
    }
    public void OnInteraction() {
        Inventory.Stack s = inv.GetSlot(position);
        pulled = Instantiate(s.it.GetModel());
        pulled.AddComponent<ItemStack>().quantity = s.q;
        Destroy(pulled.GetComponent<TrashMovement>());
        pulled.layer = 0;
        inv.RemoveFromInventory(position);
    }
}
