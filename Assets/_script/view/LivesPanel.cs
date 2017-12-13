using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LivesPanel : MonoBehaviour
{
	int lives;

	[SerializeField]
	Text livesText;

	[SerializeField]
	Color GGColor;
	
	public int Lives
	{
		set
		{
			this.lives = value;
			livesText.text = lives.ToString();
			if(lives == 0)
				livesText.color = GGColor;
		}
	}
}
