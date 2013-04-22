using UnityEngine;
using System.Collections;

public class GamePlayController : MonoBehaviour {
	int round = 0, dartNum = 0;
	public GameObject dartPrefab;
	GameObject dart;
	Vector3 gravDisplay;
	GameObject dartBoardPrefab;
	Vector3 boardVector;
	Vector3 boardPrevLoc;
	float gravMax = 13.0f;
	bool dartInScene;
	Dart dartScript;
	public GUIStyle infoStyle;
	public GUIStyle readyStyle;
	public GUIStyle gravHUDStyle;
	public GUIStyle gravVertStyle;
	public GUIStyle gravHorStyle;
	public GUIStyle gravHUDLabelStyle;
	public GUIStyle gravTextStyle;
	public GUIStyle instructionsStyle;
	int score = 0;
	
	
	// Use this for initialization
	void Start () {
		dartInScene = false;
		
		Random.seed = System.DateTime.Now.Millisecond + System.DateTime.Now.Second + System.DateTime.Now.Minute + System.DateTime.Now.Day + System.DateTime.Now.Year;
		
		dartBoardPrefab = GameObject.Find("DartBoard");
		dartBoardPrefab.transform.position = new Vector3(0, 8.075188f, -2.385722f);
		
		infoStyle.fontSize = (int)(Screen.height * 0.030f);
		gravHUDLabelStyle.fontSize = (int)(Screen.height * 0.030f);
		gravTextStyle.fontSize = (int)(Screen.height * 0.020f);
		instructionsStyle.fontSize = (int)(Screen.height * 0.022f);
		readyStyle.fontSize = (int)(Screen.height * 0.080f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
			Application.LoadLevel("darts");
		}
		
		if (Input.GetKeyDown(KeyCode.Q)) {
			Application.LoadLevel("menu");
		}
		
		switch (round) {
			case 0: {
				break;
			}
			case 1: {
				if (!dartInScene) {
					dartInScene = true;	
					spawnDart();
				}
			
				if (dartScript.getState() == Dart.DART_STATE.WAITING) {
					gravDisplay = new Vector3(0, Random.Range(-gravMax, gravMax), 0);
					dartScript.setGravity(gravDisplay);
					dartScript.setState(Dart.DART_STATE.AIMING);
				}
				break;	
			}
			case 2: {
				if (!dartInScene) {
					dartInScene = true;	
					spawnDart();
				}
			
				if (dartScript.getState() == Dart.DART_STATE.WAITING) {
					gravDisplay = new Vector3(Random.Range(-gravMax, gravMax), Random.Range(-gravMax, gravMax), 0);
					dartScript.setGravity(gravDisplay);
					dartScript.setState(Dart.DART_STATE.AIMING);
				}
				break;
			}
			case 3: {
				if (!dartInScene) {
					dartInScene = true;	
					spawnDart();
				}
			
				if (dartBoardPrefab.transform.position.x > -7.74f && dartBoardPrefab.transform.position.x < 8.26f && dartBoardPrefab.transform.position.y > 5.48f && dartBoardPrefab.transform.position.y < 10.36f) {
					boardPrevLoc = dartBoardPrefab.transform.position;
					dartBoardPrefab.transform.Translate(boardVector * Time.deltaTime * 2f, Space.World);
				} else {
					dartBoardPrefab.transform.position = boardPrevLoc;
					boardVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
				}
			
				if (dartScript.getState() == Dart.DART_STATE.WAITING) {
					gravDisplay = new Vector3(Random.Range(-gravMax, gravMax), Random.Range(-gravMax, gravMax), 0);
					dartScript.setGravity(gravDisplay);
					dartScript.setState(Dart.DART_STATE.AIMING);
				}
				break;
			}
			default: break;
		}
	}
	
	void spawnDart() {
		dart = Instantiate(dartPrefab) as GameObject;
		dartScript = dart.GetComponent<Dart>();
		dartNum++;
	}
	
