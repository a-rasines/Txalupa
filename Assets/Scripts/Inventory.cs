using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UIElements;
using System;

public abstract class AbstractInventory : MonoBehaviour {

    public delegate void ItemChangeEventHandler(int position, ItemType it, int quantity);
    public event ItemChangeEventHandler OnItemChanged;
    public struct Stack {
        public ItemType it;
        public int q;
        public Stack(ItemType it, int q) {
            if(it is null)
                this.q = 0;
            else
                this.q = q;
            this.it = it;
        }
        public Stack(byte _) {//Null constructor
            it = null;
            q = 0;

        }
        public override bool Equals(object o) {
            return this == o;
        }
        public static bool operator ==(Stack a, object b) {
            return typeof(Stack).IsInstanceOfType(b) && (Stack)b == a;
        }
        public static bool operator !=(Stack a, object b) {
            return !(a == b);
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
    /// Registra una nueva posición vacía en el inventario.
    /// </summary>
    /// <param name="pos"> Posicion unica del slot. Ningun formato obligatorio </param>
    public abstract void RegisterSlot(int pos);

    /// <summary>
    /// Anade el objeto al inventario en la posicion indicada
    /// </summary>
    /// <param name="position"> Posicion unica del slot en el que meterlo, previamente definido. </param>
    /// <param name="g"> GameObject asociado con el ItemType a añadir. </param>
    public abstract bool AddToInventory(int position, GameObject g);
    public abstract bool RemoveFromInventory(ItemType type, int amount);
    public abstract Stack RemoveFromInventory(int position);

    public abstract int GetAmountOf(ItemType type);
    public abstract Stack GetSlot(int position);
    public abstract Dictionary<int, Stack>.KeyCollection GetSlotPositions();

}
[Serializable]
public struct DefaultStack {
    public int position;
    public GameObject model;
    public int quantity;
}

public class Inventory : AbstractInventory {
    public DefaultStack[] initialInventory;
    private Dictionary<int, Stack> inventory = new Dictionary<int, Stack>();//Por slot
    private Dictionary<ItemType, int> itemCounts = new Dictionary<ItemType, int>();//Por material
    
    private void _(int _, ItemType __, int ___) {}
    private void Start() {
        OnItemChanged += _;
        if (!ItemTypes.isReady)
            FindObjectOfType<ItemTypes>().Start();
        foreach (DefaultStack ds in initialInventory) {
            ItemType type = ItemTypes.Of(ds.model);
            inventory[ds.position] = new Stack(type, ds.quantity);
            itemCounts.TryAdd(type, 0);
            itemCounts[type] += ds.quantity;
            TriggerOnItemChanged(ds.position, type, ds.quantity);
        }
    }
    public override void RegisterSlot(int pos) {
        inventory.TryAdd(pos, new Stack(0));
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
        ItemType it = ItemTypes.Of(g);
        Stack stack = new Stack(0);
        if (it is null)
            return false;
        int outV = 0;
        itemCounts.TryGetValue(it, out outV);
        if (!inventory.TryGetValue(position, out stack)) {//La posición está vacía
            inventory[position] = new Stack(it, it.GetCuantityFrom(g));
            itemCounts[it] = outV;
            
            TriggerOnItemChanged(position, it, it.GetCuantityFrom(g));
            return true;
        } else if(stack.it.Equals(it) && stack.q + it.GetCuantityFrom(g) <= it.stackCount){//La posición tiene el mismo objeto y no sobrepasan el máximo
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
        print(s.it.name);
        itemCounts[s.it] -= s.q;
        inventory[position] = new Stack(0);
        TriggerOnItemChanged(position, null, 0);
        return s;

    }
}
