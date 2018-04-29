using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TASK_STATUS
{
	NULL,
	RUNNING,
	SUCCESS,
	FAILURE

}

public class Task {

	protected List<Task> children;

	public Task()
	{
		children = new List<Task> ();
	}

	public virtual TASK_STATUS Run(Blackboard blackboard)
	{

		return TASK_STATUS.NULL;
	}

	public void AddTask(Task t)
	{
		children.Add (t);
	}

	public void RemoveTask(Task t)
	{
		children.Remove (t);
	}

}
