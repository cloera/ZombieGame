using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerExplode : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	
		Collider[] col = Physics.OverlapSphere (transform.position, 10.0f);

		foreach (Collider c in col) {
			Entity e = c.gameObject.GetComponent<Entity> ();

			if (e != null) {
				EnemyHit hit;
				hit.damage = 20;

				e.OnHit (hit);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
