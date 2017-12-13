using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Equipped : MonoBehaviour
{
	public const int numEquippedSlots = 7;

	public Image[] equippedImages = new Image[numEquippedSlots];
	public ItemScriptableObject[] equipped = new ItemScriptableObject[numEquippedSlots];
    public Image[] slotNames = new Image[numEquippedSlots];

	[SerializeField]
	private WeaponScriptableObject weaponSlot1;

    [SerializeField]
	private WeaponScriptableObject weaponSlot2;

    [SerializeField]
	private ShieldScriptableObject shieldSlot;

    /*[SerializeField]
	private HelmetScriptableObject helmetSlot;

	[SerializeField]
	private ArmorScriptableObject armorSlot;

	[SerializeField]
	private TrinketScriptableObject trinketSlot1;

	[SerializeField]
	private TrinketScriptableObject trinketSlot2;*/

    Color unequipColor = new Color(1f,.9f,.5f,.76f);

    Color equipColor = new Color(.5f, 1f, .45f, .76f);

    GameObject playerObject;

    GameObject weapon1Parent;
    GameObject weapon2Parent;
    GameObject shieldParent;

	public WeaponScriptableObject WeaponSlot1
	{
		get
		{
			return WeaponSlot1;
		}

		set
        {
            this.weaponSlot1 = value;
            if(weaponSlot1 != null) WieldNewWeapon(weaponSlot1, true);
            else WieldNewWeapon(null, true);
        }
	}

	public WeaponScriptableObject WeaponSlot2
	{
		get
		{
			return WeaponSlot2;
		}
		
		set
		{
            this.weaponSlot2 = value;
            if (weaponSlot2 != null) WieldNewWeapon(weaponSlot2, false);
            else WieldNewWeapon(null, false);
        }
	}

	public ShieldScriptableObject ShieldSlot
	{
		get
		{
			return shieldSlot;
		}

		set
		{
			this.shieldSlot = value;
            if (shieldSlot != null) WieldNewShield(shieldSlot);
            else WieldNewShield(null);
		}
	}

    /*public HelmetScriptableObject HelmetSlot
    {
        get
        {
            return helmetSlot;
        }

        set
        {
            this.helmetSlot = value;
        }
    }

    public ArmorScriptableObject ArmorSlot
    {
        get
        {
            return armorSlot;
        }

        set
        {
            this.armorSlot = value;
        }
    }

    public TrinketScriptableObject TrinketSlot1
    {
        get
        {
            return trinketSlot1;
        }

        set
        {
            this.trinketSlot1 = value;
        }
    }

    public TrinketScriptableObject TrinketSlot2
    {
        get
        {
            return trinketSlot2;
        }

        set
        {
            this.trinketSlot2 = value;
        }
    }*/

    public void AddEquip(ItemScriptableObject equipToAdd, string slotType)
    {
        for (int i = 0; i < equipped.Length; i++)
        {
            if (equipped[i] == null)
            {
                // slot is empty, add equipped
                equipped[i] = equipToAdd;
                equippedImages[i].sprite = equipToAdd.sprite;
                equippedImages[i].enabled = true;
                slotNames[i].color = equipColor;
                UpdateSlots();
                return;
            }
        }
    }

    public void AddEquipAtSlot(ItemScriptableObject equipToAdd, int equippedSlot)
    {
        if (equipped[equippedSlot] == null)
        {
            equipped[equippedSlot] = equipToAdd;
            equippedImages[equippedSlot].sprite = equipToAdd.sprite;
            equippedImages[equippedSlot].enabled = true;
            slotNames[equippedSlot].color = equipColor;
            UpdateSlots();
            return;
        }
    }

    public void RemoveEquip(ItemScriptableObject equipToRemove)
    {
        for (int i = 0; i < equipped.Length; i++)
        {
            if (equipped[i] == equipToRemove)
            {
               equipped[i] = null;
                equippedImages[i].sprite = null;
                equippedImages[i].enabled = false;
                slotNames[i].color = unequipColor;
                UpdateSlots();
                return;
            }
        }
    }

    public void RemoveEquipAtSlot(int equippedSlot)
    {
        if (equipped[equippedSlot] != null)
        {
            equipped[equippedSlot] = null;
            equippedImages[equippedSlot].sprite = null;
            equippedImages[equippedSlot].enabled = false;
            slotNames[equippedSlot].color = unequipColor;
            UpdateSlots();
            return;
        }
    }

    public void ExchangeItems(int equippedSlot1, int equippedSlot2)
    {
        ItemScriptableObject temp = equipped[equippedSlot1];
        equipped[equippedSlot1] = equipped[equippedSlot2];
        equipped[equippedSlot2] = temp;
        UpdateSlots();
    }

    private void Awake()
    {
        playerObject = GameObject.Find("BLOKK");
        weapon1Parent = GameObject.Find("BLOKK/EquippedWeapon1");
        weapon2Parent = GameObject.Find("BLOKK/EquippedWeapon2");
        weapon2Parent.SetActive(false);
        shieldParent = GameObject.Find("BLOKK/EquippedShield");
    }

    void UpdateSlots()
    {
        if (weaponSlot1 != equipped[0]) WeaponSlot1 = (WeaponScriptableObject)equipped[0];
        if (weaponSlot2 != equipped[1]) WeaponSlot2 = (WeaponScriptableObject)equipped[1];
        if (shieldSlot != equipped[3]) ShieldSlot = (ShieldScriptableObject)equipped[3];
    }

    void WieldNewWeapon(WeaponScriptableObject weapon, bool isSlot1)
    {
        weapon1Parent.SetActive(true);
        weapon2Parent.SetActive(true);
        if (playerObject.GetComponentInChildren<WeaponAnim>() == null) // there is no weapon equipped, set player to equipped
        {
            playerObject.GetComponent<Abilities>().weaponEquipped = true;
        }
        else if (isSlot1) // going into first slot, check to see if there was previously a weapon
        {
            WeaponAnim oldWeap = weapon1Parent.GetComponentInChildren<WeaponAnim>();
            if (oldWeap != null)
            {
                GameObject.Destroy(oldWeap.gameObject);
                oldWeap = null;
            }
        }
        else if (!isSlot1) // going into second slot, check to see if there was previously a weapon
        {
            WeaponAnim oldWeap = weapon2Parent.GetComponentInChildren<WeaponAnim>();
            if (oldWeap != null)
            {
                GameObject.Destroy(oldWeap.gameObject);
                oldWeap = null;
            }
        }
        if (playerObject.GetComponent<Abilities>().Weapon1Wielded)
            weapon2Parent.SetActive(false);
        else
            weapon1Parent.SetActive(false);

        if (weapon != null) // there is an weapon being placed into the slot
        {
            GameObject wielded = Instantiate(weapon.itemObject, weapon.itemObject.GetComponent<WeaponAnim>().WeapPos, Quaternion.Euler(Vector3.right)) as GameObject;
            WeaponAnim equipAnim = wielded.GetComponent<WeaponAnim>();
            equipAnim.FacingRight = playerObject.GetComponent<Movement>().facingRight;
            if (isSlot1)
            {
                playerObject.GetComponent<Abilities>().Weap1 = wielded;
                wielded.transform.parent = weapon1Parent.transform;
            }
            else
            {
                playerObject.GetComponent<Abilities>().Weap2 = wielded;
                wielded.transform.parent = weapon2Parent.transform;
            }

            if(wielded.GetComponent<MeleeWeapon>() != null)
                wielded.GetComponent<MeleeWeapon>().IsEquipped = true;
            else
                wielded.GetComponent<RangedWeapon>().IsEquipped = true;
            wielded.transform.localPosition = equipAnim.Equip();
            wielded.transform.localScale = new Vector3(1, 1, 1);
        }
        else // there is a null being placed into slot, clear related player slot
        {
            if (isSlot1)
                playerObject.GetComponent<Abilities>().Weap1 = null;
            else
                playerObject.GetComponent<Abilities>().Weap2 = null;
        }
    }

    void WieldNewShield(ShieldScriptableObject shield)
    {
        Shield oldShield = shieldParent.GetComponentInChildren<Shield>();
        if(oldShield != null)
        {
            GameObject.Destroy(oldShield.gameObject);
            oldShield = null;
        }

        if (shield != null)
        {
            GameObject wielded = Instantiate(shield.itemObject, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
            wielded.transform.parent = shieldParent.transform;
            wielded.GetComponent<Shield>().IsEquipped = true;

            playerObject.GetComponent<Abilities>().Shield = wielded;
        }
        else
        {
            playerObject.GetComponent<Abilities>().Shield = null;
        }

    }
}
