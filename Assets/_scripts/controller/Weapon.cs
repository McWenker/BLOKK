using UnityEngine;
using System;
using System.Collections;

public class Weapon : Item
{
	public bool canFire = true;
	public bool isMelee;
	public string ammoType;
	protected bool ignoresShield;

	protected WeaponAnim anim;

	[SerializeField]
	public WeaponScriptableObject weaponData;

	[SerializeField]
	int damage;

	float cooldown;

	public AudioSource fireSource;

	public float Cooldown
	{
		get
		{
			return cooldown;
		}
		
		set
		{
			this.cooldown = value;
		}
	}

	public int Damage
	{
		get
		{
			return damage;
		}

		set
		{
			this.damage = value;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		itemStat = weaponData;
		Damage = weaponData.damage;
		isMelee = weaponData.isMelee;
		ignoresShield = weaponData.ignoresShield;		
		Cooldown = weaponData.cooldown;
		fireSource.clip = weaponData.fireSound;
	}

    protected virtual void Start()
    {
        anim = GetComponent<WeaponAnim>();
    }

}