using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explode : MonoBehaviour
{
	public float radius;
	public float duration;
	public float knockBackForce = 1000f;
	public AudioSource boomSFX;

	[SerializeField]
	LayerMask hitLayer;

	int d;
	List<GameObject> hitList = new List<GameObject>();
	Health tarHP;
	Animator parentAnim;
	float r;
	public bool boom;

	public int Damage
	{
		set
		{
			this.d = value;
		}
	}

	public IEnumerator Boom()
	{
		if(!boom)
		{
			boom = true;
			CircleCollider2D circle = GetComponent<CircleCollider2D>();
			circle.enabled = true;
			yield return new WaitForSeconds(duration/8);
			boomSFX.Play();
			parentAnim.SetTrigger("BOOM");
			circle.radius = radius;
			DealDamage(d);
		}
	}

	void Awake()
	{
		parentAnim = GetComponentInParent<Animator>();
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if(((1<<col.gameObject.layer) & hitLayer) != 0)
		{
			AddTarget(col.gameObject);
		}
		if(col.gameObject.CompareTag("BOOM") && (col.gameObject != gameObject.transform.parent))
		{
			StartCoroutine(col.gameObject.GetComponentInChildren<Explode>().Boom());
		}

	}

	void AddTarget(GameObject target)
	{
		if(!hitList.Contains(target))
			hitList.Add(target);
	}

	void DealDamage(float damage)
	{
		foreach (GameObject t in hitList)
		{
			tarHP = t.GetComponent<Health>();
			tarHP.TakeDamage(damage);
			Vector3 knockBack = gameObject.transform.parent.position - t.transform.position;
			Rigidbody2D toKnockBack = t.GetComponent<Rigidbody2D>();
			toKnockBack.AddForce(new Vector2(-knockBack.x * knockBackForce, 500f));
		}
		hitList.Clear();
	}
}
