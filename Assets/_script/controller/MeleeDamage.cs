using UnityEngine;
using System.Collections;

public class MeleeDamage : MonoBehaviour
{
	public LayerMask hitLayer;

	Health hp;
	Shield sh;
	MeleeWeapon mAtk;

	void OnTriggerEnter2D(Collider2D col)
	{
		
		if(((1<<col.gameObject.layer) & hitLayer) != 0)
		{
			hp = col.gameObject.GetComponent<Health>();
			if (col.gameObject.GetComponentInChildren<Shield>() != null)
			{
				sh = col.gameObject.GetComponentInChildren<Shield>();
				int tempDamage = sh.TakeDamage(mAtk.Damage);
				hp.TakeDamage(tempDamage);
			}
			else
				hp.TakeDamage(mAtk.Damage);
		}
	}

	void Awake()
	{
		mAtk = GetComponentInParent<MeleeWeapon>();
	}
}
