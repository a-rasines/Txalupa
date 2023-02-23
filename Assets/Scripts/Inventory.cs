using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInventory : MonoBehaviour {

    public delegate void ItemChangeEventHandler(int position, ItemType it, int quantity);
    public event ItemChangeEventHandler OnItemChanged;
    public struct Stack {
        public ItemType it;
        public int q;
        public Stack(ItemType it, int q) {
            this.it = it;
            this.q = q;
        }
        public Stack(byte _) {//Null constructor
            it = null;
            q = 0;

        }
        public static bool operator ==(Stack a, Stack b) {
            return a.q == b.q && a.it == b.it;
        }
        public static bool operator !=(Stack a, Stack b) {
            return !(a == b);
        }
    }
    protected void TriggerOnItemChanged(int position, ItemType it, int quantity) {
        OnItemChanged(position, it, quantity);
    }
    /// <summary>
    /// Registra una nueva posici�n vac�a en el inventario.
    /// </summary>
    /// <param name="pos"> Posicion unica del slot. Ningun formato obligatorio </param>
    public abstract void RegisterSlot(int pos);

    /// <summary>
    /// Anade el objeto al inventario en la posicion indicada
    /// </summary>
    /// <param name="position"> Posicion unica del slot en el que meterlo, previamente definido. </param>
    /// <param name="g"> GameObject asociado con el ItemType a a�adir. </param>
    public abstract bool AddToInventory(int position, GameObject g);
    public abstract bool RemoveFromInventory(ItemType type, int amount);
    public abstract Stack RemoveFromInventory(int position);

    public abstract int GetAmountOf(ItemType type);
    public abstract Stack GetSlot(int position);
    public abstract Dictionary<int, Stack>.KeyCollection GetSlotPositions();

}

public class Inventory : AbstractInventory {
    private Dictionary<int, Stack> inventory = new Dictionary<int, Stack>();//Por slot
    private Dictionary<ItemType, int> itemCounts = new Dictionary<ItemType, int>();//Por material
    
    private void _(int _, ItemType __, int ___) {}
    private void Start() {
        OnItemChanged += _;
    }
    public override void RegisterSlot(int pos) {
        inventory.Add(pos, new Stack(0));
    }
    public override Dictionary<int, Stack>.KeyCollection GetSlotPositions() {
        return inventory.Keys;
    }
    public override int GetAmountOf(ItemType type) {
        int outV = 0;
        itemCounts.TryGetValue(type, out outV);
        return outV;
    }
    public override bool RemoveFromInventory(ItemType type, int amount) {
        if (GetAmountOf(type) < amount)
            return false;
        Dictionary<int, Stack> cl = new Dictionary<int, Stack>(inventory);
        foreach(int key in inventory.Keys) {
            Stack s = cl[key];
            if (s.it.Equals(type)) {
                int newAm = s.q - amount;
                amount -= s.q;
                if(newAm < 0) {
                    RemoveFromInventory(key);
                } else {
                    s.q = newAm;
                    return true;
                }
            }
        }
        return false;
    }
    public override bool AddToInventory(int position, GameObject g) {
        ItemType it = ItemType.Of(g);
        Stack stack = new Stack(0);
        if (it is null)
            return false;
        int outV = 0;
        itemCounts.TryGetValue(it, out outV);
        if (!inventory.TryGetValue(position, out stack)) {//La posici�n est� vac�a
            inventory[position] = new Stack(it, it.GetCuantityFrom(g));
            itemCounts[it] = outV;
            
            TriggerOnItemChanged(position, it, it.GetCuantityFrom(g));
            return true;
        } else if(stack.it.Equals(it) && stack.q + it.GetCuantityFrom(g) <= it.stackCount){//La posici�n tiene el mismo objeto y no sobrepasan el m�ximo
            stack.q += it.GetCuantityFrom(g);
            inventory[position] = stack;
            itemCounts[it] = outV;
            TriggerOnItemChanged(position, it, stack.q);
            return true;
        } else {
            return false;
        }
    }
    public override Stack GetSlot(int position) {
        Stack s = new Stack(0);
        inventory.TryGetValue(position, out s);
        return s;
    }
    public override Stack RemoveFromInventory(int position) {
        Stack s = inventory[position];
        itemCounts[s.it] -= s.q;
        inventory[position] = new Stack(0);
        TriggerOnItemChanged(position, null, 0);
        return s;

    }
}
