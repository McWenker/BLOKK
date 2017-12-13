using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour
{
	public bool dashActive;
	public bool isDash;
	public DashState dashState = DashState.Ready;
	public float dashSpeed;
	public float dashTimer;
	public float maxDash;

	public Vector2 savedVelocity;

	Movement m;
	Rigidbody2D rb2d;
	Vector2 dash;

	void Awake ()
	{
		m = GetComponent<Movement>();
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update ()
	{
		switch (dashState)
		{
		case DashState.Ready:
			if(dashActive)
			{
				if(m.facingRight)
					dash = new Vector2(dashSpeed, 0);
				else
					dash = new Vector2(-dashSpeed, 0);
				
				Physics2D.IgnoreLayerCollision(11,12);				
				gameObject.GetComponent<BlokkAnim>().Roll();
				rb2d.AddForce(dash);
				isDash = true;
                dashActive = false;
				dashState = DashState.Dashing;
			}
			break;
		case DashState.Dashing:		
			dashTimer += Time.deltaTime * 3;
			if(dashTimer >= maxDash)
			{
				dashTimer = maxDash;
				Physics2D.IgnoreLayerCollision(11,12, false);
				dashState = DashState.Cooldown;
			}
			break;
		case DashState.Cooldown:
			isDash = false;
            m.canMove = true;
            Debug.Log("canMove = true");
            dashTimer -= Time.deltaTime;
			if(dashTimer <= 0)
			{
				dashTimer = 0;
				dashState = DashState.Ready;
			}
			break;
		}
	}

	public enum DashState
	{
		Ready,
		Dashing,
		Cooldown
	}

    public void DashActive()
    {
        if(dashState == DashState.Ready)
        {
            m.canMove = false;
            rb2d.velocity = new Vector2(0, 0);
            dashActive = true;
        }
    }

}
