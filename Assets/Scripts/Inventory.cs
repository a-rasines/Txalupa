using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    private Dictionary<int, Stack> inventory = new Dictionary<int, Stack>();
    public Dictionary<ItemType, int> itemCounts = new Dictionary<ItemType, int>();
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
