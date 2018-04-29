using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEMTYPE
{
	NULL,
	AMMO,
	HEALTH
}

public abstract class ItemPickup : MonoBehaviour {

	protected ITEMTYPE itemtype;

	public virtual void Start()
	{
		GameManager.AddItem (this);
	}

	// Update is called once per frame
	void Update () {
		
	}

	public ITEMTYPE getItemType()
	{
		return itemtype;
	}

}
