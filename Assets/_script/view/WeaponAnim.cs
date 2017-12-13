using UnityEngine;
using System.Collections;

public class WeaponAnim : MonoBehaviour
{
	public Vector3 WeapPos;
	[SerializeField]
	bool facingRight = true;

	bool equipped;
	Animator anim;

	public bool FacingRight
	{
		set
		{
			this.facingRight = value;
			if(facingRight)
			{
				Vector3 newPos = new Vector3(transform.position.x,transform.position.y,-2f);
				transform.position = newPos;
			}
			else
			{
				Vector3 newPos = new Vector3(transform.position.x,transform.position.y,1f);
				transform.position = newPos;
			}
		}
	}
	
	public void Fire()
	{
		anim.SetTrigger("Fire");
	}

	public void Reload()
	{
		anim.SetTrigger("Reload");
	}

	// lets the weapon know it is equipped, attaches it to parent's Movement if so
	// returns the weapon's positioning offset for appearances
	public Vector3 Equip()
	{
		equipped = true;
        anim = GetComponent<Animator>();
        return WeapPos;
	}

	void Awake()
	{
		anim = GetComponent<Animator>();
	}
}
