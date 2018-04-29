using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon {

	private ParticleSystem shotgunParticles;
	private static float fireSpeed = 1.0f;

	protected ProjectileHit hitData;

	public override void Start ()
	{
		base.Start ();
		hitData.damage = 20;
		ammo = 5;
		type = WEAPON_TYPE.SHOTGUN;
		shotgunParticles = GetComponent<ParticleSystem> ();
	}

	public override bool Fire (Transform barrel, Vector3 target)
	{
		bool output = false;
		if (canFire && ammo > 0) {

			shotgunParticles.Stop ();

			shotgunParticles.Play ();

			canFire = false;

			Invoke ("Cooldown", fireSpeed);
			output = true;

			ammo--;
		}
		return output;
	}

	void OnParticleCollision(GameObject other)
	{

		if (other.layer == LayerMask.NameToLayer("Enemy")) {
			Enemy s = other.gameObject.GetComponent<Enemy> ();

			s.OnHit (hitData);
		}



	}
}
