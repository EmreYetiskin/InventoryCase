using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GraphicRaycaster raycaster; 
    [SerializeField] private EventSystem eventSystem;

    private void Awake()
    {
        Instance = this;        
    }
    public GameObject IsPointerOverUI()
    {
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = Input.mousePosition; 

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results); 

        if (results.Count > 0)
        {
            return results[0].gameObject; 
        }

        return null; 
    }
}
