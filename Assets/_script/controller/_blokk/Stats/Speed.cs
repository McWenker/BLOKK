using UnityEngine;
using System.Collections;

public class Speed : Stat
{
	float speedVal;
	float speedMult;
	
	public Speed()
	{
		statName = "Speed";
	}
	
	public float SpeedVal
	{
		get
		{
			return speedVal;
		}
		set
		{
			this.speedVal = value;
		}
	}

	public float SpeedMult
	{
		get
		{
			return speedMult;
		}
		set
		{
			this.speedMult = value;
		}
	}
}
