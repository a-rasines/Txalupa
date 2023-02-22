using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemType : MonoBehaviour {
    public GameObject[] gameObjects;
    public Sprite[] sprites;
    private static List<ItemType> items;
    private GameObject GetGameObject(string name) {
        foreach(GameObject g in gameObjects) {
            if (g.name.Equals(name))
                return g;
        }
        throw new NullReferenceException();
    }
    private Sprite GetSprite(string name) {
        foreach(Sprite s in sprites) {
            if(s.name.Equals(name)) {
                return s;
            }
        }
        throw new NullReferenceException();
    }
    void Start() {
        items = new List<ItemType>{
            new ItemType("Plank", 20, GetSprite("Item_Plank"), new Dictionary<GameObject, int>{
                { GetGameObject("plank1"), 1 },
                { GetGameObject("plank2"), 1 }
            }),
            new ItemType("Plastic", 20, GetSprite("Item_Plastic"), new Dictionary<GameObject, int>{
                { GetGameObject("Plastic2"), 1 },
                { GetGameObject("Plastic3"), 1 },
                { GetGameObject("Plastic6"), 1 }
            }),
            new ItemType("Scrap", 20, GetSprite("Item_Scrap"), new Dictionary<GameObject, int>{
                { GetGameObject("Scrap1"), 1 },
                { GetGameObject("Scrap2"), 1 },
                { GetGameObject("Scrap3"), 1 },
                { GetGameObject("Scrap4"), 1 }
            }),
            new ItemType("Thatch", 20, GetSprite("Item_Thatch"), new Dictionary<GameObject, int>{
                { GetGameObject("Thatch1"), 1 },
                { GetGameObject("Thatch2"), 1 },
                { GetGameObject("Thatch3"), 1 },
                { GetGameObject("Thatch4"), 3 }
            }),
            new ItemType("Suelo",20, GetSprite("BuildingIcon_FoundationTier3"), new Dictionary<GameObject, int>
            {
                {GetGameObject("Block_Foundation_Tier3"), 1 }
            }),
            new ItemType("Lanza", 1, GetSprite("Item_WoodSpear"), new Dictionary<GameObject, int>
            {
                {GetGameObject("WoodSpear.L"),1 }
            })
        };
    }
    private Dictionary<GameObject, int> go;
    public readonly int stackCount;
    public readonly Sprite icon;
    public new readonly string name;
    public int GetCuantityFrom(GameObject g) {
        try {
            foreach (GameObject g1 in go.Keys) {
                Mesh f1 = g1.GetComponent<MeshFilter>().mesh;
                if (f1 == null) f1 = g1.GetComponentInChildren<MeshFilter>().mesh;
                Mesh f2 = g.GetComponent<MeshFilter>().mesh;
                if (f2 == null) f2 = g.GetComponentInChildren<MeshFilter>().mesh;
                if (f1 == f2) {
                    if (g.GetComponent<ItemStack>() != null)
                        return g.GetComponent<ItemStack>().quantity;
                    return go[g1];
                }
            }
            return 0;
        } catch {
            return 0;
        }
    }
    public GameObject GetModel() {
        return go.GetEnumerator().Current.Key;
    }
    private ItemType(string name, int stackCount, Sprite icon, Dictionary<GameObject, int> go) {
        this.stackCount = stackCount;
        this.icon = icon;
        this.go = go;
        this.name = name;
    }
    public bool Equals(ItemType other) {
        return this.name == other.name;
    }
    public static ItemType Of(GameObject go) {
        foreach (ItemType it in items) {
            if (it.GetCuantityFrom(go) != 0) {
                return it;
            }
        }
        return null;
    }
    public static ItemType Of(string name) {
        foreach (ItemType it in items) {
            if (it.name == name) {
                return it;
            }
        }
        return null;
    }
}
