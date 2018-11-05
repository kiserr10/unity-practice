using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour {
	[SerializeField] Text textComponent;
	[SerializeField] State startingState;
	// Use this for initialization
	State state;
	void Start () {
		state = startingState;
		textComponent.text = state.GetStoryState();
	}	
	// Update is called once per frame
	void Update () {
		ManageState();
	}

	public void ManageState() {
		var nextStates = state.GetNextStates();
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			ChangeState(nextStates, 0);
		} 
		else if (Input.GetKeyDown(KeyCode.Alpha2)){
			ChangeState(nextStates, 1);
		} 
		else if (Input.GetKeyDown(KeyCode.Alpha3)){
			if (nextStates.Length >= 3) ChangeState(nextStates, 2);
		} 
		else if(Input.GetKeyDown(KeyCode.Alpha4)){
            if (nextStates.Length >= 4) ChangeState(nextStates, 3);
		}
	}
	private void ChangeState(State[] statesArr, int index) {
		state = statesArr[index];
		textComponent.text = state.GetStoryState();
	}
}
