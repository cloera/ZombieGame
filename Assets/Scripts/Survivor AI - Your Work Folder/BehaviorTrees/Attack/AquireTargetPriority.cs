using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AquireTargetPriority : Task
{

	public override TASK_STATUS Run (Blackboard blackboard)
	{
		TASK_STATUS output = TASK_STATUS.FAILURE;

		Enemy target = FindClosestTarget (blackboard.survivor.transform.position);

		if (target != null) {
			blackboard.target = target;
			output = TASK_STATUS.SUCCESS;
		}

		return output;
	}

	Enemy FindClosestTarget(Vector3 survivorPosition)
	{
		Enemy output = null;
		float closestSoFar = Mathf.Infinity;

		List<Enemy> specials = GameManager.getSpecialList ();
		List<Enemy> zombies = GameManager.getZombieList ();

		output = findClosestFromList(specials, survivorPosition, ref closestSoFar);

		if (output == null) {
			output = findClosestFromList (zombies, survivorPosition, ref closestSoFar);
		}

		return output;
	}

	Enemy findClosestFromList(List<Enemy> list, Vector3 survivorPosition, ref float closestSoFar)
	{
		Enemy output = null;

		for (int i = 0; i < list.Count; i++) {
			Enemy e = list [i];

			float dist = (e.transform.position - survivorPosition).magnitude;
			if (dist < closestSoFar) {
				if (isTargetVisible (e, survivorPosition)) {
					output = e;
					closestSoFar = dist;
				}
			}
		}

		return output;
	}

	bool isTargetVisible(Enemy e, Vector3 rayOrigin)
	{
		bool output = false;

		RaycastHit hit;
		Vector3 dir = e.transform.position - rayOrigin;
		dir.Normalize ();

		if(Physics.Raycast (rayOrigin, dir, out hit, 1000.0f))
		{
			if (hit.collider.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {

				if (hit.collider.gameObject.GetInstanceID () == e.gameObject.GetInstanceID ()) {
					output = true;
				}
			}
		}
		return output;
	}
}

