using UnityEngine;
using System.Collections;

public class Flee : Task
{

	public override TASK_STATUS Run (Blackboard blackboard)
	{
		TASK_STATUS output = TASK_STATUS.FAILURE;

		blackboard.survivor.MoveTo (blackboard.fleeDirection + blackboard.survivor.transform.position);

        //Debug.Log("Flee - output: " + output);

        return output;
	}

	void FleeFromTarget(Survivor s, Vector3 fleeDirection)
	{



	}
}