	void OnGUI() {
		if (round < 4) {
			GUI.Label(new Rect (Screen.width*0.754f, Screen.height*0.525f, 20, 5), "Round: " + round, infoStyle);
		} else {
			GUI.Label(new Rect (Screen.width*0.754f, Screen.height*0.525f, 20, 5), "DONE", infoStyle);
		}
		GUI.Label(new Rect (Screen.width*0.754f, Screen.height*0.557f, 20, 5), "Score: " + score, infoStyle);
		
		GUI.BeginGroup(new Rect (Screen.width*(1f/32f), Screen.height*(1f/33f), Screen.width*(1f/6f), Screen.height*(1f/5.5f)));
		GUI.Box(new Rect (0, 0, Screen.width*(1f/6f), Screen.height*(1f/5.5f)), "", gravHUDStyle);
			
			GUI.Box(new Rect (Screen.width*(1f/6f) / 2f - 6, Screen.height*(1f/5.5f) / 2f, 12, (-gravDisplay.y / gravMax) * Screen.height*(1f/5.5f) / 2f), "", gravVertStyle);
			GUI.Box(new Rect (Screen.width*(1f/6f) / 2f, Screen.height*(1f/5.5f) / 2f - 6, (gravDisplay.x / gravMax) * Screen.width*(1f/6f) / 2f, 12), "", gravHorStyle);
			
			// Left/Right Grav Labels
			if (gravDisplay.x < 0) {
				// L
				GUI.Label(new Rect (Screen.width*0.010f, Screen.height*0.1200f / 2f, 10, 10), Mathf.FloorToInt(gravDisplay.x / gravMax * 100) + "%", gravTextStyle);
				
				// R
				GUI.Label(new Rect (Screen.width*0.140f, Screen.height*0.2100f / 2f, 10, 10), "0%", gravTextStyle);
			} else {
				// L
				GUI.Label(new Rect (Screen.width*0.010f, Screen.height*0.1200f / 2f, 10, 10), "0%", gravTextStyle);
				
				// R
				GUI.Label(new Rect (Screen.width*0.140f, Screen.height*0.2100f / 2f, 10, 10), Mathf.FloorToInt(gravDisplay.x / gravMax * 100) + "%", gravTextStyle);
			}
		
			// Top/Bottom Grav Labels
			if (gravDisplay.y > 0) {
				// T
				GUI.Label(new Rect (Screen.width*0.092f, Screen.height*0.0160f / 2f, 10, 10), Mathf.FloorToInt(gravDisplay.y / gravMax * 100) + "%", gravTextStyle);
				
				// B
				GUI.Label(new Rect (Screen.width*0.050f, Screen.height*0.3200f / 2f, 10, 10), "0%", gravTextStyle);
			} else {
				// T
				GUI.Label(new Rect (Screen.width*0.092f, Screen.height*0.0160f / 2f, 10, 10), "0%", gravTextStyle);
				
				// B
				GUI.Label(new Rect (Screen.width*0.050f, Screen.height*0.3200f / 2f, 10, 10), Mathf.FloorToInt(gravDisplay.y / gravMax * 100) + "%", gravTextStyle);
			}
			
			
		GUI.EndGroup();
		
		GUI.Box(new Rect (Screen.width*0.058f, Screen.height*0.212f, Screen.width*0.11f, Screen.height*0.04f), "Gravity", gravHUDLabelStyle);
		
		// Instructions
		GUI.Label(new Rect (Screen.width*0.000f, Screen.height*0.810f, Screen.width*(1.3f/5f), Screen.height*(1.0f/6f)), "Instructions:\n - Aim Dart with Left Pointer Finger\n - Fire Dart with Right Finger Tap\n'R' to Restart\n'Q' to Quit", instructionsStyle);
		
		// Start Button
		if (round == 0) {
			if (GUI.Button(new Rect(Screen.width/2f - Screen.width*(1.3f/5f/2f), Screen.height/2 - Screen.height*(1.0f/6f/2f), Screen.width*(1.3f/5f), Screen.height*(1.0f/6f)), "READY", readyStyle)) {
				round++;
			}
		}
		
		// Restart and Menu Buttons
		if (round >= 4) {
			if (GUI.Button(new Rect(Screen.width/2f - Screen.width*(1.3f/5f/2f), Screen.height/2 - Screen.height*(0.8f/6f/2f), Screen.width*(1.3f/5f), Screen.height*(0.8f/6f)), "AGAIN", readyStyle)) {
				Application.LoadLevel("darts");
			}
			if (GUI.Button(new Rect(Screen.width/2f - Screen.width*0.0867f, Screen.height/2 - Screen.height*(1.0f/6f/4f) + Screen.height*.115f, Screen.width*0.1734f, Screen.height*(1.0f/6f/2f)), "MENU", readyStyle)) {
				//Application.LoadLevel("menu");
			}
		}
	}
	
	public void canSpawnDart(bool n) {
		if (dartNum >= 3) {
			dartNum = 0;
			round++;
			boardVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
		}
		dartInScene = !n;	
	}
	
	public void increaseScore(int amount) {
		score += amount;	
	}
}
