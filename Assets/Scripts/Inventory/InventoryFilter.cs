using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryFilter : AbstractInventory {
    public int[] ignorePositions;
    public GameObject[] itemTypesToAllow;
    private List<ItemType> itemTypes = new List<ItemType>();
    public Inventory inventory;
    private Dictionary<int, Stack> stackMap = new Dictionary<int, Stack>();

    void Start() {
        OnItemChanged += (csharp, es, especial) => {};
        foreach (GameObject o in itemTypesToAllow) 
            itemTypes.Add(ItemTypes.Of(o));
    }
    private void OrderInventory() {
        List<Stack> stacks = new List<Stack>();
        foreach (int i in inventory.GetSlotPositions()) {
            if (ignorePositions.Contains(i))
                continue;
            if (inventory.GetSlot(i).q != 0)
            stacks.Add(inventory.GetSlot(i));
        }
        stacks.Sort((a, b) => {
            if (a.it == null)
                if (a.it == b.it)
                    return 0;
                else
                    return 1;
            if (b.it == null)
                return -1;

            if (a.it.name != b.it.name)
                return a.it.name.CompareTo(b.it.name);
            else
                return a.q.CompareTo(b.q);
        });
        List<int> kc = new List<int>(stackMap.Keys);
        kc.Sort();
        for (int i = 0; i < stackMap.Count; i++) {
            if (ignorePositions.Contains(kc[i]))
                continue;
            else {
                if (i >= stacks.Count && stackMap[kc[i]].q != 0) {
                    TriggerOnItemChanged(kc[i], null, 0);
                    stackMap[kc[i]] = new Stack(0);
                } else if ( i < stacks.Count) {
                    if (stackMap[kc[i]] != stacks[i])
                        TriggerOnItemChanged(kc[i], stacks[i].it, stacks[i].q);
                    stackMap[kc[i]] = stacks[i];
                }

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
        stackMap.TryAdd(pos, new Stack(0));
        OrderInventory();
    }

    public override bool RemoveFromInventory(ItemType type, int amount) {
        bool res = inventory.RemoveFromInventory(type, amount);
        OrderInventory();
        return res;
    }

    public override Stack RemoveFromInventory(int position) {
        if (!ignorePositions.Contains(position)) {
            Stack s = stackMap[position];
            RemoveFromInventory(s.it, s.q);
            OrderInventory();
            return s;
        }else
            return RemoveFromInventory(position);
    }
}
