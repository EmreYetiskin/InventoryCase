using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconToItem : MonoBehaviour
{
    [SerializeField] private Items item; 
    public Items ReturnItem()
    {
        return item;
    }
}
