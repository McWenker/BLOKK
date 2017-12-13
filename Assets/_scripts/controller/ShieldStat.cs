using UnityEngine;
using System.Collections;

public class ShieldStat : MonoBehaviour
{
	bool isEquipped;
	[SerializeField]
	bool recentDamage;
	int shield;

	[SerializeField]
	float damageCooldown;
	[SerializeField]
	float rechargeTime;
	[SerializeField]
	int rechargeAmount;

	[SerializeField]
	int maxShield;

	[SerializeField]
	BarStat barSH;

	public bool IsEquipped
	{
		get
		{
			return isEquipped;
		}
		
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
				barSH.Bar = GameObject.Find("UI/Canvas/Shieldbar").GetComponent<BarAnim>();
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

	public int Shield
	{
		get
		{
			return shield;
		}

		set
		{
			this.shield = value;
			if(shield > maxShield)
				shield = maxShield;
		}
	}

	public int TakeDamage(int damage)
	{
		StopAllCoroutines();
		RecentDamage = true;
		int shieldDamage = Mathf.Clamp(damage, 0, Shield);
		int tempShield = Shield;
		Shield -= shieldDamage;
		return Mathf.Clamp((damage - tempShield), 0, damage);
	}

	IEnumerator Recharge()
	{
		while(true)
		{
			yield return new WaitForSeconds(rechargeTime);
			Shield += rechargeAmount;
		}
	}

	IEnumerator ShieldCooldown()
	{
		yield return new WaitForSeconds(damageCooldown);
		RecentDamage = false;
		yield break;
	}

	void Update()
	{
		if(isEquipped)
		{
			barSH.MaxVal = maxShield;
			barSH.CurrentVal = Shield;
		}
	}
}