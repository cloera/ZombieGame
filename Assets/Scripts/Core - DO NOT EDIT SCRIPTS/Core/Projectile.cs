using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ProjectileHit
{
	public float damage;
}

public class Projectile : MonoBehaviour {

	protected Rigidbody body;
	public float speed;

	protected ProjectileHit hitData;

	// Use this for initialization
	public virtual void Start () {
		body = GetComponent<Rigidbody> ();

		body.AddForce (transform.forward * speed);



		Destroy (gameObject, 5.0f); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
