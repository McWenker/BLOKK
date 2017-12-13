using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class RandomLootGenerationStrategy : ILootGenerationStrategy
{
	private IEnumerable<ILootGenerationStrategy> _strategies;
	private Random _random;
	
	public RandomLootGenerationStrategy(IEnumerable<ILootGenerationStrategy> strategies)
	{
		_strategies = strategies;
	}
	
	public IEnumerable<GameObject> GenerateLoot(LootGenerationContext context)
	{
		var result = new List<GameObject>();
		int stratCount = _strategies.Count();
		ILootGenerationStrategy strategy = _strategies.ElementAt(Random.Range(0,stratCount));
		result.AddRange(strategy.GenerateLoot(context));
		return result;
	}
}
