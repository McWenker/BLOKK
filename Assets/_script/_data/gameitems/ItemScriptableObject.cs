using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Item", menuName = "Items/GameItem", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
	public string itemName;
	public string itemType;
	public string itemDesc;
	public Color itemColor;
	public Sprite sprite;

	public bool isUnique;
	public bool canStack;
	public bool destroyOnUse;
	
	public GameObject itemObject;
	
	public bool isForQuest;
	public int questID;

	public bool canSell;
	public int moneyValue;
}
