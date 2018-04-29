using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : ItemPickup {

	protected float healthvalue;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		itemtype = ITEMTYPE.HEALTH;
		healthvalue = 25.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float getHealthValue()
	{
		return healthvalue;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer ("Survivor")) {
			Survivor s = other.gameObject.GetComponent<Survivor> ();
			s.AddHealth (this);
			GameManager.RemoveItem (this);
			Destroy (gameObject);
		}
	}
}
