using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSlot : Slot {
    public BuildManager manager;
    public new void OnInteraction() {
        Inventory.Stack s = inv.GetSlot(position);
        if (s.q == 0 || s.it.buildConstrain == ItemType.BuildConstrain.NonBuildable) return;
        manager.model = s.it.GetModel();
        manager.buildConstrain = s.it.buildConstrain;
        manager.enabled = true;
    }
}
