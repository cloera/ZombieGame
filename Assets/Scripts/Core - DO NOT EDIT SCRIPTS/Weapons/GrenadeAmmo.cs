using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAmmo : AmmoPickup {

	// Use this for initialization
	public override void Start () {
		base.Start ();
		ammoValue = 1;
		ammotype = AMMOTYPE.GRENADE;
	}

	// Update is called once per frame
	void Update () {

	}
}
