using System;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    // Event to show the inventory UI
    public event EventHandler showInventory;

    // Event to hide the inventory UI
    public event EventHandler hideInventory;

    // Action for removing an item from the backpack
    public Action<Items> removeItem;

    // Action for adding an item to the backpack
    public Action<Items> addItem;

    // Called when the backpack collides with another object
    private void OnCollisionEnter(Collision collision)
    {
        // Try to get the Items component from the collided object
        Items item = collision.gameObject.GetComponent<Items>();

        // If an item is found, invoke the addItem action
        addItem?.Invoke(item);
    }

    // Called when the mouse button is released
    private void OnMouseUp()
    {
        // Check if the pointer is over a UI element
        GameObject uiObject = UIManager.Instance.IsPointerOverUI();

        // If the pointer is over a UI object, attempt to remove the item
        if (uiObject != null)
        {
            RemoveItemFromBackpack(uiObject);
        }
        else
        {
            Debug.LogWarning("No UI element under the pointer.");  // Log a warning if no UI element is found under the pointer
        }

        // Hide the inventory after the mouse is released
        hideInventory?.Invoke(this, EventArgs.Empty);
    }

    // Called when the mouse is being dragged
    private void OnMouseDrag()
    {
        // Show the inventory UI when the mouse is dragged
        showInventory?.Invoke(this, EventArgs.Empty);
    }

    // Method to remove an item from the backpack
    private void RemoveItemFromBackpack(GameObject obj)
    {
        if (obj != null)  // Check if the object is not null
        {
            // Try to get the IconToItem component from the object
            IconToItem iconToItem = obj.GetComponent<IconToItem>();

            // If the IconToItem component is found
            if (iconToItem != null)
            {
                // Get the associated item from the IconToItem component
                Items item = iconToItem.ReturnItem();

                // If the item is valid, invoke the removeItem action
                if (item != null)
                {
                    removeItem?.Invoke(item);
                }
                else
                {
                    Debug.LogWarning("Item is null.");  // Log a warning if the item is null
                }
            }
            else
            {
                Debug.LogWarning("IconToItem component not found.");  // Log a warning if the IconToItem component is not found
            }
        }
        else
        {
            Debug.LogWarning("Object is null.");  // Log a warning if the object is null
        }
    }
}
