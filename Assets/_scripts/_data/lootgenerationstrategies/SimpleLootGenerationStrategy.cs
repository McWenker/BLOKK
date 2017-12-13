using UnityEngine;
using System;
using System.Collections.Generic;

public class SimpleLootGenerationStrategy : ILootGenerationStrategy
{
	private GameObject _item;
	public SimpleLootGenerationStrategy(GameObject item){
		_item = item;
	}
	
	public IEnumerable<GameObject> GenerateLoot(LootGenerationContext context){
		return new List<GameObject>{_item};
	}
}