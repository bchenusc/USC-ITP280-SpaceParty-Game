using UnityEngine;
using System.Collections;

public class Menu_Script : MonoBehaviour {
	public GUIStyle button;
	
	void Start() {
		button.fontSize = (int)(Screen.height * 0.040f);
	}
	
	void OnGUI() {
		// Roll Reversal
		if (GUI.Button(new Rect(Screen.width*0.395f - Screen.width*0.13f, Screen.height*0.42f - Screen.height*.083f, Screen.width*0.208f, Screen.height*0.178f), "", "label")) {
			Application.LoadLevel("asteroids");
		}
		
		// GlobeTrotter
		if (GUI.Button(new Rect(Screen.width*0.527f, Screen.height*0.42f - Screen.height*.083f, Screen.width*0.208f, Screen.height*0.178f), "", "label")) {
			Application.LoadLevel("globe");
		}
		
		// Space Darts
		if (GUI.Button(new Rect(Screen.width*0.395f - Screen.width*0.13f, Screen.height*0.59f, Screen.width*0.208f, Screen.height*0.178f), "", "label")) {
			Application.LoadLevel("darts");
		}
		
		// Firestorm Runner
		if (GUI.Button(new Rect(Screen.width*0.527f, Screen.height*0.59f, Screen.width*0.208f, Screen.height*0.178f), "", "label")) {
			Application.LoadLevel("fire");
		}
		
		// Quit Button
		if (GUI.Button(new Rect(Screen.width*0.400f, Screen.height*0.92f, Screen.width*0.200f, Screen.height*0.065f), "Quit", button)) {
			Application.Quit();
		}
	}
}
