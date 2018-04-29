using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultAmmo : AmmoPickup {

	// Use this for initialization
	public override void Start () {
		base.Start ();
		ammoValue = 15;
		ammotype = AMMOTYPE.ASSAULT;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
