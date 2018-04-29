using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Weapon {

	public GameObject projectilePrefab;
	private static float fireSpeed = 0.25f;

	public override void Start ()
	{
		base.Start ();
		ammo = 20;
		type = WEAPON_TYPE.ASSAULT;
	}

	public override bool Fire (Transform barrel, Vector3 target)
	{
		bool output = false;
		if (canFire && ammo > 0) {
			Instantiate (projectilePrefab, barrel.position, barrel.rotation);
			canFire = false;

			Invoke ("Cooldown", fireSpeed);
			output = true;

			ammo--;
		}
		return output;
	}


}
