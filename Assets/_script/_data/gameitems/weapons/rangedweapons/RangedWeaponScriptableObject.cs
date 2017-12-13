using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Ranged Weapon", menuName = "Items/Weapon/Ranged Weapon", order = 3)]
public class RangedWeaponScriptableObject : WeaponScriptableObject
{	
	public GameObject bullet;
	
	public string ammoType;
	public int maxAmmo;
	public float reloadTime;
	public float bulletSpeed;

	public float spread;
	public int projCount;
	
	public AudioClip reloadSound;
	
	//public string damageType;
}
