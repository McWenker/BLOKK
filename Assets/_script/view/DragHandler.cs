using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;
    public static Transform startParent;
    public static int itemBeingDraggedStartIndex;
    public static bool isFromEquipment;

    Vector3 startPosition;

    public void OnBeginDrag(PointerEventData eventData) // when this object begins being dragged
    {
        itemBeingDragged = gameObject;
        itemBeingDraggedStartIndex = Convert.ToInt32(transform.parent.name.Replace("ItemSlot", ""));
        if (GetComponentInParent<Equipped>() != null)
            isFromEquipment = true;
        else if (GetComponentInParent<Inventory>() != null)
            isFromEquipment = false;

        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == startParent)
        {
            transform.position = startPosition;
        }
    }
}
