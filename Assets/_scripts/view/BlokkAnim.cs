using UnityEngine;
using System.Collections;

public class BlokkAnim : MonoBehaviour
{
	public float speed = 0;

	Grounded g;
	Rigidbody2D rb2d;
	Animator anim;
    Abilities abs;

	public void Jump()
	{
		anim.SetBool("Ground", false);
	}

	public void Roll()
	{
		anim.SetTrigger("Roll");
	}

	public void Fire()
	{
		anim.SetBool("Fire 0", true);
	}

	public void EndFire()
	{
		anim.SetBool("Fire 0", false);
	}

	void Awake()
	{
		g = GetComponent<Grounded>();
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
        abs = GetComponent<Abilities>();
	}

	void FixedUpdate()
	{
		anim.SetBool("Ground", g.Ground);
        anim.SetBool("Armed", abs.weaponEquipped);
		anim.SetFloat("vSpeed", rb2d.velocity.y);
		anim.SetFloat("Speed", speed);
	}

}
