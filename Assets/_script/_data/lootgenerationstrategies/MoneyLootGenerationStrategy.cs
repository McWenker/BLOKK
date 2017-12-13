using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoneyLootGenerationStrategy : ILootGenerationStrategy
{
	private GameObject _money;
	public MoneyLootGenerationStrategy(GameObject money, int minWealth, int maxWealth)
	{
		_money = money;
		_money.GetComponent<Money>().minWealth = minWealth;
		_money.GetComponent<Money>().maxWealth = maxWealth;
	}

	public IEnumerable<GameObject> GenerateLoot(LootGenerationContext context)
	{
		return new List<GameObject>{_money};
	}
}
