using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggler : MonoBehaviour{
    // Start is called before the first frame update
    public bool initialState = false;
    public bool state { get { return initialState; } set { initialState = value; } }
    public static void ToggleVisibility(GameObject g) {
        g.SetActive(!g.active);
    }
    public void ToggleScript(MonoBehaviour mb) {
        mb.enabled = !mb.enabled;
    }
    public void VisibilityToState(GameObject g) {
        g.SetActive(state);
    }
    public void ScriptToState(MonoBehaviour m) {
        m.enabled = state;
    }
    public void ToggleState() {
        state = !state;
    }
}
