using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WEAPON_TYPE
{
	NULL = -1,
	PISTOL = 0,
	ASSAULT = 1,
	SHOTGUN = 2,
	GRENADE = 3
}

public abstract class Weapon : MonoBehaviour {

	public WEAPON_TYPE type;
	protected bool canFire;
	protected int ammo;

	public virtual void Start()
	{
		canFire = true;
	}

	public abstract bool Fire (Transform barrel, Vector3 target);

	protected void Cooldown()
	{
		canFire = true;
	}

	public void AddAmmo(AmmoPickup pickup)
	{
		ammo += pickup.getAmmoValue ();
	}

	public int getAmmo()
	{
		return ammo;
	}

}
