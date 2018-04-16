using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float mainThrust = 1000f;
	
	enum State { Alive, Dying, Transcending };

	Rigidbody rigidBody;
	AudioSource audioSource;
	State state;
	

	// Use this for initialization

	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		float thrustThisFrame = mainThrust * Time.deltaTime;
		if(isAlive()) {
			Rotate();
			Thrust(thrustThisFrame);
			Reverse(thrustThisFrame);
		}
	}	

	void OnCollisionEnter(Collision collision){
		
		if(!isAlive()){ return; }

		switch (collision.gameObject.tag){
			case "Friendly":
				break; 
			case "Gold":
				print("Gold Aquired!");
				break;
			case "Finish":
				state = State.Transcending;
				Invoke("AdvanceNextLevel", 1f);
				break;
			case "Win":
				Invoke("Win", 1f);
				break;
			default:
				state = State.Dying;
				audioSource.Stop();
				Invoke("LoadFirstLevel", 1f);
				break;
		}
	}
	private void AdvanceNextLevel () {
		SceneManager.LoadScene(1);
	}
	private void LoadFirstLevel () {
		SceneManager.LoadScene(0);
	}
	private void Win () {
		state = State.Alive;
		print("Game Won!");
		SceneManager.LoadScene(0);
	}
	private bool isAlive () {
		return (state == State.Alive);
	}

	private void Thrust (float thrust) {
		float thrustThisFrame = mainThrust * Time.deltaTime;
		if (Input.GetKey(KeyCode.Space) && isAlive()) {
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
