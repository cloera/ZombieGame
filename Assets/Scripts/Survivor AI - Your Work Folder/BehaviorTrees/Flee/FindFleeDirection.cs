using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FindFleeDirection : Task
{

	public override TASK_STATUS Run (Blackboard blackboard)
	{
		TASK_STATUS output = TASK_STATUS.SUCCESS;

		blackboard.fleeDirection = FleeDirection (blackboard);

        //Debug.Log("FindFleeDirection - output: " + output);

        return output;
	}

	Vector3 FleeDirection(Blackboard bd)
	{
        Vector3 output = Vector3.zero;

        List<Enemy> zombies = GameManager.getZombieList();
        List<Enemy> special = GameManager.getSpecialList();

        Vector3 survivorPosition = bd.survivor.transform.position;

        for (int i = 0; i < special.Count; i++)
        {
            Enemy e = special[i];

            Vector3 dir = survivorPosition - e.transform.position;
            float dist = dir.magnitude;
            if (dist < bd.fleeDistance)
            {
                if (isTargetVisible(e, survivorPosition))
                {
                    dir.Normalize();
                    dir *= bd.fleeDistance;
                    output += dir;
                }
            }
        }

        for (int i = 0; i < zombies.Count; i++) {
			Enemy e = zombies [i];

			Vector3 dir = survivorPosition - e.transform.position;
			float dist = dir.magnitude;
			if (dist < bd.fleeDistance) {
				if (isTargetVisible (e, survivorPosition)) {
					dir.Normalize ();
					dir *= bd.fleeDistance;
					output += dir;
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

