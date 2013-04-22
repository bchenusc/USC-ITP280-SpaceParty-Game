using UnityEngine;
using System.Collections;

public class Camera_Script : MonoBehaviour {
	int count;
	bool gameEnded;
	// Use this for initialization
	void Start () {
		count = 0;
		gameEnded = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		if (gameEnded) {
			GUI.Box(new Rect(Screen.width/2-100, Screen.height/2-35, 200, 70), "Game Over! Your Score is: " + count);
			if (GUI.Button(new Rect(Screen.width/2-50, Screen.height/2+35, 100, 70), "Quit Game")) {
				Application.LoadLevel("menu");
			}
			//Debug.Log("INGUI");
		}
	}
	
	void incrementScore() {
		if(!gameEnded) {
			++count;
		}
	}
	
	void endGame() {
		gameEnded = true;
		//Debug.Log("Game Ended");
	}
}
