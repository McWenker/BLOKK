using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	public float maxSpeed = 10f;
	public float jumpForce;
	public float airDrag;
	public bool canMove = true;
	public bool facingRight = true;
	public Dash isDashing;
	public bool moveSwitch = true;

	public bool thruFloor;
	
	Grounded g;
	Rigidbody2D rb2d;
	BlokkAnim ba;
	WeaponAnim weapAnim;
	BoxCollider2D boxCol;
	CircleCollider2D cirCol;

	[SerializeField]
	Vector3 velocity;

	public bool ThruFloor
	{
		get
		{
			return thruFloor;
		}

		set
		{
            if(this.thruFloor != value) // value has changed
            {
                this.thruFloor = value;
                if (thruFloor)
                {
                    StartCoroutine(ThruFloorMinimum());
                }
            }
            
            /*StartCoroutine(ThruFloorMinimum());
            if (thruFloor && g.grounded && rb2d.velocity.y < 0)
            {
                rb2d.AddForce(new Vector2(0, -50f));
            }*/
		}
	}

	public void ProcessMovement(float m)
	{
		if(moveSwitch){
			if(!isDashing.isDash)
			{
				Flip();
				if(canMove)
                {
                    ba.speed = Mathf.Abs(m);
                    if (g.Ground)
						rb2d.velocity = new Vector2(m * maxSpeed, rb2d.velocity.y);
					else
						rb2d.velocity = new Vector2((m * maxSpeed / airDrag), rb2d.velocity.y);
				}
                else
                {
                    ba.speed = 0;
                }
			}
		}
	}

	public void Jump()
	{
		rb2d.AddForce(new Vector2(0, jumpForce));
	}

	void Awake()
	{
		g = GetComponent<Grounded>();
		rb2d = GetComponent<Rigidbody2D>();
		ba = GetComponent<BlokkAnim>();
		weapAnim = GetComponentInChildren<WeaponAnim>();
		isDashing = GetComponent<Dash>();
	}
	

	void Update()
	{
		StartCoroutine(CheckWeapAnim());
		velocity = rb2d.velocity;
	}

	void Flip() // Flips player based on mouse's position
	{
		Vector3 theScale = transform.localScale;
		
		if (facingRight)
		{
			theScale.x = 1;
			transform.localScale = theScale;
			if(weapAnim != null)
				weapAnim.FacingRight = true;
		}
		
		else
		{
			theScale.x = -1;
			transform.localScale = theScale;
			if(weapAnim != null)
				weapAnim.FacingRight = false;
		}
		
	}

	IEnumerator CheckWeapAnim()
	{		
		weapAnim = GetComponentInChildren<WeaponAnim>();
		yield return new WaitForSeconds(0.5f);
	}

    IEnumerator ThruFloorMinimum()
    {
        Physics2D.IgnoreLayerCollision(12, 17, true);
        yield return new WaitForSeconds(0.33f);
        Physics2D.IgnoreLayerCollision(12, 17, false);
        thruFloor = false;
    }
}
