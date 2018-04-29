using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public struct EnemyHit
{
	public float damage;
}

public enum EnemyState
{
	SPAWNING,
	NORMAL
}

public enum EnemyType
{
	ZOMBIE,
	BOOMER,
	SPITTER,
	MOOSE

}

public class Enemy : Entity {

	protected Survivor target;

	protected NavMeshAgent agent;
	protected EnemyState state;

	protected Animator anim;
	protected CapsuleCollider capCollider;

	protected bool canAttack;
	protected bool canSearch;
	protected float attackSpeed;

	public GameObject splatPrefab;

	protected EnemyType enemytype;
	// Use this for initialization
	public override void Start () {
		base.Start ();

		agent = GetComponent<NavMeshAgent> ();
		state = EnemyState.SPAWNING;

		anim = GetComponentInChildren<Animator> ();

		canSearch = true;
		canAttack = true;
		capCollider = GetComponent<CapsuleCollider> ();

		GameManager.AddEnemy (this, enemytype);
	}

	public EnemyType GetEnemyType()
	{
		return enemytype;
	}

	protected void FindClosestTarget()
	{
		target = null;

		List<Survivor> survivors = GameManager.getSurvivorList ();

		float closestDist = Mathf.Infinity;
		for (int i = 0; i < survivors.Count; i++) {
			if (survivors [i].GetSurvivorState () == SURVIVORSTATE.NORMAL) {
				float dist = Vector3.Distance (survivors [i].transform.position, transform.position);
				if (dist < closestDist) {
					closestDist = dist;
					target = survivors [i];
				}
			}
		}

		canSearch = true;
	}

	protected bool isTargetInRange(float attackRange)
	{
		bool output = false;

		if (Vector3.Distance(target.transform.position, transform.position) < attackRange) {
			output = true;
		}
		return output;
	}

	protected void Cooldown()
	{
		canAttack = true;
	}

	protected override void Dead ()
	{
		base.Dead ();

		Instantiate (splatPrefab, transform.position, splatPrefab.transform.rotation);
	}

}
