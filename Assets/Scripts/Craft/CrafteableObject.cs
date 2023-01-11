using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrafteableObject : MonoBehaviour
{
    public new string name;
    [SerializeField] private GameObject crafteablePrefab;
    [SerializeField] private int woodRequirements;
    [SerializeField] private int plasticRequirements;
    [SerializeField] private int scrapRequirements;
    [SerializeField] private int leavesRequirements;
    public int GetWood()
    {
        return woodRequirements;
    }
    public GameObject GetPrefab()
    {
        return crafteablePrefab;
    }
    public int GetPlastic()
    {
        return plasticRequirements;
    }
    public int GetScrap()
    {
        return scrapRequirements;
    }
    public int GetLeaves()
    {
        return leavesRequirements;
    }
}
