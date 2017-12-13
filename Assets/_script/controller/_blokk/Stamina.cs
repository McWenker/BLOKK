using UnityEngine;
using System.Collections;

public class Stamina : MonoBehaviour
{
	bool ground;

	int stam;

	[SerializeField]
	int maxStam;

	[SerializeField]
	int rechargeAmount;

	[SerializeField]
	float rechargeTime;

	[SerializeField]
	BarStat barST;

	bool Ground
	{
		get
		{
			return ground;
		}

		set
		{
			this.ground = value;
			if(ground)
				StartCoroutine(Recharge());
			else
				StopAllCoroutines();
		}
	}

	public int Stam
	{
		get
		{
			return stam;
		}

		set
		{
			this.stam = value;
			if(stam > maxStam)
				stam = maxStam;
			barST.CurrentVal = stam;
		}
	}

	void Update()
	{
		if(Ground != GetComponent<Grounded>().Ground)
			Ground = GetComponent<Grounded>().Ground;
		barST.MaxVal = maxStam;
	}

	IEnumerator Recharge()
	{
		while(true)
		{
			yield return new WaitForSeconds(rechargeTime);
			Stam += rechargeAmount;
		}
	}
}