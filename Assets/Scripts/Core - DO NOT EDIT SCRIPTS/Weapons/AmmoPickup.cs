using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AMMOTYPE
{
    NULL,
    ASSAULT,
    SHOTGUN,
    GRENADE
}

public abstract class AmmoPickup : ItemPickup
{

    protected AMMOTYPE ammotype;
    protected int ammoValue;

    public override void Start()
    {
        base.Start();

        itemtype = ITEMTYPE.AMMO;

        Invoke("CleanUp", 20.0f);
    }

    public AMMOTYPE getAmmoType()
    {
        return ammotype;
    }

    public int getAmmoValue()
    {
        return ammoValue;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Survivor"))
        {
            Survivor s = other.gameObject.GetComponent<Survivor>();
            s.AddAmmo(this);
            GameManager.RemoveItem(this);
            Destroy(gameObject);
        }
    }


    void CleanUp()
    {
        GameManager.RemoveItem(this);

        Destroy(gameObject);
    }
}