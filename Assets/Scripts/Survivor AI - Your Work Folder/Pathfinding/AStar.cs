using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar {

    Dictionary<PathNode, float> gScore;
    Dictionary<PathNode, float> fScore;
    Dictionary<PathNode, PathNode> cameFromPath;

    public AStar(Blackboard b)
    {
        this.gScore = new Dictionary<PathNode, float>();
        this.fScore = new Dictionary<PathNode, float>();

        cameFromPath = new Dictionary<PathNode, PathNode>();

        foreach(PathNode node in b.graph)
        {
            cameFromPath.Add(node, null);
            this.gScore.Add(node, Mathf.Infinity);
            this.fScore.Add(node, Mathf.Infinity);
        }
    }

    public bool GetPath(ref LinkedList<PathNode> result, PathNode Start, PathNode End)
    {
        LinkedList<PathNode> openList = new LinkedList<PathNode>();
        LinkedList<PathNode> closedList = new LinkedList<PathNode>();

        // Push start to open list
        openList.AddFirst(Start);

        // Set g score of first node. Should be 0 since it is distance from start to start
        gScore[openList.First.Value] = 0.0f;

        // Set f score of first node. Should be distance from start to end
        fScore[openList.First.Value] = DistanceBetweenPoints(openList.First.Value, End);

        while (openList.Count > 0)
        {
            // Get currNode with the lowest score in openList
            PathNode currNode = GetLowestFScore(fScore, openList);

            // Did I reach the end?
            if (currNode == End)
            {
                result = ReconstructPath(cameFromPath, currNode);
                return true;
            }

            // Remove currNode from open list and add to closed list
            openList.Remove(currNode);
            closedList.AddFirst(currNode);

            // For each neighbor in currNode's walkable adjacent tiles
            foreach(PathNode neighbor in currNode.neighbors)
            {
                // if neighbor is in the closed list
                if (closedList.Contains(neighbor))
                {
                    // Ignore
                    continue;
                }
                    
                // if neighbor is NOT in open list
                if(!openList.Contains(neighbor))
                {
                    // Add it
                    openList.AddFirst(neighbor);
                    //gScore.Add(neighbor, gScore[currNode] + DistanceBetweenPoints(currNode, neighbor));
                    //fScore.Add(neighbor, gScore[neighbor] + DistanceBetweenPoints(neighbor, End));
                }

                // Get possible g score
                float possibleGScore = gScore[currNode] + DistanceBetweenPoints(currNode, neighbor);

                // Evaluate fScore of neighbor compared to other neighbors
                if (gScore[neighbor] <= possibleGScore)
                {
                    //// This should be the best path so far
                    //// update bestNode option and currFScore
                    //bestNode = neighbor;
                    //currFScore = fScore[neighbor];

                    ////Debug.Log(bestNode);
                    ////Debug.Log(currFScore);
                    continue;
                }

                //Debug.Log("In for loop.....");
                // Push best node out of neighbors onto list
                cameFromPath[neighbor] = currNode;
                gScore[neighbor] = possibleGScore;
                fScore[neighbor] = gScore[neighbor] + DistanceBetweenPoints(neighbor, End);
                //Debug.Log("Added node to cameFromPath: " + currNode);
            }
        }

        // No path found
        return false;
    }

    private LinkedList<PathNode> ReconstructPath(Dictionary<PathNode, PathNode> cameFromPath, PathNode currNode)
    {
        LinkedList<PathNode> result = new LinkedList<PathNode>();
        // Add ending node to list first
        result.AddFirst(currNode);
        // While current node is a key of cameFromPath dictionary
        while(cameFromPath[currNode] != null)
        {
            // Go to next node in path
            currNode = cameFromPath[currNode];
            // Add to list
            result.AddFirst(currNode);
        }
        return result;
    }

    private PathNode GetLowestFScore(Dictionary<PathNode, float> fScore, LinkedList<PathNode> openList)
    {
        PathNode result = null;
        float lowestScore = float.MaxValue;

        foreach(PathNode node in openList)
        {
            if(lowestScore > fScore[node])
            {
                lowestScore = fScore[node];
                result = node;
            }
        }

        return result;
    }

    private float DistanceBetweenPoints(PathNode start, PathNode end)
    {
        Vector3 dist = end.transform.position - start.transform.position;

        return dist.magnitude;
    }

}
