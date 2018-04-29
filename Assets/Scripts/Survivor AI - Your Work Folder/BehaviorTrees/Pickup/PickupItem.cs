using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PickupItem : Task
{
    public override TASK_STATUS Run(Blackboard blackboard)
    {
        TASK_STATUS output = TASK_STATUS.FAILURE;

        ItemPickup item = blackboard.nearestItem;

        // If there is an item to get
        if (item != null)
        {
            Vector3 itemDir = item.transform.position - blackboard.survivor.transform.position;

            // If we don't have a path to follow anymore
            if (blackboard.pickupPath.Count <= 0)
            {
                
                // Get the item
                blackboard.survivor.MoveTo(item.transform.position);

                output = TASK_STATUS.SUCCESS;
                //Debug.Log("PickupItem - output: " + output + ", item: " + item);
            }
        }

        return output;
    }
}
