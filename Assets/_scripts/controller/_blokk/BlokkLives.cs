using UnityEngine;
using System.Collections;

public class BlokkLives : MonoBehaviour
{
	[SerializeField]
	Transform respawnLoc;

	[SerializeField]
	LivesPanel livesPanel;

	int lives = 1;

	Animator anim;
	Movement move;
	InputController input;
	Health health;
	GameObject equippedWeapon1;
    GameObject equippedWeapon2;

    public int Lives
	{
		get
		{
			return lives;
		}

		set
		{
			int holder = lives;
			this.lives = value;
			livesPanel.Lives = lives;
			if (lives < holder)
			{
				move.moveSwitch = false;
				input.canInput = false;
				equippedWeapon1.SetActive(false);
                equippedWeapon2.SetActive(false);
                if (lives == 0)
				{
					StartCoroutine(BlokkRespawn(true));
				}
				else
				{
					StartCoroutine(BlokkRespawn(false)); // eventually could take in level or respawn location
				}
			}
		}
	}

	IEnumerator BlokkRespawn(bool gameOver)
	{
		while(true)
		{
			yield return new WaitForSeconds(2f);
			if(!gameOver)
			{
				gameObject.transform.position = respawnLoc.position;
				anim.SetBool("Dead", false);			
				move.moveSwitch = true;
				input.canInput = true;
				health.CurHP = health.MaxHP;
				equippedWeapon1.SetActive(true);
                equippedWeapon2.SetActive(true);
            }
			else
			{
				Application.LoadLevel("gameover");
			}
			yield break;
		}
	}

	void Awake()
	{
		anim = GetComponent<Animator>();
		move = GetComponent<Movement>();
		input = GetComponent<InputController>();
		health = GetComponent<Health>();
		equippedWeapon1 = transform.Find("EquippedWeapon1").gameObject;
        equippedWeapon2 = transform.Find("EquippedWeapon2").gameObject;
    }
}
