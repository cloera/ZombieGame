using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buckshot : Projectile {

	// Use this for initialization
	public override void Start () {
		base.Start ();

		hitData.damage = 10;

		Destroy (gameObject, 1.0f);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			Enemy s = other.gameObject.GetComponent<Enemy> ();

			s.OnHit (hitData);
			Destroy (gameObject);
		}
		else if (other.gameObject.layer != LayerMask.NameToLayer("Survivor")) {
			Destroy (gameObject);
		}
	}


}
