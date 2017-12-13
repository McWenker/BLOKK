using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon
{
	MeleeDamage md;
	Health hp;
	MeleeWeaponScriptableObject meleeData;

	public IEnumerator Fire(LayerMask targetLayer)
	{
		md.hitLayer = targetLayer;
		canFire = false;
		fireSource.Play();
		anim.Fire();



		yield return new WaitForSeconds(Cooldown);
		canFire = true;

	}

	protected override void Awake()
	{
		base.Awake();
		md = GetComponentInChildren<MeleeDamage>();
		if(weaponData.GetType() == typeof(MeleeWeaponScriptableObject))
		{
			// melee specific
			meleeData = (MeleeWeaponScriptableObject)weaponData;
		}
		else
			return;
	}

    protected override void Start()
    {
        base.Start();
    }

}