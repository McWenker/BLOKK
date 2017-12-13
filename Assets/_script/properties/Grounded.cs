using UnityEngine;
using System.Collections;

public class Grounded : MonoBehaviour
{
	public LayerMask whatIsGround;
	public Transform topLeft;
	public Transform bottomRight;

    float fallingDistance;
    [SerializeField]
    bool grounded;

    public bool Ground
    {
        get
        {
            return grounded;
        }

        set
        {
            bool oldValue = this.grounded;
            if (oldValue != value)
            {
                this.grounded = value;
                if (grounded == false)
                {
                    StartCoroutine(FallTimer());
                }
                else if (grounded == true)
                {
                    StopAllCoroutines();
                    FallingDamage(fallingDistance);
                    fallingDistance = 0;
                }
            }
        }
    }

	void FixedUpdate ()
	{
		CheckGround();
	}

	void CheckGround ()
	{
		Ground = Physics2D.OverlapArea(topLeft.position, bottomRight.position, whatIsGround);
	}

    void FallingDamage(float fallDist)
    {
        if (fallDist >= 1.5f)
        {
            Health hp = GetComponent<Health>();
            if (hp != null)
            {
                hp.TakeDamage((int)fallDist * 6);
            }
        }
    }

    IEnumerator FallTimer()
    {
        int x = 0;
        fallingDistance = x;
        while (Ground == false)
        {
            yield return new WaitForSeconds(.5f);
            x++;
            fallingDistance = (x / 2);
        }
    }
}
