using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using static ItemType;

public class ItemTypes : MonoBehaviour {
    public GameObject[] gameObjects;
    public Sprite[] sprites;
    public static bool isReady = false;
    private static List<ItemType> items = null;
    private GameObject GetGameObject(string name) {
        foreach (GameObject g in gameObjects)
            if (g.name == name)
                return g;
        throw new NullReferenceException(""+gameObjects.Count());
    }
    private Sprite GetSprite(string name) {
        foreach (Sprite s in sprites) {
            if (s.name == name) {
                return s;
            }
        }
        throw new NullReferenceException();
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
    public void Start() {
        if (items == null)
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
                }, BuildConstrain.WaterBuildable),
                new ItemType("Lanza", 1, GetSprite("Item_WoodSpear"), new Dictionary<GameObject, int>
                {
                    {GetGameObject("WoodSpear.L"),1 }
                })
            };
        isReady = true;
    }
}
public class ItemType{
    
    public enum BuildConstrain {
        NonBuildable,
        WaterBuildable,
        GridBuildable,
        WallBuildable
    }

    private Dictionary<GameObject, int> go;
    public readonly int stackCount;
    public readonly Sprite icon;
    public new readonly string name;
    public readonly int buildable;
    public readonly BuildConstrain buildConstrain;
    public int GetCuantityFrom(GameObject g) {
        try {
            foreach (GameObject g1 in go.Keys) {
                MeshFilter f1 = null;
                g1.TryGetComponent<MeshFilter>(out f1);
                if (f1 == null) f1 = g1.GetComponentInChildren<MeshFilter>();
                MeshFilter f2 = null; 
                g.TryGetComponent<MeshFilter>(out f2);
                if (f2 == null) f2 = g.GetComponentInChildren<MeshFilter>();
                if (f1.sharedMesh == f2.sharedMesh) {
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
        return go.Keys.First();
    }
    public ItemType(string name, int stackCount, Sprite icon, Dictionary<GameObject, int> go) {
        this.stackCount = stackCount;
        this.icon = icon;
        this.go = go;
        this.name = name;
        this.buildConstrain = BuildConstrain.NonBuildable;
    }
    public ItemType(string name, int stackCount, Sprite icon, Dictionary<GameObject, int> go, BuildConstrain buildConstrain) {
        this.stackCount = stackCount;
        this.icon = icon;
        this.go = go;
        this.name = name;
        this.buildConstrain = buildConstrain;
    }
    public override bool Equals(object other) {
        return this == other;
    }
    public static bool operator ==(ItemType a, object b) {
        return typeof(ItemType).IsInstanceOfType(b) &&  a == (ItemType)b;
    }
    public static bool operator !=(ItemType a, object b) {
        return !(a == b);
    }
    public static bool operator ==(ItemType a, ItemType b) {
        try {
            return a is null && b is null || a is not null && b is not null & a.name == b.name;
        } catch {
            return false;
        }
    }
    public static bool operator !=(ItemType a, ItemType b) {
        return !(a == b);
    }
}
