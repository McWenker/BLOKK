using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class RepeatedLootGenerationStrategy : ILootGenerationStrategy
{
	private int _iterations;
	private ILootGenerationStrategy _strategy;
	public RepeatedLootGenerationStrategy(int iterations, ILootGenerationStrategy strategy){
		_strategy = strategy;
		_iterations = iterations;
	}
	public IEnumerable<GameObject> GenerateLoot(LootGenerationContext context){
		var results = new List<GameObject>();
		for(int i = 0; i < _iterations; i++)
		{
			results.AddRange(_strategy.GenerateLoot(context));
		}
		return results;
	}
}