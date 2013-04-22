using UnityEngine;
using System.Collections;

public class Player_stats : MonoBehaviour {

	public int score = 0;
	public bool move2D = false;
	public bool magnetOn = false;
	
	Player_move playerScript;
	
	public GUIStyle guistyle;
	
	void Start(){
		playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_move>();	
	}
	
	void OnTriggerEnter(Collider c){
		if (c.transform.CompareTag("Coin")){
			score+=25;
		}
		if (c.transform.CompareTag ("Magnet")){
			magnetOn = true;	
		}
	}
	
	void OnGUI(){
		GUI.Box (new Rect (20,20,50,30), ""+score, guistyle);
	}
	void FixedUpdate(){
		if(!IsInvoking("addScore") && !playerScript.playerDead){
			InvokeRepeating("addScore",2,1);	
		}
		if (playerScript.playerDead){
			CancelInvoke("addScore");	
		}
	}
	
	void addScore(){
		score++;	
	}
}
