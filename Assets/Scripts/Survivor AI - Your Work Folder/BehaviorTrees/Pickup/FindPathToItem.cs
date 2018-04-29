using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPathToItem : Task {


    public FindPathToItem() : base()
    {
    }

    public override TASK_STATUS Run(Blackboard blackboard)
    {
        TASK_STATUS output = TASK_STATUS.FAILURE;

        PathNode start = NearestNodeStart(blackboard.survivor, blackboard.graph);
        PathNode end = NearestNodeEnd(blackboard.nearestItem, blackboard.graph);

        //blackboard.pickupPath.AddFirst(start);

        AStar aStar = new AStar(blackboard);
        bool status = aStar.GetPath(ref blackboard.pickupPath, start, end);

        if(status == false)
        {
            // Failed
            Debug.Log("FindPathToItem - output: " + output);
            return output;
        }

        //blackboard.pickupPath.AddLast(end);
        //Debug.Log("Appending node to end: " + end);

        //if (blackboard.pickupPath.First != null)
        //{
        //    output = TASK_STATUS.SUCCESS;
        //    Debug.Log("FindPathToItem - output: " + output);
        //}

        output = TASK_STATUS.SUCCESS;
        Debug.Log("FindPathToItem - output: " + output);

        return output;
    }

    private PathNode NearestNodeStart(Survivor start, PathNode[] graph)
    {
        float dist;
        float closestDist = float.MaxValue;
        PathNode nearestNode = null;

        foreach (PathNode node in graph)
        {
            dist = (node.transform.position - start.transform.position).magnitude;
            if(dist <= closestDist)
            {
                closestDist = dist;
                nearestNode = node;
            }
        }

        return nearestNode;
    }

    private PathNode NearestNodeEnd(ItemPickup end, PathNode[] graph)
    {
        Vector3 dir;
        float dist;
        float closestDist = float.MaxValue;
        PathNode nearestNode = null;

        foreach (PathNode node in graph)
        {
            dir = node.transform.position - end.transform.position;
            dist = dir.magnitude;
            if (dist <= closestDist)
            {
                closestDist = dist;
                nearestNode = node;
            }
        }

        return nearestNode;
    }
}
