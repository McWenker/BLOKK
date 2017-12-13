using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Shield", order = 1)]
public class ShieldScriptableObject : ItemScriptableObject
{
	public float damageCooldown;
	public float rechargeTime;
	public int rechargeAmount;	
	public int maxShield;
}
