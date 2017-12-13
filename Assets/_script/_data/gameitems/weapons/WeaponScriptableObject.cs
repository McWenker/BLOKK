using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapons/Weapon", order = 1)]
public class WeaponScriptableObject : ItemScriptableObject
{	
	public bool isMelee;

	public float cooldown;

	public int damage;

	public AudioClip fireSound;

	public bool ignoresShield;

	//public string damageType;
}
