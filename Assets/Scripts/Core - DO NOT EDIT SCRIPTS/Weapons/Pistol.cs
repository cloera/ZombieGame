using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {

	public GameObject projectilePrefab;
	private static float fireSpeed = 1.0f;

	public override void Start ()
	{
		base.Start ();

		type = WEAPON_TYPE.PISTOL;
	}

	public override bool Fire (Transform barrel, Vector3 target)
	{
		bool output = false;
		if (canFire) {
			Instantiate (projectilePrefab, barrel.position, barrel.rotation);
			canFire = false;

			Invoke ("Cooldown", fireSpeed);
			output = true;
		}
		return output;
	}

}
