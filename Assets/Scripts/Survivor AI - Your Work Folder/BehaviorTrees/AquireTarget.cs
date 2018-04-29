using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquireTarget : Task {


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
		List<Enemy> zombies = GameManager.getZombieList ();

		for (int i = 0; i < zombies.Count; i++) {
			Enemy e = zombies [i];

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
