using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour {
	[SerializeField] Text textComponent;
	[SerializeField] State startingState;
	[SerializeField] State caughtState;
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
        // Debug.Log(nextStates.Length);
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            state = startingState;
			textComponent.text = state.GetStoryState();
        }
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
        int randNumb = Random.Range(0, statesArr.Length);
		if(index == randNumb ){
			state = caughtState;
			textComponent.text = state.GetStoryState();
		} else {
			state = statesArr[index];
            textComponent.text = state.GetStoryState();
		}		
	}
}
