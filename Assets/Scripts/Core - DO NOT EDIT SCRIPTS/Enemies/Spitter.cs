using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : SpecialEnemy {

	protected EnemyHit hit;

	public GameObject vomitPrefab;
	public Transform barrel;

	public Transform leftSide;
	public Transform rightSide;

	// Use this for initialization
	public override void Start () {
		hit.damage = 10;
		attackSpeed = 3.0f;

		enemytype = EnemyType.SPITTER;

		base.Start ();
		Invoke ("StartMoving", 0.5f);
	}

	void FixedUpdate () {
		if (state == EnemyState.NORMAL) {
			if (target == null) {
				FindClosestTarget ();
			} 
			else {
				if (isTargetInRange (50.0f) && isTargetVisible (target)) {
					agent.isStopped = true;
					transform.LookAt (target.transform.position);

					if (canAttack) {
						anim.SetTrigger ("Attack");
						canAttack = false;

						Invoke ("SpitVomit", 0.75f);

						Invoke ("Cooldown", attackSpeed);
					}
				}
				else {
					agent.isStopped = false;
					agent.SetDestination (target.transform.position);
				}

				if (canSearch == true) {
					Invoke ("FindClosestTarget", 1.0f);
					canSearch = false;
				}
			}


		}
	}

	void StartMoving()
	{
		anim.SetTrigger ("Spawned");
		state = EnemyState.NORMAL;
	}


	protected override void Dead ()
	{
		GameManager.RemoveEnemy (this, enemytype);
		base.Dead ();
	}

	void SpitVomit()
	{
		Instantiate (vomitPrefab, barrel.position, transform.rotation);
	}

	bool isTargetVisible(Survivor s)
	{
		bool output = false;

		RaycastHit hit1;
		RaycastHit hit2;
		Vector3 dir1 = s.transform.position - leftSide.transform.position;
		dir1.Normalize ();
		Vector3 dir2 = s.transform.position - rightSide.transform.position;
		dir2.Normalize ();

		if(Physics.Raycast (leftSide.position, dir1, out hit1, 1000.0f))
		{
			if (hit1.collider.gameObject.layer == LayerMask.NameToLayer ("Survivor")) {
				if (Physics.Raycast (rightSide.position, dir2, out hit2, 1000.0f)) {
					if (hit2.collider.gameObject.layer == LayerMask.NameToLayer ("Survivor")) {
						output = true;
					}
					//Debug.DrawLine (rightSide.position, hit2.point, Color.red);
				}
			}
			//Debug.DrawLine (leftSide.position, hit1.point, Color.red);
		}
		return output;
	}
}
