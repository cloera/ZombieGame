using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedAmmo : Task {

    public override TASK_STATUS Run(Blackboard blackboard)
    {
        TASK_STATUS output = TASK_STATUS.FAILURE;

        AmmoPickup[] ammoList = GameObject.FindObjectsOfType<AmmoPickup>();
        WEAPON_TYPE weaponType;
        Survivor s = blackboard.survivor;

        float dist;
        float closestDist = Mathf.Infinity;

        foreach(AmmoPickup ammo in ammoList)
        {
            //Debug.Log("Ammo left:" + blackboard.survivor.getWeapon(weaponType).getAmmo() +
            //            " Weapon type:" + blackboard.survivor.getWeapon(weaponType) +
            //            " Ammo type:" + ammo.getAmmoType());
            weaponType = GetWeaponForAmmo(ammo);

            dist = (ammo.transform.position - s.transform.position).magnitude;

            if (ammo.getAmmoType() != AMMOTYPE.NULL) //&& ammo.getAmmoType() != AMMOTYPE.GRENADE)
            {
                if (s.getWeapon(weaponType).getAmmo() < 2 && dist <= closestDist)
                {
                    closestDist = dist;
                    blackboard.nearestItem = ammo;
                    output = TASK_STATUS.SUCCESS;
                }
            }

        }

        //Debug.Log("NeedAmmo - output: " + output);
        return output;
    }

    public WEAPON_TYPE GetWeaponForAmmo(AmmoPickup ammo)
    {
        WEAPON_TYPE weapon = WEAPON_TYPE.NULL;

        switch (ammo.getAmmoType())
        {
            case AMMOTYPE.ASSAULT:
                weapon = WEAPON_TYPE.ASSAULT;
                break;
            case AMMOTYPE.SHOTGUN:
                weapon = WEAPON_TYPE.SHOTGUN;
                break;
            case AMMOTYPE.GRENADE:
                weapon = WEAPON_TYPE.GRENADE;
                break;
        }

        return weapon;
    }
}
