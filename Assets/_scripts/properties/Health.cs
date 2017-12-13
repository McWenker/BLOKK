using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
	public bool dying = false;
	[SerializeField]
	float maxHP;
	float curHP;
	bool invuln;
	Animator anim;

	[SerializeField]
	bool isPlayer;

	[SerializeField]
	BarStat barHP;

	public float MaxHP
	{
		get
		{
			return maxHP;
		}

		set
		{
			this.maxHP = value;
		}
	}

	public float CurHP
	{
		get
		{
			return curHP;
		}

		set
		{
			this.curHP = value;
			if(curHP > maxHP)
				curHP = maxHP;
			if(curHP < 0)
				curHP = 0;
		}
	}

	void Awake()
	{		
		CurHP = maxHP;
		anim = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		if(CurHP <= 0 && !dying)
		{
			dying = true;
			Die();
		}
		barHP.MaxVal = MaxHP;
		barHP.CurrentVal = CurHP;
	}

	void Die()
	{
		if(GetComponent<Movement>() == null)
		{
			GetComponent<BoxCollider2D>().enabled = false;
			GetComponent<CircleCollider2D>().enabled = false;
		}
		else
		{
			anim.SetBool("Dead", true);
			GetComponent<BlokkLives>().Lives--;
		}
		anim.SetTrigger("DED");
	}

	IEnumerator DoT(int damage, int duration)
	{
		while (true)
		{
			int x;
			for(x = 0; x < duration; x++)
			{
				yield return new WaitForSeconds(1f);
				TakeDamage((float)damage);
			}
			yield return new WaitForSeconds(duration);
			x = 0;
			yield break;
		}
	}

	IEnumerator Invuln(int duration)
	{
		while (true)
		{
			invuln = true;
			yield return new WaitForSeconds(duration);
			invuln = false;
			yield break;
		}
	}

	public void TakeDamage(float dam)
	{
		if(!invuln)
		{
			CurHP -= dam;
			if(CurHP <= 0 && !dying)
			{
				dying = true;
				if(GetComponent<Loot>() != null)
				{
					IEnumerable<GameObject> itemsToSpawn = GetComponent<Loot>().DeathLootGenerator.GenerateLoot(null);
					foreach(GameObject item in itemsToSpawn)
					{
						Instantiate(item, new Vector3((transform.position.x + Random.Range(-1.5f,1.5f)), transform.position.y), Quaternion.identity);
					}
				}
				Die();
			}
			else
			{
				anim.SetTrigger("Ow");
				if(isPlayer)
					StartCoroutine(Invuln(1));
			}
		}
		else
			return;
	}

	public void TakeDoT(int damage, int duration)
	{
		StartCoroutine(DoT(damage, duration));
	}

	public void Decay()
	{
		Destroy(gameObject);
	}

}
