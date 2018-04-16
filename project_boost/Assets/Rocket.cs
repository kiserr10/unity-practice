using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float mainThrust = 1000f;
	

	Rigidbody rigidBody;
	AudioSource audioSource;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		float thrustThisFrame = mainThrust * Time.deltaTime;
		Rotate();
		Thrust(thrustThisFrame);
		Reverse(thrustThisFrame);
	}	

	void OnCollisionEnter(Collision collision){
		switch (collision.gameObject.tag){
			case "Friendly":
				print("Safe");
				break; 
			case "Fuel":
				print("Fuel Aquired!");
				break;
			case "Gold":
				print("Gold Aquired!");
				break;
			default:
				print("TOAST!");
				break;
		}
	}

	private void Thrust (float thrust) {
		float thrustThisFrame = mainThrust * Time.deltaTime;
			if (Input.GetKey(KeyCode.Space)) {
			rigidBody.AddRelativeForce(Vector3.up * thrust);
			if (!audioSource.isPlaying){
				audioSource.Play();
			}
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			audioSource.Stop();
		}
	}
	private void Reverse (float thrust) {
		if(Input.GetKey(KeyCode.W)) {
			rigidBody.AddRelativeForce(Vector3.down * thrust);
		}
	}

	private void Rotate() {
		//take manual control of rotation
		rigidBody.freezeRotation = true;
		
		float rotationThisFrame = rcsThrust * Time.deltaTime;

		if (Input.GetKey(KeyCode.A)){
			transform.Rotate(Vector3.forward * rotationThisFrame);
		} else if (Input.GetKey(KeyCode.D)){
			transform.Rotate(-Vector3.forward * rotationThisFrame);
		}
		rigidBody.freezeRotation = false;
	}
}
