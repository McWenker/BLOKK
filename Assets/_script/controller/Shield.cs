using UnityEngine;
using System.Collections;

public class Shield : Item
{
	[SerializeField]
	bool recentDamage;
	int shieldPoints;

	float damageCooldown;
	float rechargeTime;
	int rechargeAmount;
	
	int maxShield;

	public ShieldScriptableObject shieldData;

	public override bool IsEquipped
	{
		set
		{
			this.isEquipped = value;
			if (!isEquipped && GetComponent<BoxCollider2D>())
			{
				GetComponent<BoxCollider2D>().enabled = true;
				GetComponent<SpriteRenderer>().enabled = true;
			}
			else if (GetComponent<BoxCollider2D>())
			{
				GetComponent<BoxCollider2D>().enabled = false;
				GetComponent<SpriteRenderer>().enabled = false;
				StartCoroutine(Recharge());
			}
		}
	}

	public bool RecentDamage
	{
		get
		{
			return recentDamage;
		}

		set
		{
			this.recentDamage = value;
			if(recentDamage)
			{
				StopAllCoroutines();
				StartCoroutine(ShieldCooldown());
			}
			else
				StartCoroutine(Recharge());
		}
	}

	public int MaxShield
	{
		get
		{
			return maxShield;
		}

		set
		{
			this.maxShield = value;
		}
	}

	public int ShieldPoints
	{
		get
		{
			return shieldPoints;
		}

		set
		{
			this.shieldPoints = value;
			if(shieldPoints > maxShield)
				shieldPoints = maxShield;
		}
	}

	public int TakeDamage(int damage)
	{
		StopAllCoroutines();
		RecentDamage = true;
		int shieldDamage = Mathf.Clamp(damage, 0, shieldPoints);
		int tempShield = shieldPoints;
		shieldPoints -= shieldDamage;
		return Mathf.Clamp((damage - tempShield), 0, damage);
	}

	IEnumerator Recharge()
	{
		while(true)
		{
			yield return new WaitForSeconds(rechargeTime);
			ShieldPoints += rechargeAmount;
		}
	}

	IEnumerator ShieldCooldown()
	{
		yield return new WaitForSeconds(damageCooldown);
		RecentDamage = false;
		yield break;
	}

	protected override void Awake()
	{
		base.Awake();
		MaxShield = shieldData.maxShield;
		shieldPoints = MaxShield;
		damageCooldown = shieldData.damageCooldown;
		rechargeTime = shieldData.rechargeTime;
		rechargeAmount = shieldData.rechargeAmount;
	}
}