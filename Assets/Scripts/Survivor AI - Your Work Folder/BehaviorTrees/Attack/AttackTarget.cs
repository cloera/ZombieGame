using UnityEngine;
using System.Collections;

public class AttackTarget : Task
{

	public override TASK_STATUS Run (Blackboard blackboard)
	{
		TASK_STATUS output = TASK_STATUS.FAILURE;

		if (blackboard.target != null) {

			blackboard.survivor.Fire (blackboard.target.transform.position);
			output = TASK_STATUS.SUCCESS;
		}

		return output;
	}

}

