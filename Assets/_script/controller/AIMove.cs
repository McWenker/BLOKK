using UnityEngine;
using System.Collections;

public class AIMove : MonoBehaviour
{
	public MonsterScriptableObject monsterData;
	public bool facingRight = true;

	string monsterName;
	LayerMask aggroLayer;
	LayerMask hitLayer;
	bool isFlyer;
	float moveForce;
	float attackDistance;
	float aggroDistance;

	Rigidbody2D rb2d;
	Weapon weaponStats;
	GameObject weap;
	GameObject target;
	Animator anim;
	WeaponAnim weapAnim;
	Health hp;

	void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
		weaponStats = GetComponentInChildren<Weapon>();
		anim = GetComponent<Animator>();
		weap = weaponStats.gameObject;
		weapAnim = weap.GetComponent<WeaponAnim>();
		hp = GetComponent<Health>();

		monsterName = monsterData.monsterName;
		aggroLayer = monsterData.aggroLayer;
		hitLayer = monsterData.hitLayer;
		isFlyer = monsterData.isFlyer;
		moveForce = monsterData.speed;
		attackDistance = monsterData.attackDistance;
		aggroDistance = monsterData.aggroDistance;

		hp.MaxHP = monsterData.maxHP;
		hp.CurHP = hp.MaxHP;

	}

	void FixedUpdate()
	{
		if(!hp.dying)
		{
			if(target != null)
			{
				if(weaponStats.isMelee)
				{
					StartCoroutine(ChaseTarget());
				}
				else
				{
					StartCoroutine(ShootTarget());
				}
			}
			else
			{
				StartCoroutine(CheckForTarget());
				Idle();
			}
			if(rb2d.velocity.x > 0 && !facingRight)
				Flip();
			else if(rb2d.velocity.x < 0 && facingRight)
				Flip();
		}

		else
		{
			rb2d.velocity = new Vector2(0,0);
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		weapAnim.FacingRight = facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void AimRangedWeapon(Transform target)
	{
		Vector3 gunAngle;
		float AngleRad = Mathf.Atan2(target.position.y - gameObject.transform.position.y, target.position.x - gameObject.transform.position.x);
		// Get Angle in Degrees
		float AngleDeg = AngleRad * Mathf.Rad2Deg;
		// Rotate Object based on facing, arbitrarily adjust for some reason
		if (target.position.x > gameObject.transform.position.x)
			gunAngle = new Vector3(0, 0, AngleDeg-90);
		else
			gunAngle = new Vector3(0, 0, -AngleDeg+90);
		weaponStats.gameObject.transform.rotation = Quaternion.Euler(gunAngle);
	}

	IEnumerator ChaseTarget()
	{
		Vector2 chaseVector;
		if(isFlyer)
		{
			if((target.transform.position.x > transform.position.x) && (target.transform.position.y > transform.position.y))
				chaseVector = new Vector2(1, 1);
			else if((target.transform.position.x > transform.position.x) && (target.transform.position.y < transform.position.y))
				chaseVector = new Vector2(1, -1);
			else if((target.transform.position.x < transform.position.x) && (target.transform.position.y > transform.position.y))
				chaseVector = new Vector2(-1, 1);
			else if((target.transform.position.x < transform.position.x) && (target.transform.position.y < transform.position.y))
				chaseVector = new Vector2(-1, -1);
			else
				chaseVector = new Vector2(0, 0);
		}
		else
		{
			if(target.transform.position.x > transform.position.x)
				chaseVector = new Vector2(1, 0);
			else if(target.transform.position.x < transform.position.x)
				chaseVector = new Vector2(-1, 0);
			else
				chaseVector = new Vector2(0, 0);
		}

		rb2d.AddForce(chaseVector*moveForce);
		anim.SetFloat("Speed", Mathf.Abs(chaseVector.x));
		yield return new WaitForSeconds(0.5f);
		if((Mathf.Abs(chaseVector.x) < attackDistance) && (Mathf.Abs(chaseVector.y) < attackDistance) && weaponStats.canFire)
		{
			StartCoroutine(weap.GetComponent<MeleeWeapon>().Fire(aggroLayer));
		}
	}

	IEnumerator ShootTarget()
	{
		Vector2 aimVector;
		aimVector = new Vector2((gameObject.transform.position.x - target.transform.position.x), (gameObject.transform.position.y - target.transform.position.y));
		if((Mathf.Abs(aimVector.x) < attackDistance) && (Mathf.Abs(aimVector.y) < attackDistance))
		{
			if(aimVector.x < 0 && !facingRight)
			{
				Flip();

			}
			else if(aimVector.x > 0 && facingRight)
			{
				Flip();
			}
			yield return new WaitForSeconds(0.2f);
			AimRangedWeapon(target.transform);
			if(weaponStats.canFire)
				StartCoroutine(weap.GetComponent<RangedWeapon>().Fire(facingRight, hitLayer));
		}
	}

	IEnumerator Idle()
	{
		int roll = Random.Range(1,101);
		if(roll > 60)
		{
			Debug.Log(gameObject.name + " meandering..."); //meander around
		}
		yield return new WaitForSeconds(8f);
	}

	IEnumerator CheckForTarget()
	{
		Collider2D[] targetList = Physics2D.OverlapCircleAll(gameObject.transform.position, aggroDistance, aggroLayer);
		if(targetList.Length != 0)
		{
			target = targetList[0].gameObject;
		}
		yield return new WaitForSeconds(0.5f);
	}
}
