using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BehaviorTree
{

	Task root;
	Blackboard pBlackboard;

	public BehaviorTree(Task rootTask, Blackboard blackboard)
	{
		root = rootTask;
		pBlackboard = blackboard;
	}


	public TASK_STATUS Run()
	{
		TASK_STATUS output = TASK_STATUS.NULL;

		root.Run (pBlackboard);

		return output;
	}


}

