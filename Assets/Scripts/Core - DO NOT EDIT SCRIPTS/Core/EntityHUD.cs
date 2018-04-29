using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHUD : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.rotation = Quaternion.Euler (90, 0, 0);
	}
}
