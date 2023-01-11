using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] CrafteableObject[] crafteableObject;
    public int seleccionado { get; set; }
    public void Crafting()
    {
        if (inventory.GetAmountOf(ItemType.Of("Plank")) >= crafteableObject[seleccionado].GetWood() && inventory.GetAmountOf(ItemType.Of("Plastic")) >= crafteableObject[seleccionado].GetPlastic() && inventory.GetAmountOf(ItemType.Of("Scrap")) >= crafteableObject[seleccionado].GetScrap() && inventory.GetAmountOf(ItemType.Of("Thatch")) >= crafteableObject[seleccionado].GetLeaves())
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if ((inventory.GetSlot(y * 10 + x)).q == 0)
                    {

                        inventory.AddToInventory((y * 10 + x), crafteableObject[seleccionado].GetPrefab());
                    }
                }
            }
        }
    }
}
