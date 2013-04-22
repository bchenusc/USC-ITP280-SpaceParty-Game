using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	int score;
	bool gamePaused;
	bool gameStarted;
	bool gameOver;
	// Use this for initialization
	void Start () {
		score = 0;
		gamePaused = true;
		gameStarted = false;
		gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (gamePaused) {
			Time.timeScale = 0;	
		}
		else {
		Time.timeScale = 1;
		score += 1;
		}
	}
	
	void OnGUI () {
		
		
		if (gameStarted) {
			//Upper Left 
			GUI.Box(new Rect(10,10,100,50), "Score:" + score);
			guiText.text = "Score: " + score;
			if (GUI.Button (new Rect(10, 70, 100, 50), "Reset")) {
				Application.LoadLevel("globe");
			}
			
			//Upper Right Buttons
			if (GUI.Button(new Rect(Screen.width-85, 10, 75, 25), "Pause")) {
				toggleGamePause();
			}
			if (GUI.Button(new Rect(Screen.width-85, 45, 75, 25), "Quit")) {
				//Debug.Log("Quitting Game");
				Application.LoadLevel("menu");	
			}
		}
		
		else if (!gameStarted) {
			//Start button at center	
			if (GUI.Button(new Rect(Screen.width/2-125, Screen.height/2-80, 250, 100), "Start Game\n (Have your hand in place already!)")) {
				gameStarted = true;	
				gamePaused = false;
			}
		}
		if (gameOver)
			GUI.Box(new Rect(Screen.width/2-50, Screen.height/2-80, 100, 50), "Game over!");
	}
	
	void toggleGamePause() {
		if (!gameOver) {
			if (gamePaused) 
				gamePaused = false;
			else if (!gamePaused)
				gamePaused = true;
		}
	}
	
	void toggleGameOver() {
		gameOver = true;
	}
	

}
