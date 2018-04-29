using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToItem : Task {


    public override TASK_STATUS Run(Blackboard blackboard)
    {
        TASK_STATUS output = TASK_STATUS.FAILURE;

        if(blackboard.pickupPath.Count > 0)
        {
            Survivor survivor = blackboard.survivor;
            PathNode pathnode = blackboard.pickupPath.First.Value;

            // Move to PathNode
            Vector3 direction = SeekDirection(survivor.transform.position, pathnode.transform.position);
            survivor.MoveTo(direction + survivor.transform.position);

            // If at PathNode
            if(IsAtPathNode(pathnode, survivor))
            {
                //Debug.Log("Remove first: " + blackboard.pickupPath.First);

                // Remove from list
                blackboard.pickupPath.RemoveFirst();
            }
            output = TASK_STATUS.SUCCESS;
           // Debug.Log("MoveToItem - output: " + output);
        }
        
        return output;
    }

    private bool IsAtPathNode(PathNode node, Survivor s)
    {
        Vector2 dir = node.transform.position - s.transform.position;

        return (dir.magnitude <= 0.8f);
    }

    private Vector3 SeekDirection(Vector3 start, Vector3 end)
    {
        Vector3 dir = end - start;
        float dist = dir.magnitude;
        dir.Normalize();
        dir *= dist;

        return dir;
    }
}
