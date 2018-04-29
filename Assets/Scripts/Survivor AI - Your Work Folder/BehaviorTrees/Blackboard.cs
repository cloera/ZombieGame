using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Blackboard 
{
    public Survivor survivor;
	public Enemy target;
    public Vector3 predictedTarget;
	public float fleeDistance;
	public Vector3 fleeDirection;
    public ItemPickup nearestItem;

    public LinkedList<PathNode> pickupPath;

    public PathNode[] graph;


    public Blackboard(Survivor s, PathNode[] g)
	{
		this.survivor = s;
        this.nearestItem = null;
        this.pickupPath = new LinkedList<PathNode>();
        this.graph = g;
    }
	
}


