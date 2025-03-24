using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private void Start()
    {
        Hide();
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void InventoryItemActive(Transform item)
    {
        item.gameObject.SetActive(true);
    }
    public void InventoryItemInactive(Transform item)
    {
        item.gameObject.SetActive(false);
    }
}
