using UnityEngine;
using System.Collections;

public class Interact : MonoBehaviour
{
	[SerializeField]
	float speechRange;

	public bool interacting = false;

	public GameObject holder;

	Vector3 WeapPos;
	Abilities abs;
	bool weapEquipped;
	bool shieldEquipped;
	Transform equippedWeap;
	Transform equippedShield;
	AmmoAnim weaponInfo;
    DialogManager dialog;

	SpeechPanel speechPanel;
	[SerializeField]
	bool speechState;
	[SerializeField]
	bool speechTime = true;

	Transform speakerTransform;
	IEnumerator rangeCoroutine;
	IEnumerator timeCoroutine;

	Inventory inventory;
	Equipped equipped;

	GameObject lastHolder;
	int speechCount = 0;

	public bool SpeechState
	{
		get
		{
			return speechState;
		}

		set
		{
			this.speechState = value;
			speechPanel.IsSpeaking = speechState;
		}
	}

	public void Interaction(GameObject interObject)
	{
        if (interObject != null)
        {
            if (interObject.CompareTag("NPC"))
            {
                if (speechTime)
                {
                    timeCoroutine = CheckSpeechTime();
                    StartCoroutine(timeCoroutine);
                    interObject.GetComponent<NPC>().Speak();
                    speakerTransform = interObject.transform;
                    SpeechState = true;
                    lastHolder = interObject;
                    if(rangeCoroutine != null)
                        StopCoroutine(rangeCoroutine);
                    rangeCoroutine = CheckSpeechRange(speakerTransform);
                    StartCoroutine(rangeCoroutine);
                }
            }

            if (interObject.CompareTag("Weapon") && !interObject.GetComponent<Weapon>().IsEquipped)
            {
                if (abs.weaponEquipped)
                {
                    if (interObject.GetComponent<Weapon>().isMelee)
                        PickUp(interObject.GetComponent<MeleeWeapon>().weaponData, interObject);
                    else
                    {
                        PickUp(interObject.GetComponent<RangedWeapon>().weaponData, interObject);
                    }
                }
                else
                {
                    if (interObject.GetComponent<Weapon>().isMelee)
                        equipped.AddEquipAtSlot(interObject.GetComponent<MeleeWeapon>().weaponData, 0);
                    else
                    {
                        equipped.AddEquipAtSlot(interObject.GetComponent<RangedWeapon>().weaponData, 0);
                    }
                    Destroy(interObject);
                }
            }

            if (interObject.CompareTag("PowerUp"))
            {
                abs.Learn(interObject.name);
                Destroy(interObject);
            }

            if (interObject.CompareTag("Shield") && !interObject.GetComponent<Shield>().IsEquipped)
            {
                if (abs.shieldEquipped)
                {
                    PickUp(interObject.GetComponent<Shield>().shieldData, interObject);
                }
                else
                {
                    equipped.AddEquipAtSlot(interObject.GetComponent<Shield>().shieldData, 3);
                    Destroy(interObject);
                }
            }

            if (interObject.CompareTag("Money"))
            {
                Debug.Log("Picked up " + interObject.GetComponent<Money>().Wealth() + " gold pieces.");
                Destroy(interObject);
            }

            if (interObject.CompareTag("Quest"))
            {
                ItemScriptableObject questItemData = interObject.GetComponent<QuestPickup>().ItemData;
                QuestController.instance.CheckItem(questItemData.questID, questItemData.itemName);
                PickUp(questItemData, interObject);
            }
        }
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if(!col.CompareTag("Ground") && !col.CompareTag("Enemy"))
		{
			holder = col.gameObject;
		}
	}

	void Awake()
	{
		abs = GetComponentInParent<Abilities>();
		weaponInfo = GameObject.Find("UI/Canvas/WeaponInfo").GetComponent<AmmoAnim>();
		speechPanel = GameObject.Find("UI/Canvas/SpeechPanel").GetComponent<SpeechPanel>();
		inventory = GameObject.Find("UI/PersistentCanvas/Inventory/BackpackColumns").GetComponent<Inventory>();
		equipped = GameObject.Find("UI/PersistentCanvas/Inventory/EquippedRows").GetComponent<Equipped>();
        dialog = GameObject.FindObjectOfType<DialogManager>();
    }

	IEnumerator CheckSpeechRange(Transform speaker)
	{
		while(true)
		{
			double currentRange = Mathf.Abs(speaker.position.x - transform.position.x);
			if((currentRange > speechRange)) 
			{
				speechState = false;
                dialog.DismissDialog(false);
			}
			yield return new WaitForSeconds(.6f);
		}
	}

	IEnumerator CheckSpeechTime()
	{
		speechTime = false;
		while(true)
		{
			yield return new WaitForSeconds(.6f);
			speechTime = true;
			StopCoroutine(timeCoroutine);
		}
	}

	void PickUp(ItemScriptableObject item, GameObject instance)
	{
		inventory.AddItem(item);
		GameObject.Destroy(instance);
	}

	void ShieldEquip(GameObject item)
	{
		if(shieldEquipped)
		{
			Transform oldShield = GameObject.Find("BLOKK/EquippedShield").transform.GetChild(0);
			oldShield.position = item.transform.position;
			oldShield.transform.parent = null;
			oldShield.GetComponent<ShieldStat>().IsEquipped = false;
		}
		else
		{
			shieldEquipped = true;
		}
		item.transform.parent = GameObject.Find("BLOKK/EquippedShield").transform;
		item.GetComponent<ShieldStat>().IsEquipped = true;
	}

	/*void WeapEquip(GameObject item)
	{
		if(weapEquipped)
		{
			Transform oldWeap = GameObject.Find("BLOKK/EquippedWeapon").transform.GetChild(0);
			oldWeap.position = item.transform.position;
			oldWeap.transform.parent = null;
			oldWeap.GetComponent<Weapon>().IsEquipped = false;
		}
		else
		{
			weapEquipped = true;
			abs.weaponEquipped = true;
		}
		item.transform.parent = GameObject.Find("BLOKK/EquippedWeapon").transform;
		item.GetComponent<Weapon>().IsEquipped = true;
		if(item.GetComponent<Weapon>().isMelee)
		{
			abs.isMelee = true;
		}
		else
		{
			abs.isMelee = false;
		}
		weaponInfo.PlayerWeapon = item.GetComponent<Weapon>();
		WeaponAnim equipAnim = item.GetComponent<WeaponAnim>();
		item.transform.localPosition = equipAnim.Equip();
		item.transform.localScale = new Vector3(1,1,1);
		abs.weap = item;
	}*/
}
