using UnityEngine;
using System.Collections;

public class Selector : Task
{

	public override TASK_STATUS Run (Blackboard blackboard)
	{
		TASK_STATUS output = TASK_STATUS.FAILURE;

		foreach (Task t in children) {
			if (t.Run (blackboard) == TASK_STATUS.SUCCESS) {
				output = TASK_STATUS.SUCCESS;
				break;
			}
		}
		return output;
	}
}

