using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSlot : Slot {
    BuildManager manager;
    public void OnInteraction() {
        Inventory.Stack s = inv.GetSlot(position);
        if (s.q == 0 || s.it.buildConstrain == ItemType.BuildConstrain.NonBuildable) return;
        manager.model = s.it.GetModel();
        manager.isWaterBuildable = s.it.buildConstrain == ItemType.BuildConstrain.WaterBuildable;
        manager.enabled = true;
    }
}
