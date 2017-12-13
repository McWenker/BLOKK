using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
	float move;
	float down;
	Vector3 aim;
	Movement m;
	Aiming a;
	Abilities abs;
	public InventoryDisplay invDis;

	bool invState = false;
	public bool canInput = true;

	void Awake()
	{
		m = GetComponent<Movement>();
		a = GetComponent<Aiming>();
		abs = GetComponent<Abilities>();
	}

	void FixedUpdate()
	{
		if(canInput)
		{
			if(!invState)
			{
				move = Input.GetAxis("Horizontal");
				aim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				m.ProcessMovement(move);
				a.ProcessAim(aim);
			}
			else
			{
				// inventory processing here
			}
		}
	}

	void Update()
	{	
		if(canInput)
		{
			if(Input.GetKeyDown(KeyCode.I))
			{
				if(!invState)
				{
					invState = true;
					invDis.Display = true;
				}
				else
				{
					invState = false;
					invDis.Display = false;
                }
			}
			
			if(!invState)
			{
				if(Input.GetKeyDown(KeyCode.E))
				{
					abs.Interact();
				}

                if (Input.GetKey(KeyCode.S))
                {
                    m.ThruFloor = true;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    abs.Jump();
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    abs.Roll();
                }

				if(Input.GetKey(KeyCode.Mouse0))
				{
					abs.Fire();
				}
				else
					abs.EndFire();

				if(Input.GetKey(KeyCode.R))
				{
					abs.Reload();
				}

                if(Input.GetKeyDown(KeyCode.Alpha1))
                {
                    abs.Weapon1Wielded = true;
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    abs.Weapon1Wielded = false;
                }
            }
		}
	}
}
