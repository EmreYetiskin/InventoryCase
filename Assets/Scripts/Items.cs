using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


public enum ItemType
{
    fruit,
    vegetable,
    snack
}
public class Items : MonoBehaviour
{
    public string itemName;
    public int itemId;
    public float weight;
    public ItemType itemType;
    public Sprite iconSprite;
    public Transform iconHoldPoint;
    public Transform targetPoint;
}

