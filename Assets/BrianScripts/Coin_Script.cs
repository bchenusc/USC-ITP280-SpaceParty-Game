using UnityEngine;
using System.Collections;

public class Coin_Script : MonoBehaviour {
	
	Transform player;
	public float speed=1;
	Player_stats player_stats;
	GUISCRIPTS guiscript;
	
	void Start(){
		guiscript = GameObject.Find ("GUIHANDLE").GetComponent<GUISCRIPTS>();
		player = GameObject.FindGameObjectWithTag("Player").transform;	
		player_stats = player.GetComponent<Player_stats>();
	}
	
	void OnTriggerEnter(Collider c){		
		if (c.transform.CompareTag("DestroyEnemy")){
			Destroy(gameObject);	
		}
		if (c.transform.CompareTag("Player")){
			guiscript.coinAudio.Play();
			Destroy (gameObject);	
		}
	}
	
	void Update(){
		if (Vector3.Distance(transform.position,player.position)<200 && player_stats.magnetOn){
			transform.rotation=Quaternion.identity;
			transform.Translate((player.position-transform.position).normalized*speed*500*Time.deltaTime);	
			
		}else{
			transform.Translate(-Vector3.forward*75*Time.deltaTime*speed);
			transform.Rotate(new Vector3(0,0,1)*Time.deltaTime*100);
		}
	}
}
