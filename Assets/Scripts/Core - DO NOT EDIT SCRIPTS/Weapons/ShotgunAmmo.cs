using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAmmo : AmmoPickup {

	// Use this for initialization
	public override void Start () {
		base.Start ();
		ammoValue = 5;
		ammotype = AMMOTYPE.SHOTGUN;
	}

	// Update is called once per frame
	void Update () {

	}
}
