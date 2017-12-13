using UnityEngine;
using System.Collections;

public class Armor : Stat
{
	int armorVal;
	int armorMult;

	public Armor()
	{
		statName = "Armor";
	}

	public int ArmorVal
	{
		get
		{
			return armorVal;
		}
		set
		{
			this.armorVal = value;
		}
	}

	public int ArmorMult
	{
		get
		{
			return armorMult;
		}
		set
		{
			this.armorMult = value;
		}
	}
}
