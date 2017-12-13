using UnityEngine;
using System.Collections;

public class GrassblockAnim : MonoBehaviour
{
	public float delay;

	Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		StartCoroutine(Wind());
	}

	IEnumerator Wind()
	{
		int roll = Random.Range(0,1000);
		if(roll == 999)
		{
			anim.SetTrigger("Wind");
		}
		yield return new WaitForSeconds(delay);
	}
}
