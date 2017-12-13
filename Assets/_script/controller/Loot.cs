using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Loot : MonoBehaviour
{
	[SerializeField]
	GameObject drop;

	[SerializeField]
	GameObject trash1;
	
	[SerializeField]
	GameObject trash2;
	
	[SerializeField]
	GameObject green1;

	[SerializeField]
	GameObject green2;

	[SerializeField]
	GameObject money;

	[SerializeField]
	int wealthMin;

	[SerializeField]
	int wealthMax;

	public ILootGenerationStrategy DeathLootGenerator {get; private set;}
	
	/*public Loot(ILootGenerationStrategy deathLootStrategy)
	{
		DeathLootGenerator = deathLootStrategy;
	}*/



	void Awake()
	{

		var trashLoot = new List<ILootGenerationStrategy>(){ 
			new SimpleLootGenerationStrategy(trash1),
			new SimpleLootGenerationStrategy(trash2)

		};

		var greenLoot = new List<ILootGenerationStrategy>(){ 
			new SimpleLootGenerationStrategy(green1),
			new SimpleLootGenerationStrategy(green2)
		};

		DeathLootGenerator = new SequentialLootGenerationStrategy(new List<ILootGenerationStrategy>(){
			new RepeatedLootGenerationStrategy(3, new RandomLootGenerationStrategy(
				new List<ILootGenerationStrategy>(){
					new RandomLootGenerationStrategy(trashLoot),
					new NullItemGenerationStrategy()
				}
			)),
			new SimpleLootGenerationStrategy(drop),
			new RepeatedLootGenerationStrategy(5, new RandomLootGenerationStrategy(
				new List<ILootGenerationStrategy>()
				{
					new RandomLootGenerationStrategy(greenLoot),
					new MoneyLootGenerationStrategy(money, wealthMin, wealthMax),
					new NullItemGenerationStrategy(),
					new NullItemGenerationStrategy()
				}
			))
					
			
		});
	}

}
