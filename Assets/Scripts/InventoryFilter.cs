using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryFilter : AbstractInventory {
    public int[] ignorePositions;
    public GameObject[] itemTypesToAllow;
    private List<ItemType> itemTypes;
    public Inventory inventory;
    private Dictionary<int, Stack> stackMap;

    void Start() {
        OnItemChanged += (csharp, es, especial) => {};
        OrderInventory();
    }
    private void OrderInventory() {
        List<Stack> stacks = new List<Stack>();
        foreach (int i in inventory.GetSlotPositions()) {
            if (ignorePositions.Contains(i))
                continue;
            stacks.Add(inventory.GetSlot(i));
        }
        stacks.Sort((a, b) => {
            if (a.it.name != b.it.name)
                return a.it.name.CompareTo(b.it.name);
            else
                return a.q.CompareTo(b.q);
        });
        List<int> kc = new List<int>(inventory.GetSlotPositions());
        kc.Sort();
        for (int i = 0; i < Mathf.Min(stacks.Count, stackMap.Count); i++) {
            if (ignorePositions.Contains(kc[i]))
                continue;
            else {
                if (stackMap[kc[i]] != stacks[i])
                    TriggerOnItemChanged(kc[i], stacks[i].it, stacks[i].q);
                stackMap[i] = stacks[i];
            }
        }
        

    }
    public override bool AddToInventory(int position, GameObject g) {
        if(inventory.AddToInventory(position, g)) {
            OrderInventory();
            return true;
        }
        return false;
    }

    public override int GetAmountOf(ItemType type) {
        return inventory.GetAmountOf(type);
    }

    public override Stack GetSlot(int position) {
        Stack s = new Stack(0);
        stackMap.TryGetValue(position, out s);
        return s;
    }

    public override Dictionary<int, Stack>.KeyCollection GetSlotPositions() {
        return inventory.GetSlotPositions();
    }

    public override void RegisterSlot(int pos) {
        stackMap.Add(pos, new Stack(0));
    }

    public override bool RemoveFromInventory(ItemType type, int amount) {
        bool res = inventory.RemoveFromInventory(type, amount);
        OrderInventory();
        return res;
    }

    public override Stack RemoveFromInventory(int position) {
        Stack res = inventory.RemoveFromInventory(position);
        if(!ignorePositions.Contains(position))
            OrderInventory();
        return res;
    }
}
