using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    // Singleton pattern to ensure only one instance of InventoryManager exists
    public static InventoryManager Instance { get; private set; }

    [SerializeField] private Backpack backpack;  // Reference to the Backpack component
    [SerializeField] private InventoryUI inventoryUI;  // Reference to the Inventory UI component
    private List<Items> itemList = new List<Items>();  // List to store items in the inventory

    private string url = "https://wadahub.manerai.com/api/inventory/status";  // Server URL for inventory status update
    private string authToken = "Bearer kPERnYcWAY46xaSy8CEzanosAgsWM84Nx7SKM4QBSqPq6c7StWfGxzhxPfDh8MaP";  // Authorization token for API request

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Ensure only one instance of InventoryManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // Destroy this instance if another one already exists
            return;
        }
        Instance = this;  // Assign this instance to the Singleton

        // Subscribe to Backpack events
        backpack.hideInventory += Backpack_hideInventory;
        backpack.showInventory += Backpack_showInventory;
        backpack.addItem += Backpack_addItem;
        backpack.removeItem += Backpack_RemoveItem;
    }

    // Method to handle adding an item to the backpack
    private void Backpack_addItem(Items item)
    {
        AddItem(item);  // Add item to the internal list
        SendItemAction(item.itemId, "folding");  // Send action to the server (item folded into the backpack)
    }

    // Method to handle removing an item from the backpack
    private void Backpack_RemoveItem(Items item)
    {
        RemoveItem(item);  // Remove item from the internal list
        SendItemAction(item.itemId, "retrieving");  // Send action to the server (item retrieved from the backpack)
    }

    // Method to send item action (folding/retrieving) to the server
    private void SendItemAction(int itemId, string action)
    {
        // Convert the item action data to JSON format
        string jsonData = JsonUtility.ToJson(new ItemRequest(itemId, action));
        // Start a coroutine to send the POST request
        StartCoroutine(SendPostRequest(jsonData));
    }

    // Coroutine to send a POST request to the server
    private IEnumerator SendPostRequest(string jsonData)
    {
        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);  // Convert JSON string to byte array
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);  // Set the upload handler with JSON data
            www.downloadHandler = new DownloadHandlerBuffer();  // Set the download handler to receive the response
            www.SetRequestHeader("Authorization", authToken);  // Set the Authorization header with the Bearer token
            www.SetRequestHeader("Content-Type", "application/json");  // Set the Content-Type header to JSON

            // Wait for the request to complete
            yield return www.SendWebRequest();

            // Check the result of the request
            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Success: " + www.downloadHandler.text);  // Log success and the server's response
                HandleResponse(www.downloadHandler.text);  // Handle the server's response
            }
            else
            {
                Debug.LogError("Error: " + www.error);  // Log error if the request fails
            }
        }
    }

    // Method to handle the server's response
    private void HandleResponse(string response)
    {
        // You can process the response here (e.g., check for success status)
        Debug.Log("Response from server: " + response);
    }

    // Method to add an item to the inventory
    public void AddItem(Items item)
    {
        // Check if the item is valid and not already in the list
        if (item != null && !itemList.Contains(item))
        {
            itemList.Add(item);  // Add item to the list
            item.iconHoldPoint.gameObject.SetActive(true);  // Enable the item's icon in the UI
            item.GetComponent<Rigidbody>().isKinematic = true;  // Set Rigidbody to kinematic to avoid physics interaction
            item.GetComponent<MoveToTarget>().MoveObject();  // Move the item to its target position
        }
    }

    // Method to remove an item from the inventory
    private void RemoveItem(Items item)
    {
        // Check if the item is valid and exists in the list
        if (item != null && itemList.Contains(item))
        {
            itemList.Remove(item);  // Remove item from the list
            item.iconHoldPoint.gameObject.SetActive(false);  // Disable the item's icon in the UI
            item.GetComponent<Rigidbody>().isKinematic = false;  // Set Rigidbody to not kinematic to allow physics interaction
            item.GetComponent<MoveToTarget>().ReturnToOriginalPosition();  // Return the item to its original position
        }
    }

    // Event handler for showing the inventory UI
    private void Backpack_showInventory(object sender, EventArgs e)
    {
        inventoryUI.Show();  // Show the inventory UI
    }

    // Event handler for hiding the inventory UI
    private void Backpack_hideInventory(object sender, EventArgs e)
    {
        inventoryUI.Hide();  // Hide the inventory UI
    }
}

// Class to define the data structure for item request
[System.Serializable]
public class ItemRequest
{
    public int itemId;  // The ID of the item
    public string action;  // The action to be performed (e.g., "folding" or "retrieving")

    // Constructor to initialize the request
    public ItemRequest(int itemId, string action)
    {
        this.itemId = itemId;
        this.action = action;
    }
}


