using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private ItemData itemSO;

    public string GetName()
    {
       return itemSO.Name;
    }
}
