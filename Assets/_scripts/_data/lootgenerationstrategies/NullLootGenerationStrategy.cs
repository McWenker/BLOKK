using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class NullItemGenerationStrategy : ILootGenerationStrategy
{
	public IEnumerable<GameObject> GenerateLoot(LootGenerationContext context)
	{
		return new List<GameObject>{};
	}
}