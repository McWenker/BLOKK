using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Abilities : MonoBehaviour
{
    public bool weaponEquipped = false;
    public bool shieldEquipped = false;
	public bool isMelee;
	public LayerMask enemyLayer;

	public Dictionary<string, bool> abilityUnlock =	new Dictionary<string, bool>()
	{
		{"EquipProjectile", false}, // TODO
		{"DodgeRoll", false},
		{"DoubleJump", false},
		{"AirDash", false},
		{"EquipShield", false}, // TODO
		{"KeyShot", false} // TODO
	};

	[SerializeField]
	int jumpCost;
	[SerializeField]
	int doubleJumpCost;
	[SerializeField]
	int dashCost;
	[SerializeField]
	int airDashCost;

    [SerializeField]
    AmmoAnim weaponView;

    [SerializeField]
    GameObject weap1Parent;
    [SerializeField]
    GameObject weap1;

    [SerializeField]
    GameObject weap2Parent;
    [SerializeField]
    GameObject weap2;

    [SerializeField]
    GameObject weap;

    [SerializeField]
    Shield shield;

    [SerializeField]
    BarStat barSH;

    int ammo;
	int maxAmmo;
	bool doubleJump;
    [SerializeField]
    bool weapon1Wielded = true;
   
	GameObject interact;
	Grounded g;
	Movement m;
	BlokkAnim ba;
	Interact inter;
	Weapon ws;
	Dash d;
	Stamina st;

    public GameObject Weap1
    {
        get
        {
            return weap1;
        }

        set
        {
            this.weap1 = value;
            if (weap1 != null && weap2 == null)
                weapon1Wielded = true;
            CheckWeapon();
        }
    }

    public GameObject Weap2
    {
        get
        {
            return weap2;
        }

        set
        {
            this.weap2 = value;
            if (weap2 != null && weap1 == null)
                weapon1Wielded = false;
            CheckWeapon();
        }
    }

    public GameObject Shield
    {
        set
        {
            if (value != null)
            {
                this.shield = value.GetComponent<Shield>();
                shieldEquipped = true;                    
            }
            else
            {
                shieldEquipped = false;
            }
        }
    }

    public bool Weapon1Wielded
    {
        get
        {
            return weapon1Wielded;
        }

        set
        {
            this.weapon1Wielded = value;
            weap1Parent.SetActive(weapon1Wielded);
            weap2Parent.SetActive(!weapon1Wielded);
            if(weapon1Wielded)
            {
                if (weap1 != null)
                {
                    if (weap1.GetComponent<RangedWeapon>() != null)
                    {
                        weaponView.PlayerWeapon = weap1.GetComponent<RangedWeapon>();
                        isMelee = false;
                    }
                    else
                    {
                        weaponView.PlayerWeapon = weap1.GetComponent<MeleeWeapon>();
                        isMelee = true;
                    }
                }
                else
                    weaponEquipped = false;
            }
            else
            {
                if (weap2 != null)
                {
                    if (weap2.GetComponent<RangedWeapon>() != null)
                    {
                        weaponView.PlayerWeapon = weap2.GetComponent<RangedWeapon>();
                        isMelee = false;
                    }
                    else
                    {
                        weaponView.PlayerWeapon = weap2.GetComponent<MeleeWeapon>();
                        isMelee = true;
                    }
                }
                else
                    weaponEquipped = false;
            }

            CheckWeapon();
        }
    }

	public void Jump()
	{
		if (g.Ground)
		{
			if (st.Stam >= jumpCost)
			{
				m.Jump();
				ba.Jump();
				doubleJump = true;
				st.Stam -= jumpCost;
			}
		}
		else if (abilityUnlock["DoubleJump"] && doubleJump)
		{
			if (st.Stam >= doubleJumpCost)
			{
				m.Jump();
				ba.Jump();
				doubleJump = false;
				st.Stam -= doubleJumpCost;
			}
		}
	}

	public void Reload()
	{
		if(ws != null)
		{
			if (!isMelee && (ammo < maxAmmo))
			{
               StartCoroutine(weap.GetComponent<RangedWeapon>().Reload(ammo, maxAmmo));
			}
		}
	}
	
	public void Roll()
	{
        Debug.Log("Abilities.Roll()");
		if (g.Ground)
		{
            if (abilityUnlock["DodgeRoll"])
			{
				if(st.Stam >= dashCost)
				{
                    d.DashActive();
					st.Stam -= dashCost;
				}
			}
		}
		else if (abilityUnlock["DodgeRoll"] && abilityUnlock["AirDash"])
		{	
			if(st.Stam >= airDashCost)
            {
                d.DashActive();
                st.Stam -= airDashCost;
			}
		}
	}
	
	public void Fire()
	{
		if(weaponEquipped && ws.canFire)
		{
			ba.Fire();
			if(isMelee)
				StartCoroutine(weap.GetComponent<MeleeWeapon>().Fire(enemyLayer));
			else
			{
				if(ammo > 0)
					StartCoroutine(weap.GetComponent<RangedWeapon>().Fire(m.facingRight, enemyLayer));
			}
		}
	}

	public void EndFire()
	{
		ba.EndFire();
	}

	public void Interact()
	{
		inter.Interaction(inter.holder);
	}

	public void Learn(string Ability)
	{
		abilityUnlock[Ability] = true;
	}

	void Awake()
	{
		g = GetComponent<Grounded>();
		m = GetComponent<Movement>();
		d = GetComponent<Dash>();
		ba = GetComponent<BlokkAnim>();
		inter = GetComponentInChildren<Interact>();
		st = GetComponent<Stamina>();
	}

    void CheckAmmo()
    {
        if (weap != null)
        {
            if (weap.GetComponent<RangedWeapon>() != null)
            {
                maxAmmo = weap.GetComponent<RangedWeapon>().MaxAmmo;
                ammo = weap.GetComponent<RangedWeapon>().Ammo;
            }
        }
    }

    void CheckWeapon()
    {
        if (weapon1Wielded && weap1 != null)
        {
            weap = weap1;
        }
        else if (!weapon1Wielded && weap2 != null)
        {
            weap = weap2;
        }

        if (weap != null)
        {
            ws = weap.GetComponent<MeleeWeapon>();
            if (ws == null)
            {
                RangedWeapon wsRanged = weap.GetComponent<RangedWeapon>();
                if (wsRanged != null)
                {
                    ws = wsRanged;
                    isMelee = false;
                }
            }
            else
            {
                isMelee = true;
            }
        }
        else
        {
            weaponEquipped = false;
        }
    }

    void Update()
	{
        if (weaponEquipped && weap != null)
        {
            weap.GetComponent<SpriteRenderer>().enabled = !m.isDashing.isDash;
            if(weaponView.PlayerWeapon != ws)
            {
                weaponView.PlayerWeapon = ws;
            }
        }
        else
        {
            weaponView.PlayerWeapon = null;
        }

        if(shieldEquipped)
        {
            barSH.MaxVal = shield.MaxShield;
            barSH.CurrentVal = shield.ShieldPoints;
        }

        else
        {
            barSH.MaxVal = 0;
            barSH.CurrentVal = 0;
        }

        CheckAmmo();
	}



}
