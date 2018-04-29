using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

	float fuseTime = 3.0f;
	float throwspeed = 30.0f;
	float maxThrowDistance = 30.0f;
	Vector3 startPos;

	public Vector3 targetLocation;
	public GameObject explosionAnimPrefab;

	// Use this for initialization
	void Start () {

		startPos = transform.position;
		Invoke ("Explode", fuseTime);
	}

	// Update is called once per frame
	void Update () {

		if (Vector3.Distance (startPos, transform.position) < maxThrowDistance)
		{
			float step = throwspeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, targetLocation, step);
		}


	}

	public void SetTarget(Vector3 position)
	{
		targetLocation = new Vector3 (position.x, 1.0f, position.z);

	}

	void Explode()
	{
		Collider[] col = Physics.OverlapSphere (transform.position, 10.0f);

		foreach (Collider c in col) {
			Entity e = c.gameObject.GetComponent<Entity> ();

			if (e != null) {
				if (e.gameObject.layer != LayerMask.NameToLayer ("Survivor")) {
					EnemyHit hit;
					hit.damage = 50;

					e.OnHit (hit);
				}
			}
		}

		Instantiate (explosionAnimPrefab, transform.position, explosionAnimPrefab.transform.rotation);
		Destroy (gameObject);
	}
}
