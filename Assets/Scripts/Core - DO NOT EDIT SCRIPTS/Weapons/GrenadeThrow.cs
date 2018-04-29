using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : Weapon {

	public GameObject projectilePrefab;
	private static float fireSpeed = 1.0f;

	public override void Start ()
	{
		base.Start ();
		ammo = 1;
		type = WEAPON_TYPE.GRENADE;
	}

	public override bool Fire (Transform barrel, Vector3 target)
	{
		bool output = false;
		if (canFire && ammo > 0) {
			GameObject gObj = Instantiate (projectilePrefab, transform.position, transform.rotation);

			Grenade grenade = gObj.GetComponent<Grenade> ();
			grenade.SetTarget (target);

			canFire = false;

			Invoke ("Cooldown", fireSpeed);
			output = true;

			ammo--;
		}
		return output;
	}
}
