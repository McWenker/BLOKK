using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AmmoAnim : MonoBehaviour
{
	int ammo;
	int maxAmmo;

    [SerializeField]
    Abilities playerAbil;

	[SerializeField]
	Image weaponSpriteField;
	
	[SerializeField]
	Text weaponNameField;
	
	[SerializeField]
	Text ammoField;
	
	[SerializeField]
	Text ammoTypeField;

	Weapon playerWeapon;

	public Weapon PlayerWeapon
	{
        get
        {
            return playerWeapon;
        }

		set
		{
			this.playerWeapon = value;
            if (playerWeapon != null)
            {
                weaponSpriteField.sprite = playerWeapon.idleSprite;
                weaponNameField.text = playerWeapon.itemName;
                ammoField.gameObject.SetActive(!playerWeapon.isMelee);
                ammoTypeField.gameObject.SetActive(!playerWeapon.isMelee);
                ammoTypeField.text = playerWeapon.ammoType;
            }
            else
            {
                ammoField.gameObject.SetActive(false);
                ammoTypeField.gameObject.SetActive(false);
            }
		}
	}

    void Update ()
	{
		if(playerWeapon != null)
		{
			weaponSpriteField.gameObject.SetActive(true);
			if(!playerWeapon.isMelee)
				AmmoCount();
		}
		else
		{
			weaponSpriteField.gameObject.SetActive(false);
		}
	}

	void AmmoCount ()
	{
		ammoField.text = playerWeapon.GetComponent<RangedWeapon>().Ammo + " / " + playerWeapon.GetComponent<RangedWeapon>().MaxAmmo;
	}
}
