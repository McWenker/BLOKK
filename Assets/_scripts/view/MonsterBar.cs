using UnityEngine;
using System.Collections;

public class MonsterBar : MonoBehaviour
{
	[SerializeField]
	BarStat bar;

	bool isShowing;

	void Update()
	{
		if (bar.CurrentVal == bar.MaxVal)
		{
			gameObject.SetActive(false);
		}
		else
		{
			gameObject.SetActive(true);
		}
	}
}
