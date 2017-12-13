using UnityEngine;
using System.Collections;

public class RangedWeapon : Weapon
{	
	[SerializeField]
	Transform firingSpot;

	RangedWeaponScriptableObject rangedData;

	int ammo;

	bool canReload = true;
	GameObject bullet;
	float reloadTime;
	float spread;

	int projCount;	
	int maxAmmo;	
	float bulletSpeed;

	AudioClip reloadSound;
	public AudioSource reloadSource;

	public int Ammo
	{
		get
		{
			return ammo;
		}
		
		set
		{
			this.ammo = value;
			if (ammo > maxAmmo)
				ammo = maxAmmo;
			if (ammo <= 0)
				canFire = false;
		}
	}

	public int MaxAmmo
	{
		get
		{
			return maxAmmo;
		}
		
		set
		{
			this.maxAmmo = value;
		}
	}

	public float BulletSpeed
	{
		get
		{
			return bulletSpeed;
		}
		
		set
		{
			this.bulletSpeed = value;
		}
	}

	public IEnumerator Fire(bool facingRight, LayerMask targetLayer)
	{
		canFire = false;
		if(Ammo > 0)
		{
			Ammo -= 1;
			fireSource.Play();

			for(int i = 0; i < projCount; i++)
			{
				Quaternion thisShot;
				thisShot = transform.rotation;
                thisShot = Quaternion.Euler(0, 0, thisShot.eulerAngles.z);
				thisShot = new Quaternion(thisShot.x, thisShot.y + Random.Range(-spread, spread), thisShot.z, thisShot.w);
				GameObject bul = Instantiate(bullet, firingSpot.position, thisShot) as GameObject;
				Projectile movBul = bul.GetComponent<Projectile>();
 				movBul.Damage = Damage;
				movBul.Speed = BulletSpeed;
				movBul.hitLayer = targetLayer;
				movBul.SendMessage("Move");
			}
		}
		yield return new WaitForSeconds(Cooldown);
		canFire = true;

	}

	public IEnumerator Reload(int ammo, int availAmmo)
	{
		canReload = false;
		canFire = false;
		reloadSource.Play();
        anim.Reload();
		yield return new WaitForSeconds(reloadTime);
		int ammoXChange = Mathf.Clamp((MaxAmmo - Ammo), 0, availAmmo);
		Ammo += ammoXChange;
		availAmmo -= ammoXChange;
		canReload = true;
		canFire = true;
	}

	protected override void Awake()
	{
		base.Awake();
        
        if (weaponData.GetType() == typeof(RangedWeaponScriptableObject))
		{
			rangedData = (RangedWeaponScriptableObject)weaponData;

			ammoType = rangedData.ammoType;

			MaxAmmo = rangedData.maxAmmo;
			bullet = rangedData.bullet;
			BulletSpeed = rangedData.bulletSpeed;
			reloadTime = rangedData.reloadTime;

			spread = rangedData.spread;
			projCount = rangedData.projCount;
			reloadSource.clip = rangedData.reloadSound;

			if(IsEquipped)
				Ammo = MaxAmmo;
		}
		else
			return;
	}

    protected override void Start()
    {
        base.Start();
    }

}