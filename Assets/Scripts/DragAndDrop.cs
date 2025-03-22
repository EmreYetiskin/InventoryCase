using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 mousePosition;
    private Rigidbody rb;

    private void Start()
    {
         rb = GetComponent<Rigidbody>();
    }
    private Vector3 GetMousePos()
    {
        Vector3 mousePos = Camera.main.WorldToScreenPoint(transform.position);
        return mousePos;
    }
    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
    }
    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }
    private void OnMouseUp()
    {
        rb.AddForce(Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition));
    }
}