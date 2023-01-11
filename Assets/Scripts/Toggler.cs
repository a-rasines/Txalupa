using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggler : MonoBehaviour{
    // Start is called before the first frame update
    public static void ToggleVisibility(GameObject g) {
        g.SetActive(!g.active);
    }
    public void ToggleScript(MonoBehaviour mb) {
        mb.enabled = !mb.enabled;
    }
}
