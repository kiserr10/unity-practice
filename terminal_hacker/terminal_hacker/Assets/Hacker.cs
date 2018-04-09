using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {
	//Game configuration Data
	string[] level1Passwords = {"dog", "rajah", "matt", "ross", "hat"};
	string[] level2Passwords = {"safari", "elephant", "anaconda", "tiger", "cheetah"};
	string[] level3Passwords = {"encyclopedia", "dictionary", "washington", "alphabet", "xylophone"};

	//Game State//
	int level;
	string password;
	string gamePassword;
	enum Screen { MainMenu, Password, Win };
	Screen currentScreen = Screen.MainMenu;
	// Use this for initialization
	void Start () {
		print(level2Passwords);
		ShowMainMenu();
	}
	void OnUserInput (string input) {
		if (input == "menu"){
			ShowMainMenu();
		} else if (currentScreen == Screen.MainMenu){
			RunMainMenu(input);
		} else if (currentScreen == Screen.Password){
			RunLevel(input);
		}
	}
	void SetWinScreen(){
		currentScreen = Screen.Win;
		Terminal.ClearScreen();
		Terminal.WriteLine("Access Initiated...\n");
		ShowLevelReward();
	}
	void ShowLevelReward(){
		switch(level){
			case 1: 
				Terminal.WriteLine("Have a book...");
				break;
			case 2: 
				Terminal.WriteLine("Have a gun...");
				break;
			case 3: 
				Terminal.WriteLine("Have a nuke...");
				break;
		}
	}
	void RunLevel(string input){
		password = input;
		if (password == gamePassword){
			SetWinScreen();
		} else {
			ShowErrorMessage();
		}
	}
	void ShowErrorMessage(){
		Terminal.WriteLine("Access Denied!\nPlease Try Another Passcode...");
	}

	void RunMainMenu(string input){
		bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
		if (isValidLevelNumber){
			level = int.Parse(input);
			StartGame();
		} else {
			Terminal.WriteLine("Please Choose A Valid Department Number");	
		}
	}
	void StartGame(){
		currentScreen = Screen.Password;
		Terminal.ClearScreen();
		int index = Random.Range(0, 6);
		switch (level){
			case 1: 
				gamePassword = level1Passwords[index];
				break;
			case 2:
				gamePassword = level2Passwords[index];
				break;
			case 3: 
				gamePassword = level3Passwords[index];
				break;
			default:
				Debug.LogError("You Are Now Locked Out and Under Surveillence");
				break;
		}
		Terminal.WriteLine("You Have Chosen Level " + level + "\n");
		Terminal.WriteLine("Please Enter PassCode:");
	}
	void ShowMainMenu() {
		currentScreen = Screen.MainMenu;
		Terminal.ClearScreen();
		Terminal.WriteLine("Welcome To Hackers Inc.\n");
		Terminal.WriteLine("System Mainframe:\n");
		Terminal.WriteLine("1. Town Library");
		Terminal.WriteLine("2. Police Station");
		Terminal.WriteLine("3. CIA\n");
		Terminal.WriteLine("Press a Number to Choose a Department:");
	}
}
