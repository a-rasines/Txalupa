// Script name: InventoryVR
// Script purpose: attaching a gameobject to a certain anchor and having the ability to enable and disable it.
// This script is a property of Realary, Inc

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryVR : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject Anchor1;
    public GameObject HUD;
    public GameObject Anchor2;
    public GameObject fullInv;
    public GameObject Anchor3;
    bool UIActive;

    private void Start()
    {
        Inventory.SetActive(false);
        HUD.SetActive(false);
        fullInv.SetActive(false);
        UIActive = false;
    }

    private void Update()
    {
        // if (UnityEngine.Input.GetJoystickNames().Equals(Button.Four))
        if (Input.GetKeyDown(KeyCode.K))
        {
            UIActive = !UIActive;
            Inventory.SetActive(UIActive);
            HUD.SetActive(UIActive);
            fullInv.SetActive(UIActive);
        }
        if (UIActive)
        {
            Inventory.transform.position = Anchor1.transform.position;
            Inventory.transform.eulerAngles = new Vector3(Anchor1.transform.eulerAngles.x + 15, Anchor1.transform.eulerAngles.y, 0);

            HUD.transform.position = Anchor2.transform.position;
            HUD.transform.eulerAngles = new Vector3(Anchor2.transform.eulerAngles.x + 15, Anchor2.transform.eulerAngles.y, 0);

            fullInv.transform.position = Anchor3.transform.position;
            fullInv.transform.eulerAngles = new Vector3(Anchor3.transform.eulerAngles.x + 15, Anchor3.transform.eulerAngles.y, 0);
        }
    }
}