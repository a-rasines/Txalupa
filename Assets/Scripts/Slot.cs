using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Inventory inv;
    public Image icon;
    public int position;
    private void OnTriggerEnter(Collider other) {
        if(inv.AddToInventory(position, other.gameObject)) {
            icon.sprite = inv.GetSlot(position).it.icon;
            icon.color = Color.white;
            Destroy(other.gameObject);
        }
    }
    public void OnInteraction() {

    }
}
