using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Monster", menuName = "Characters", order = 2)]
public class MonsterScriptableObject : ScriptableObject
{
	public string monsterName;

	public float speed;

	public bool isFlyer;
	public bool isMelee;
	public bool givesXP;

	public int maxHP;
	public int xp;

	public LayerMask aggroLayer;
	public LayerMask hitLayer;
	public float aggroDistance;
	public float attackDistance;

	//public ILootGenerationStrategy lootList;
}
