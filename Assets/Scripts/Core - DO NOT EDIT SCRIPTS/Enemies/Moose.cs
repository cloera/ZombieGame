using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moose : SpecialEnemy {

	protected EnemyHit hit;

	private bool enrage;
	public float enrageSpeed;

	public Transform exitLocation;

	public LayerMask rayMask;



	// Use this for initialization
	public override void Start () {
		hit.damage = 50;
		attackSpeed = 1.0f;

		enemytype = EnemyType.MOOSE;
		enrage = false;

		exitLocation = GameManager.getRandomZombieSpawnLocation ();

		base.Start ();

		agent.SetDestination (exitLocation.position);
		Invoke ("StartMoving", 0.5f);

		Invoke ("ScanArea", 1.0f);
	}

	void FixedUpdate () {
		if (state == EnemyState.NORMAL) {
			if (enrage == true) {
				agent.SetDestination (target.transform.position);

				if (canAttack) {
					if (isTargetInRange (6.0f)) {
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

		if (Vector3.Distance (transform.position, exitLocation.position) < 1.0f) {
			//Destroy (gameObject);
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

	public override void OnHit (EnemyHit hit)
	{
		base.OnHit (hit);
		CanadianFury ();
	}

	public override void OnHit (ProjectileHit proj)
	{
		base.OnHit (proj);
		CanadianFury ();
	}

	void CanadianFury()
	{
		if (enrage == false) {
			agent.speed = enrageSpeed;
			enrage = true;
			FindClosestTarget ();
		}
	}

	void ScanArea()
	{
		if (enrage == false) {
			List<Survivor> survivors = GameManager.getSurvivorList ();

			foreach (Survivor s in survivors) {
				SurvivorDetect (s);
			}

			Invoke ("ScanArea", 1.0f);
		}

	}

	void SurvivorDetect(Survivor s)
	{
		RaycastHit hit;
		Vector3 dir = s.transform.position - transform.position;
		dir.Normalize ();

		if (Physics.Raycast (transform.position, dir, out hit, 15.0f, rayMask)) {

			if (hit.collider.gameObject.layer == LayerMask.NameToLayer ("Survivor")) {
				enrage = true;
				target = s;
				agent.speed = enrageSpeed;
			}

		}
	}


}
