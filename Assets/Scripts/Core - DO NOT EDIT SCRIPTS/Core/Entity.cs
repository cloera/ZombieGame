using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Entity : MonoBehaviour {

	public float health;

	Slider healthBar;

	// Use this for initialization
	public virtual void Start () {
		healthBar = GetComponentInChildren<Slider> ();

		healthBar.minValue = 0;
		healthBar.maxValue = health;
		healthBar.value = health;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected virtual void Dead()
	{
		Destroy(gameObject);
	}

	public virtual void OnHit(ProjectileHit proj)
	{
		health -= proj.damage;
		healthBar.value = health;

		if (health <= 0) {
			Dead ();
		}

	}

	public virtual void OnHit(EnemyHit hit)
	{
		health -= hit.damage;
		healthBar.value = health;

		if (health <= 0) {
			Dead ();
		}
	}

	protected void AddHealth(float value)
	{
		health = Mathf.Clamp (value + health, 0, 100);
		healthBar.value = health;

	}
}
