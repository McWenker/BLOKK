using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{	
	public ItemScriptableObject itemStat;

	public Sprite idleSprite;
	public string itemName;
	protected string itemType;

	[SerializeField]
	protected bool isEquipped;
	
	public virtual bool IsEquipped
	{
		get
		{
			return isEquipped;
		}
		
		set
		{
			this.isEquipped = value;
			if (!isEquipped && GetComponent<BoxCollider2D>())
				GetComponent<BoxCollider2D>().enabled = true;
			else if (GetComponent<BoxCollider2D>())
				GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	protected virtual void Awake()
	{
		if(itemStat != null)
		{
			itemName = itemStat.itemName;
			itemType = itemStat.itemType;
			idleSprite = itemStat.sprite;
		}
	}
	
}
