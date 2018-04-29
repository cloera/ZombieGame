using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Zombie : Enemy {

	protected EnemyHit hit;


	// Use this for initialization
	public override void Start () {
		hit.damage = 10;
		attackSpeed = 1.0f;

		enemytype = EnemyType.ZOMBIE;

		base.Start ();
		Invoke ("StartMoving", 0.5f);
	}
	
	void FixedUpdate () {
		if (state == EnemyState.NORMAL) {
			if (target == null) {
				FindClosestTarget ();
			} else {
				agent.SetDestination (target.transform.position);

				if (canAttack) {
					if (isTargetInRange (2.0f)) {
						anim.SetTrigger ("Attack");
						canAttack = false;

						target.OnHit (hit);

						Invoke ("Cooldown", attackSpeed);
					}
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
}
