using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum DEBUGMODE
{
	DISABLED,
	ENABLED
}

public enum SURVIVORSTATE
{
	NORMAL,
	DEAD,
}

public enum SURVIVORNAME
{
	FRANCIS,
	LOUIS,
	ZOEY,
	BILL
}

public class Survivor : Entity {

	NavMeshAgent agent;
	Animator animator;

	public Transform barrel; 
	public GameObject projectilePrefab;

	public SURVIVORNAME name;
	SURVIVORSTATE state;

	public DEBUGMODE mode;

	public Weapon currentWeapon;
	Weapon[] weaponList;


	int groundMask;



	// Use this for initialization
	public override void Start () {
		base.Start ();
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponentInChildren<Animator> ();
		groundMask = LayerMask.GetMask ("Ground");

		state = SURVIVORSTATE.NORMAL;
		GameManager.AddSurvivor (this);

		weaponList = GetComponentsInChildren<Weapon> ();

		foreach (Weapon w in weaponList) {
			w.enabled = false;
			if (w.type == WEAPON_TYPE.PISTOL) {
				currentWeapon = w;
				currentWeapon.enabled = true;
			}
		}

	}


	
	// Update is called once per frame
	void FixedUpdate () {
		animator.SetFloat ("Move", agent.velocity.sqrMagnitude);

		if (mode == DEBUGMODE.ENABLED) {
			DebugMove ();
			DebugFire ();
			DebugSwitchWeapons ();
		}


	}

	public void MoveTo(Vector3 point)
	{
		if (state == SURVIVORSTATE.NORMAL && mode == DEBUGMODE.DISABLED) {
			agent.SetDestination (point);
		}

	}

	public void Fire(Vector3 point)
	{
		if (state == SURVIVORSTATE.NORMAL && mode == DEBUGMODE.DISABLED) {

			transform.LookAt (point);

			if (currentWeapon.Fire (barrel, point)) {
				animator.SetTrigger ("Shoot");
			}
		}
	}


	public void SwitchWeapons(WEAPON_TYPE type)
	{
		currentWeapon.enabled = false;

		foreach (Weapon w in weaponList) {
			if (type == w.type) {
				animator.SetTrigger ("SwitchWeapons");
				animator.SetInteger ("Weapon", (int)type);
				currentWeapon = w;
				currentWeapon.enabled = true;
				break;
			}
		}
	}

	protected override void Dead ()
	{
		state = SURVIVORSTATE.DEAD;
		agent.isStopped = true;
	}

	public SURVIVORSTATE GetSurvivorState()
	{
		return state;
	}

	public void AddAmmo(AmmoPickup pickup)
	{
		switch (pickup.getAmmoType ()) 
		{
		case AMMOTYPE.ASSAULT:
			getWeapon (WEAPON_TYPE.ASSAULT).AddAmmo (pickup as AssaultAmmo);
			break;
		case AMMOTYPE.SHOTGUN:
			getWeapon (WEAPON_TYPE.SHOTGUN).AddAmmo (pickup as ShotgunAmmo);
			break;
		case AMMOTYPE.GRENADE:
			getWeapon (WEAPON_TYPE.GRENADE).AddAmmo (pickup as GrenadeAmmo);
			break;
		default:
			break;
		}
	}

	public void AddHealth(HealthPack pack)
	{
		AddHealth (pack.getHealthValue());
	}

	public Weapon getWeapon(WEAPON_TYPE type)
	{
		Weapon output = null;
		foreach(Weapon w in weaponList)
		{
			if (w.type == type) {
				output = w;
				break;
			}
		}

		return output;
	}


	#region DEBUG_CODE
	void DebugMove()
	{
		if (Input.GetButtonDown ("Fire1")) {

			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit groundHit;

			if (Physics.Raycast (camRay, out groundHit, 1000.0f, groundMask)) {
				if (state == SURVIVORSTATE.NORMAL) {
					agent.SetDestination (groundHit.point);
				}
			}
		}
	}

	void DebugFire()
	{
		if (Input.GetButton ("Fire2")) {

			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit groundHit;

			if (Physics.Raycast (camRay, out groundHit, 1000.0f, groundMask)) {
				transform.LookAt (groundHit.point);
				if (currentWeapon.Fire (barrel, groundHit.point)) {
					animator.SetTrigger ("Shoot");
				}
			}
		}

	}

	void DebugSwitchWeapons()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			SwitchWeapons (WEAPON_TYPE.PISTOL);
		}
		else if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			SwitchWeapons (WEAPON_TYPE.ASSAULT);
		}
		else if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			SwitchWeapons (WEAPON_TYPE.SHOTGUN);
		}
		else if(Input.GetKeyDown(KeyCode.Alpha4))
		{
			SwitchWeapons (WEAPON_TYPE.GRENADE);
		}
	}

	#endregion
}
