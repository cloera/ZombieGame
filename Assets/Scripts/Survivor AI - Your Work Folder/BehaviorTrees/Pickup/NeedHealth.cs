using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedHealth : Task {

    public override TASK_STATUS Run(Blackboard blackboard)
    {
        TASK_STATUS output = TASK_STATUS.FAILURE;

        HealthPack[] healthPacks = GameObject.FindObjectsOfType<HealthPack>();

        if(blackboard.survivor.health < 50.0f && healthPacks.Length > 0)
        {
            float dist;
            float closestDist = float.MaxValue;

            foreach (HealthPack h in healthPacks)
            {
                dist = (h.transform.position - blackboard.survivor.transform.position).magnitude;
                if (dist <= closestDist)
                {
                    closestDist = dist;
                    blackboard.nearestItem = h;
                }
            }
            output = TASK_STATUS.SUCCESS;
            //Debug.Log("NeedHealth - output: " + output);
        }

        return output;
    }
}
