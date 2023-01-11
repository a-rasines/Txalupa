using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    private Dictionary<int, Stack> inventory = new Dictionary<int, Stack>();//Por slot
    private Dictionary<ItemType, int> itemCounts = new Dictionary<ItemType, int>();//Por material
    public struct Stack{
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
    }
    public int GetAmountOf(ItemType type) {
        int outV = 0;
        itemCounts.TryGetValue(type, out outV);
        return outV;
    }
    public bool RemoveFromInventory(ItemType type, int amount) {
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
    public bool AddToInventory(int position, GameObject g) {
        ItemType it = ItemType.Of(g);
        Stack stack = new Stack(0);
        int outV = 0;
        itemCounts.TryGetValue(it, out outV);
        if (it is null)
            return false;
        if(!inventory.TryGetValue(position, out stack)) {//La posición está vacía
            inventory[position] = new Stack(it, it.GetCuantityFrom(g));
            itemCounts[it] = outV;
            return true;
        } else if(stack.it.Equals(it) && stack.q + it.GetCuantityFrom(g) <= it.stackCount){//La posición tiene el mismo objeto y no sobrepasan el máximo
            stack.q += it.GetCuantityFrom(g);
            inventory[position] = stack;
            itemCounts[it] = outV;
            return true;
        } else {
            return false;
        }
    }
    public Stack GetSlot(int position) {
        Stack s = new Stack(0);
        inventory.TryGetValue(position, out s);
        return s;
    }
    public Stack RemoveFromInventory(int position) {
        Stack s = inventory[position];
        itemCounts[s.it] -= s.q;
        inventory.Remove(position);
        return s;

    }
}
