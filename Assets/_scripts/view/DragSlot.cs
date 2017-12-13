using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSlot : MonoBehaviour, IDropHandler
{
    int itemBeingReplacedUIindex;
    int replacingItemUIindex;

    Inventory inventory;
    Equipped equipped;

    [SerializeField]
    string slotType;

    public ItemScriptableObject item
    {
        get
        {
            if(transform.childCount > 0 && inventory != null)
            {
                return inventory.items[itemBeingReplacedUIindex];
            }
            else if (transform.childCount > 0 && equipped != null)
            {
                return equipped.equipped[itemBeingReplacedUIindex];
            }
            return null;
        }
    }

    void Awake()
    {
        itemBeingReplacedUIindex = Convert.ToInt32(transform.name.Replace("ItemSlot", ""));
        inventory = GetComponentInParent<Inventory>();
        equipped = GetComponentInParent<Equipped>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(!item) // there is no item in this slot, only drop
        {
            replacingItemUIindex = Convert.ToInt32(DragHandler.startParent.name.Replace("ItemSlot", "")); // find UIindex of replacingItem

            if (inventory != null) // item was dropped into Backpack
            {
                if (!DragHandler.isFromEquipment) // item came from Backpack
                {
                    inventory.AddItemAtSlot(inventory.items[DragHandler.itemBeingDraggedStartIndex], itemBeingReplacedUIindex);
                    inventory.RemoveItemAtSlot(replacingItemUIindex);
                }

                else if (DragHandler.isFromEquipment) // item came from Equipped
                {
                    inventory.AddItemAtSlot(DragHandler.itemBeingDragged.GetComponentInParent<Equipped>().equipped[DragHandler.itemBeingDraggedStartIndex], itemBeingReplacedUIindex);
                    DragHandler.itemBeingDragged.GetComponentInParent<Equipped>().RemoveEquipAtSlot(replacingItemUIindex);
                }
            }

            if (equipped != null) // item was dropped into Equipped
            {
                if (!DragHandler.isFromEquipment) // item came from Backpack
                {
                    ItemScriptableObject[] inventoryArray = DragHandler.itemBeingDragged.GetComponentInParent<Inventory>().items;
                    if (inventoryArray[DragHandler.itemBeingDraggedStartIndex].itemType == slotType)
                    {
                        equipped.AddEquipAtSlot(inventoryArray[DragHandler.itemBeingDraggedStartIndex], itemBeingReplacedUIindex);
                        DragHandler.itemBeingDragged.GetComponentInParent<Inventory>().RemoveItemAtSlot(replacingItemUIindex);
                    }
                }

                else if (DragHandler.isFromEquipment) // item came from Equipped
                {
                    ItemScriptableObject[] equippedArray = DragHandler.itemBeingDragged.GetComponentInParent<Equipped>().equipped;
                    if (equippedArray[DragHandler.itemBeingDraggedStartIndex].itemType == slotType)
                    {
                        equipped.AddEquipAtSlot(equipped.equipped[DragHandler.itemBeingDraggedStartIndex], itemBeingReplacedUIindex);
                        equipped.RemoveEquipAtSlot(replacingItemUIindex);
                    }
                }
            }
        }

        else // there is an item in this slot, drop and swap
        {
            replacingItemUIindex = Convert.ToInt32(DragHandler.startParent.name.Replace("ItemSlot", "")); // find UIindex of replacingItem

            if(inventory != null) // item was dropped into Backpack
            {
                if (!DragHandler.isFromEquipment) // item came from Backpack
                {
                    inventory.ExchangeItems(replacingItemUIindex, itemBeingReplacedUIindex);
                }
                else if (DragHandler.isFromEquipment) // item came from Equipped
                {
                    if (DragHandler.itemBeingDragged.GetComponentInParent<Equipped>().equipped[replacingItemUIindex].itemType == slotType)
                    {
                        ItemScriptableObject toUnEquip = equipped.equipped[replacingItemUIindex];
                        ItemScriptableObject toEquip = inventory.items[itemBeingReplacedUIindex];
                        inventory.RemoveItemAtSlot(itemBeingReplacedUIindex);
                        equipped.RemoveEquipAtSlot(replacingItemUIindex);
                        inventory.AddItemAtSlot(toUnEquip, itemBeingReplacedUIindex);
                        equipped.AddEquipAtSlot(toEquip, replacingItemUIindex);
                    }
                }
            }
            
            if (equipped != null) // item was dropped into Equipped
            {
                if (!DragHandler.isFromEquipment) // item came from Backpack
                {
                    Inventory temp_Inv = DragHandler.itemBeingDragged.GetComponentInParent<Inventory>();
                    if (temp_Inv.items[replacingItemUIindex].itemType == slotType)
                    {
                        ItemScriptableObject toUnEquip = equipped.equipped[itemBeingReplacedUIindex];
                        ItemScriptableObject toEquip = temp_Inv.items[replacingItemUIindex];
                        equipped.RemoveEquipAtSlot(itemBeingReplacedUIindex);
                        temp_Inv.RemoveItemAtSlot(replacingItemUIindex);
                        equipped.AddEquipAtSlot(toEquip, itemBeingReplacedUIindex);
                        temp_Inv.AddItemAtSlot(toUnEquip, replacingItemUIindex);
                    }
                }
                else if (DragHandler.isFromEquipment) // item came from Equipped
                {
                    if(equipped.equipped[replacingItemUIindex].itemType == slotType)
                    {
                        equipped.ExchangeItems(replacingItemUIindex, itemBeingReplacedUIindex);
                    }
                }
            }
        }
    }
}
