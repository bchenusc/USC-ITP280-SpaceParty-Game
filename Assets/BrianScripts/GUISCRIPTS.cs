using UnityEngine;
using System.Collections;
using Leap;

public class GUISCRIPTS : MonoBehaviour {
	
	
	public AudioSource coinAudio;
	public AudioSource explosionAudio;
	public AudioSource powerupAudio;
	
	
	Controller controller;
	
	Frame curFrame;
	
	public bool start=false;
	private Vector3 spawnerLocation = new Vector3 (0,0, 755.1806f);
	public Transform player;
	public Transform spawner;
	
	public float timeAtStart=0;
	
	public int startBoxWidth=100;
	public int startBoxHeight=30;
	
	public GUIStyle guistyle;
	public GUIContent startContent;
	
	Player_move playerscript;
	
	public bool hideMsg1 = false;

	// Use this for initialization
	void Awake () {
		start = false;
	}
	
	void Start(){
		InitializeStartContent();
		
		startBoxWidth = (int)(UnityEngine.Screen.width*4/5.0f);
		startBoxHeight = (int)(UnityEngine.Screen.height*4/5.0f);
		controller = new Leap.Controller();
		Random.seed = System.DateTime.Now.Millisecond + System.DateTime.Now.Year +System.DateTime.Now.Hour+System.DateTime.Now.Second;
	}
	
	void OnGUI(){
		if (!start){
			GUI.Box (new Rect (UnityEngine.Screen.width/2-startBoxWidth/2,UnityEngine.Screen.height/2-startBoxHeight/2,startBoxWidth,startBoxHeight), startContent, guistyle );
			if(GUI.Button (new Rect (UnityEngine.Screen.width/2-UnityEngine.Screen.width/10/2,UnityEngine.Screen.height/2-UnityEngine.Screen.height/10/2+UnityEngine.Screen.height/5,UnityEngine.Screen.width/10,UnityEngine.Screen.height/10), "Start")){
				Transform newPlayer = Instantiate(player, transform.position, Quaternion.identity) as Transform;
				playerscript = newPlayer.GetComponent<Player_move>();
				Transform spawn = Instantiate(spawner, spawnerLocation, Quaternion.identity) as Transform;
				timeAtStart = Time.timeSinceLevelLoad;
				spawn.GetComponent<Enemy_spawn>().timeAtStart = timeAtStart;
				start = true;
			}
		}
		if (playerscript.playerDead){
			if(GUI.Button (new Rect (UnityEngine.Screen.width/2-UnityEngine.Screen.width/10/2-100,UnityEngine.Screen.height/2-UnityEngine.Screen.height/10/2+UnityEngine.Screen.height/5,UnityEngine.Screen.width/10,UnityEngine.Screen.height/10), "Quit")){
				//BACK TO MAIN MENU HERE
				Application.LoadLevel("menu");
				
			}
			if(GUI.Button (new Rect (UnityEngine.Screen.width/2-UnityEngine.Screen.width/10/2+100,UnityEngine.Screen.height/2-UnityEngine.Screen.height/10/2+UnityEngine.Screen.height/5,UnityEngine.Screen.width/10,UnityEngine.Screen.height/10), "Restart")){
				//Restart Game	
				Application.LoadLevel("fire");
			}
			
		}
		
	
	}
	
	void InitializeStartContent(){
		startContent.text = "How to Play: \n - Left Hand Controls Shuttle \n - Left Hand Hover and Right Hand Smash Respawns Shuttle";
	}
	
	void FixedUpdate(){
		if (!start){
			curFrame = controller.Frame();
			
			if (curFrame.Hands.Count>0){
				if (curFrame.Hands[0].PalmVelocity.y<-100){
					Instantiate(player, transform.position, Quaternion.identity);
					Transform spawn = Instantiate(spawner, spawnerLocation, Quaternion.identity) as Transform;
					timeAtStart = Time.timeSinceLevelLoad;
					spawn.GetComponent<Enemy_spawn>().timeAtStart = timeAtStart;
					start = true;
				}
			}
		}
	}
	
	
}
