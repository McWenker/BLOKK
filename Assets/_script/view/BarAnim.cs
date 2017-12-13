using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BarAnim : MonoBehaviour
{	
	float fillAmount;

	[SerializeField]
	Image content;

	[SerializeField]
	Text valueText;

	public float MaxValue { get; set; }

	public float Value
	{
		set
		{
			fillAmount = CalculateFill(value, 0, MaxValue, 0, 1);
            if (valueText && value > 0)
                valueText.text = value + " / " + MaxValue;
            else if (valueText && value == 0)
                valueText.text = " ";
			if(gameObject.layer == 11)
				CheckForHide(value, MaxValue);
		}
	}

	float CalculateFill (float curValue, float inMin, float inMax, float outMin, float outMax)
	{
		return(curValue - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
		// calculate current value, place it within inMin-inMax scale, convert that scale to 0-1
		// for example, with 78hp and 230 max
		// (78 - 0) * (1 - 0) / (230 - 0) + 0 = 78/230
		//    78    *    1    /    230        = 0.339
		// this formula is intentionally complicated to account for alternate inMin/outMax values
	}

	// Update is called once per frame
	void Update ()
	{
		if(gameObject.layer == 11)
			FixFlip();
		HandleBar();
	}

	void HandleBar ()
	{
		if(fillAmount != content.fillAmount)
			content.fillAmount = fillAmount;
	}

	void CheckForHide(float cur, float max)
	{
		if (cur == max)
		{
			gameObject.SetActive(false);
		}
		else if (cur != max)
		{
			gameObject.SetActive(true);
		}
	}

	void FixFlip()
	{
		if (gameObject.GetComponentInParent<AIMove>().gameObject.transform.localScale.x == -1)
			transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
		else
			transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
	}
}
