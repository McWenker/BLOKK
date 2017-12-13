using System;
using System.Collections.Generic;
using UnityEngine;

public interface ILootGenerationStrategy
{
	IEnumerable<GameObject> GenerateLoot(LootGenerationContext context);
}