using UnityEngine;
using System.Collections;

public class Sequence : Task
{

	public override TASK_STATUS Run (Blackboard blackboard)
	{
		TASK_STATUS output = TASK_STATUS.SUCCESS;

		foreach (Task t in children) {
			if (t.Run (blackboard) == TASK_STATUS.FAILURE) {
				output = TASK_STATUS.FAILURE;
				break;
			}
		}
		return output;
	}

}

