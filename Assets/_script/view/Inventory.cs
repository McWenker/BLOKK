using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inventory : MonoBehaviour
{
	public Image[] itemImages = new Image[numItemSlots];
	public ItemScriptableObject[] items = new ItemScriptableObject[numItemSlots];

	public const int numItemSlots = 20;

	public void AddItem(ItemScriptableObject itemToAdd)
	{
		for(int i = 0; i < items.Length; i++)
		{
			if(items[i] == null)
			{
				// slot is empty, add item
				items[i] = itemToAdd;
				itemImages[i].sprite = itemToAdd.sprite;
				itemImages[i].enabled = true;
				return;
			}
		}
	}

    public void AddItemAtSlot(ItemScriptableObject itemToAdd, int itemSlot)
    {
        if(items[itemSlot] == null)
        {
            items[itemSlot] = itemToAdd;
            itemImages[itemSlot].sprite = itemToAdd.sprite;
            itemImages[itemSlot].enabled = true;
            return;
        }
    }

	public void RemoveItem(ItemScriptableObject itemToRemove)
	{
		for(int i = 0; i < items.Length; i++)
		{
			if(items[i] == itemToRemove)
			{
				items[i] = null;
				itemImages[i].sprite = null;
				itemImages[i].enabled = false;
				return;
			}
		}
	}

    public void RemoveItemAtSlot(int itemSlot)
    {
        if (items[itemSlot] != null)
        {
            items[itemSlot] = null;
            itemImages[itemSlot].sprite = null;
            itemImages[itemSlot].enabled = false;
            return;
        }
    }

	public void ExchangeItems(int itemSlot1, int itemSlot2)
	{
		ItemScriptableObject temp = items[itemSlot1];
		items[itemSlot1] = items[itemSlot2];
		items[itemSlot2] = temp;
	}

    public int CheckItem(GameObject itemToCheck)
    {
        int counter = 0;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                if(itemToCheck.name == items[i].itemName)
                {
                    counter++;
                }
            }
        }
        return counter;
    }
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}
