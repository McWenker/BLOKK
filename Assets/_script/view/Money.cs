using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour
{
	public int minWealth;
	public int maxWealth;

	int _count;

	public Money(int count)
	{
		_count = count;
	}

	public int Wealth()
	{
		return _count;
	}

	void Awake()
	{
		_count = Random.Range(minWealth, maxWealth);
	}
}