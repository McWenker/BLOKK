using UnityEngine;
using System.Collections;

public class Stam : Stat
{
	int stamVal;
	int stamMult;
	
	int stam5;
	
	public Stam()
	{
		statName = "Stam";
	}
	
	public int StamVal
	{
		get
		{
			return stamVal;
		}
		set
		{
			this.stamVal = value;
		}
	}
	
	public int StamMult
	{
		get
		{
			return stamMult;
		}
		set
		{
			this.stamMult = value;
		}
	}
	
	public int Stam5
	{
		get
		{
			return stam5;
		}
		set
		{
			this.stam5 = value;
		}
	}
}
