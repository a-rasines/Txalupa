using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject ItemInSlot;
    public Image slotImage;
    Color originalColor;
    void Start()
    {
        slotImage = GetComponentInChildren<Image>();
        originalColor = slotImage.color;
    }

    private void OnTriggerStay(Collider other)
    {
        if (ItemInSlot != null) return;
        GameObject obj = other.gameObject;
        if (!IsItem(obj)) return;
        InsertItem(obj);
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject == ItemInSlot) {
            ItemInSlot = null;
            Item item = other.gameObject.GetComponent<Item>();
            ResetColor();
            item.currentSlot = null;
            item.inSlot = false;
        }
    }

    bool IsItem(GameObject obj)
    {
        return obj.GetComponent<Item>();
    }

    public void InsertItem(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.transform.SetParent(gameObject.transform, true);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = obj.GetComponent<Item>().slotRotation;
        obj.GetComponent<Item>().inSlot = true;
        obj.GetComponent<Item>().currentSlot = this;
        ItemInSlot = obj;
        slotImage.color = Color.grey;
    }

    public void ResetColor()
    {
        slotImage.color = originalColor;
        
    }
}