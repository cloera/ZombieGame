using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictAim : Task {

    public override TASK_STATUS Run(Blackboard blackboard)
    {
        TASK_STATUS output = TASK_STATUS.FAILURE;

        if(blackboard.target != null)
        {
            blackboard.predictedTarget = Pursue(blackboard.survivor, blackboard.target);

            output = TASK_STATUS.SUCCESS;
        }
        
        return output;
    }

    private Vector3 Pursue(Survivor survivor, Enemy target)
    {
        Vector3 dir = target.transform.position - survivor.transform.position;
        float numFramesAhead = 3.0f;
        Vector3 futurePosition = target.transform.position + (getVelocity(target) * numFramesAhead);

        return Seek(survivor, futurePosition);
    }

    private Vector3 Seek(Survivor s, Vector3 target)
    {
        Vector3 dir = target - s.transform.position;
        dir.Normalize();
        dir *= dir.magnitude;

        return dir;
    }

    public Vector3 getVelocity(Enemy e)
    {
        Rigidbody body = e.GetComponent<Rigidbody>();
        return body.velocity;

    }
}
