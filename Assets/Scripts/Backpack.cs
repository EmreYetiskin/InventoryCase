using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    private Dictionary<ItemData,Transform>itemList= new Dictionary<ItemData,Transform>();

    private void OnCollisionEnter(Collision collision)
    {
        
       // itemList.Add(collision.gameObject.)
    }
    private void AddItem()
    {

    }
    private void RemoveItem()
    {

    }
}
