using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	public bool dieOnWall;
	public bool explosive;
	public LayerMask hitLayer;
	public LayerMask explodeOn;

	float speed;
	int damage;
	Animator anim;
	float moveX;
	float moveY;
	Rigidbody2D rb2d;
	Health hp;
	Shield sh;

	[SerializeField]
	bool isDot;

	[SerializeField]
	float cleanTime;

	//[SerializeField]
	//string DoTtype;

	[SerializeField]
	int Dotdamage;

	[SerializeField]
	int Dotduration;

	public float Speed
	{
		set
		{
			this.speed = value;
		}
	}

	public int Damage
	{
		set
		{
			this.damage = value;
		}
	}

	public IEnumerator Clean()
	{
		yield return new WaitForSeconds(cleanTime);
		if(!explosive)
			Destroy(gameObject);
		else
		{
			Boom();
		}
	}

	public void QuickClean()
	{
		Destroy(gameObject);
	}

	// projectile hits something
	void OnTriggerEnter2D(Collider2D col)
	{
		// any non explosive
		if(!explosive)
		{
			// projectile hits Ground layer
			if(col.gameObject.layer == 8)
			{
				// lasers, etc
				if(dieOnWall)
					Destroy(gameObject);
				// slag, could also work for arrows, etc
				else
				{
					rb2d.velocity = new Vector2(0, 0);
					damage = 0;
					anim.SetTrigger("Burnout");
				}
			}
			// hit an enemy
			if(((1<<col.gameObject.layer) & hitLayer) != 0)
			{
				//TODO deal damage class
				hp = col.gameObject.GetComponent<Health>();
				if (col.gameObject.GetComponentInChildren<Shield>() != null)
				{
					if(isDot)
					{
						hp.TakeDoT(Dotdamage, Dotduration);
					}
					sh = col.gameObject.GetComponentInChildren<Shield>();
					int tempDamage = sh.TakeDamage(damage);
					hp.TakeDamage(tempDamage);
				}
				else
				{
					if(isDot)
					{
						hp.TakeDoT(Dotdamage, Dotduration);
					}
					hp.TakeDamage(damage);
				}
				Destroy(gameObject);
			}
		}

		// rockets, missiles, etc
		else if(((1<<col.gameObject.layer) & explodeOn) != 0)
		{
			//It matched one
			Boom();
		}
	}

	void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		Move();
		StartCoroutine(Clean());
	}


	void Boom()
	{
		rb2d.velocity = new Vector2(0,0);
		Explode boom = GetComponentInChildren<Explode>();
		boom.Damage = damage;
		StartCoroutine(boom.Boom());
	}

	void Move()
	{
		rb2d.AddForce(transform.up*speed);
	}
}
