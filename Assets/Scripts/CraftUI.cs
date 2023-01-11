using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftUI : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private TextMeshProUGUI requirementsText;
    [SerializeField] private Button craftButton;

    private Slot[] craftSlots;
    private ToggleGroup toggleGroup;
    private Crafteable selectedCrafteable;

}
