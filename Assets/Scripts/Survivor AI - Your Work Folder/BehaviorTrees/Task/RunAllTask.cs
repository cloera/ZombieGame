using UnityEngine;
using System.Collections;

public class RunAllTask : Task
{
	public override TASK_STATUS Run (Blackboard blackboard)
	{
		TASK_STATUS output = TASK_STATUS.SUCCESS;

		foreach (Task t in children) {
			t.Run (blackboard);
		}
		return output;
	}
}

