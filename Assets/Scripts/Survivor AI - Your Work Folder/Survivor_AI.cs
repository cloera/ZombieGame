using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor_AI : MonoBehaviour {
    
	public Survivor Francis;
	public Survivor Zoey;
	public Survivor Bill;
	public Survivor Louis;

	BehaviorTree FrancisBT;
	BehaviorTree ZoeyBT;
	BehaviorTree BillBT;
	BehaviorTree LouisBT;

	PathNode[] graph;

	// Use this for initialization
	void Start () {


		graph = GameObject.FindObjectsOfType<PathNode> ();

		for(int i = 0; i < 4; i++)
		{
			RunAllTask rootTask = new RunAllTask ();

            /******* Attack ******/
			Sequence attackSequence = new Sequence ();

			AquireTargetPriority aquireTargetNode = new AquireTargetPriority ();
            //PredictAim predictAimNode = new PredictAim();
            ChooseWeapon chooseWeapon = new ChooseWeapon();
			AttackTarget attackNode = new AttackTarget ();

			attackSequence.AddTask (aquireTargetNode);
            //attackSequence.AddTask(predictAimNode);
            attackSequence.AddTask(chooseWeapon);
            attackSequence.AddTask (attackNode);

            /****** Movement *******/
            Selector movementSelector = new Selector();

                /****** Flee *******/
			    Sequence fleeSequence = new Sequence ();

			    FindFleeDirection findFleeNode = new FindFleeDirection ();
			    Flee fleeNode = new Flee ();

			    fleeSequence.AddTask (findFleeNode);
			    fleeSequence.AddTask (fleeNode);

                /****** Pickups ******/
                Selector pickupSelector = new Selector();                
                    MoveToItem moveToItemNode = new MoveToItem();
                    PickupItem pickupItemNode = new PickupItem();
                    Sequence pickupSequence = new Sequence();
                        Selector choosePickup = new Selector();
                            NeedAmmo needAmmoNode = new NeedAmmo();
                            NeedHealth needHealthNode = new NeedHealth();
                        FindPathToItem findPathToItemNode = new FindPathToItem();

            choosePickup.AddTask(needHealthNode);
            choosePickup.AddTask(needAmmoNode);

            pickupSequence.AddTask(choosePickup);
            pickupSequence.AddTask(findPathToItemNode);

            pickupSelector.AddTask(moveToItemNode);
            pickupSelector.AddTask(pickupItemNode);
            pickupSelector.AddTask(pickupSequence);

            movementSelector.AddTask(pickupSelector);
            movementSelector.AddTask(fleeSequence);



            // Add sequences to root
            rootTask.AddTask(attackSequence);
            rootTask.AddTask(movementSelector);
            //rootTask.AddTask(pickupSelector);

            if (i == 0) {
				Blackboard bd = new Blackboard (Francis, graph);
				bd.fleeDistance = 10.0f;
				FrancisBT = new BehaviorTree (rootTask, bd);
			} else if (i == 1) {
				Blackboard bd = new Blackboard (Zoey, graph);
				bd.fleeDistance = 10.0f;
				ZoeyBT = new BehaviorTree (rootTask, bd);
			} else if (i == 2) {
				Blackboard bd = new Blackboard (Bill, graph);
				bd.fleeDistance = 10.0f;
				BillBT = new BehaviorTree (rootTask, bd);
			} else {
				Blackboard bd = new Blackboard (Louis, graph);
				bd.fleeDistance = 10.0f;
				LouisBT = new BehaviorTree (rootTask, bd);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		//float startTime = Time.time * 1000;

        if(Francis.GetSurvivorState() != SURVIVORSTATE.DEAD)
        {
            FrancisBT.Run();
        }
		if(Zoey.GetSurvivorState() != SURVIVORSTATE.DEAD)
        {
            ZoeyBT.Run();
        }
		if(Louis.GetSurvivorState() != SURVIVORSTATE.DEAD)
        {
            LouisBT.Run();
        }
		if(Bill.GetSurvivorState() != SURVIVORSTATE.DEAD)
        {
            BillBT.Run();
        }
		//PrintUpdateTime (startTime);
	}

	private void PrintUpdateTime(float startTime)
	{
		
		float processTime = (Time.time * 1000) - startTime;
		if (processTime < 0.0001f) {
			processTime = 0.0f;
		}
		Debug.Log ("AI Update (ms): " + processTime);

	}


}
