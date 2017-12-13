using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryDisplay : MonoBehaviour
{
	bool display = false;
	Canvas canvas;

	public bool Display
	{
		set
		{
			this.display = value;
			canvas.enabled = display;
		}
	}

	void Awake()
	{
		canvas = GetComponent<Canvas>();
	}
}
