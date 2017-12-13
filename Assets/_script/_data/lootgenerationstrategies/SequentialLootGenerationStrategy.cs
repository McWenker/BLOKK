using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SequentialLootGenerationStrategy : ILootGenerationStrategy
{
	private IEnumerable<ILootGenerationStrategy> _strategies;
	public SequentialLootGenerationStrategy(IEnumerable<ILootGenerationStrategy> strategies){
		_strategies = strategies;
	}
	public IEnumerable<GameObject> GenerateLoot(LootGenerationContext context){
		var results = new List<GameObject>();
		foreach (var strategy in _strategies)
		{
			results.AddRange(strategy.GenerateLoot(context));
		}
		return results;
	}
}