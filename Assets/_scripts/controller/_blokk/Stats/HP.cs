using UnityEngine;
using System.Collections;

public class HP : Stat
{
	int hpVal;
	int hpMult;

	int hp5;
	
	public HP()
	{
		statName = "HP";
	}
	
	public int HPVal
	{
		get
		{
			return hpVal;
		}
		set
		{
			this.hpVal = value;
		}
	}
	
	public int HPMult
	{
		get
		{
			return hpMult;
		}
		set
		{
			this.hpMult = value;
		}
	}

	public int HP5
	{
		get
		{
			return hp5;
		}
		set
		{
			this.hp5 = value;
		}
	}
}
