using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CraftSlot : MonoBehaviour
{
    public Crafteable CrafteablePrefab => crafteablePrefab;

    public Toggle Toggle { get; private set; }

    [SerializeField] public Crafteable crafteablePrefab;
    private TextMeshProUGUI label;

    private void Awake()
    {
        Toggle = gameObject.GetComponentInChildren<Toggle>();
        label = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        label.text = crafteablePrefab.GetLabel();
    }
}
