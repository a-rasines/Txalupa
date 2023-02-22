using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryFilter : AbstractInventory {
    public int[] ignorePositions;
    public GameObject[] itemTypesToAllow;
    private List<ItemType> itemTypes;
    public Inventory inventory;
    private Dictionary<int, Stack> stackMap;

    void Start() {


    }
    public override bool AddToInventory(int position, GameObject g) {
        //Anadir objeto a inventario filtrado si esta en la lista
        return inventory.AddToInventory(position, g);
    }

    public override int GetAmountOf(ItemType type) {
        return inventory.GetAmountOf(type);
    }

    public override Stack GetSlot(int position) {//Implement
        throw new System.NotImplementedException();
    }

    public override Dictionary<int, Stack>.KeyCollection GetSlotPositions() {
        return inventory.GetSlotPositions();
    }

    public override void RegisterSlot(int pos) {
        stackMap.Add(pos, new Stack(0));
        inventory.RegisterSlot(pos);
    }

    public override bool RemoveFromInventory(ItemType type, int amount) {
        //Retirar misma cantidad del inventario filtrado. Si vacio reordenar
        throw new System.NotImplementedException();
    }

    public override Stack RemoveFromInventory(int position) {
        //Coger el stack en esa posicion y quitar la misma cantidad en este inventario. Si vacio reordenar
        throw new System.NotImplementedException();
    }
}
