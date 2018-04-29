using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public List<Camera> cameras = new List<Camera>();
	int index;
	Camera curCamera;

	// Use this for initialization
	void Start () {
		curCamera = cameras [0];
		index = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Jump")) {
			curCamera.enabled = false;

			index++;
			if (index >= cameras.Count) {
				index = 0;
			}
			curCamera = cameras [index];
			curCamera.enabled = true;
		}

	}
}
