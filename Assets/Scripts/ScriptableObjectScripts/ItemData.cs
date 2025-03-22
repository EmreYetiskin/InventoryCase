using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Armor,
    Potion
}
[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string Name;
    public int Identifier;
    public float Weight;
    public ItemType Type;
    public Transform Prefab;
    public Sprite Icon;
}
