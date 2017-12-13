using UnityEngine;
using System.Collections;

public class Aiming : MonoBehaviour
{
	bool rangeEquipped = false;
	Movement move;
	Transform weapTransform;
	Transform charTransform;
	[SerializeField] float aimBuffer;
	[SerializeField] Vector3 gunAngle;

	public float aimClamp;

	public void ProcessAim(Vector3 aimPoint)
	{
		// Debug.Log (aimPoint.x - charTransform.position.x);
		float bufferCheck = aimPoint.x - charTransform.position.x;
		Vector3 pos = new Vector3(aimPoint.x, aimPoint.y, 0);
		if(bufferCheck > aimBuffer || bufferCheck < -aimBuffer)
			if (pos.x > gameObject.transform.position.x)
				move.facingRight = true;
			else
				move.facingRight = false;
		if(rangeEquipped)
			AimGun(aimPoint);
	}

	// calculate aiming position based on mouse location
	void AimGun(Vector3 aim)
	{
		float AngleRad = Mathf.Atan2(aim.y - charTransform.position.y, aim.x - charTransform.position.x);
		// Get Angle in Degrees
		float AngleDeg = AngleRad * Mathf.Rad2Deg -90;
		// Rotate Object based on facing, arbitrarily adjust for some reason
		if (aim.x > charTransform.position.x)
			gunAngle = new Vector3(0, 0, AngleDeg);
		else
			gunAngle = new Vector3(0, 0, AngleDeg);
		weapTransform.rotation = Quaternion.Euler(gunAngle);
	}

	void Awake()
	{
		move = gameObject.GetComponent<Movement>();
		charTransform = gameObject.GetComponent<Transform>();
	}

	void FixedUpdate()
	{
		if (gameObject.GetComponent<Abilities>().weaponEquipped && move.canMove && GetComponentInChildren<WeaponAnim>() != null)
		{
			weapTransform = gameObject.GetComponentInChildren<WeaponAnim>().transform;
			if (gameObject.GetComponent<Abilities>().isMelee)
				rangeEquipped = false;
			else
				rangeEquipped = true;
		}
		else
			rangeEquipped = false;
	}
}