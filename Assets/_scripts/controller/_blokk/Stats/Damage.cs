using UnityEngine;
using System.Collections;

public class Damage : Stat
{
	int damageVal;
	int damageMult;
	
	public Damage()
	{
		statName = "Damage";
	}
	
	public int DamageVal
	{
		get
		{
			return damageVal;
		}
		set
		{
			this.damageVal = value;
		}
	}

	public int DamageMult
	{
		get
		{
			return damageMult;
		}
		set
		{
			this.damageMult = value;
		}
	}
}
