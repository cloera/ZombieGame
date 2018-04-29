using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseWeapon : Task {

    public override TASK_STATUS Run(Blackboard blackboard)
    {
        TASK_STATUS output = TASK_STATUS.FAILURE;

        Survivor s = blackboard.survivor;
        Enemy e = blackboard.target;

        float enemyDist = (e.transform.position - s.transform.position).magnitude;

        if(s.getWeapon(WEAPON_TYPE.GRENADE).getAmmo() > 0 && GetZombieCountInRadius(s) >= 5)
        {
            s.SwitchWeapons(WEAPON_TYPE.SHOTGUN);
            output = TASK_STATUS.SUCCESS;
        }
        // When to use shotgun
        else if(s.getWeapon(WEAPON_TYPE.SHOTGUN).getAmmo() > 0 && enemyDist < 8.0f)
        {
            s.SwitchWeapons(WEAPON_TYPE.SHOTGUN);
            output = TASK_STATUS.SUCCESS;
        }
        // When to use assault rifle
        else if(e.GetEnemyType() != EnemyType.ZOMBIE && enemyDist < 25.0f && 
                s.getWeapon(WEAPON_TYPE.ASSAULT).getAmmo() > 0)
        {
            s.SwitchWeapons(WEAPON_TYPE.ASSAULT);
            output = TASK_STATUS.SUCCESS;
        }
        else
        {
            s.SwitchWeapons(WEAPON_TYPE.PISTOL);
            output = TASK_STATUS.SUCCESS;
        }
        return output;
    }

    private int GetZombieCountInRadius(Survivor s)
    {
        List<Enemy> zombies = GameManager.getZombieList();
        List<Enemy> special = GameManager.getSpecialList();

        int count = 0;

        for (int i = 0; i < special.Count; i++)
        {
            Enemy e = special[i];

            float distanceSqr = (e.transform.position - s.transform.position).sqrMagnitude;
            if (distanceSqr < 15.0f)
                count += 3;
        }

        for (int i = 0; i < zombies.Count; i++)
        {
            Enemy e = zombies[i];

            float distanceSqr = (e.transform.position - s.transform.position).sqrMagnitude;
            if (distanceSqr < 15.0f)
                count += 1;
        }

        return count;
    }
}
