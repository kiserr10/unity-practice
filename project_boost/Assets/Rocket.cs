using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	Rigidbody rigidBody;
	AudioSource audioSource;
	bool isRocketFiring;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		isRocketFiring = false;
	}
	
	// Update is called once per frame
	void Update () {
		ProcessInput();
	}

	private void ProcessInput() {
		if (Input.GetKey(KeyCode.Space)) {
			rigidBody.AddRelativeForce(Vector3.up);
			if (!isRocketFiring){
				isRocketFiring = true;
				audioSource.Play();
			}
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			isRocketFiring = false;
			audioSource.Stop();
		}
		if (Input.GetKey(KeyCode.A)){
			transform.Rotate(Vector3.forward);
		} else if (Input.GetKey(KeyCode.D)){
			transform.Rotate(-Vector3.forward);
		}
	}
}
