using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] private Transform inventoryUI;
    private List<Items> itemList = new List<Items>();
    private void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Items item = collision.gameObject.GetComponent<Items>();

        if (item != null && !itemList.Contains(item))
        {
            AddItem(item);
            item.GetComponent<MoveToTarget>().MoveObject();
        }
    }
    private void AddItem(Items item)
    {
        itemList.Add(item);
        item.GetComponent<Rigidbody>().isKinematic = true;
        
        Debug.Log(itemList.Count);
        Debug.Log(itemList);
    }
    private void RemoveItem()
    {

    }
    private void OnMouseUp()
    {
        inventoryUI.gameObject.SetActive(false);
    }
    private void OnMouseDrag()
    {
        inventoryUI.gameObject.SetActive(true);
    }
}
