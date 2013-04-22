using UnityEngine;
using System.Collections;

public class FallingMagnetScript : MonoBehaviour {

	Transform player;
	public Transform magnet;
	public float speed=1;
	Player_stats player_stats;
	GUISCRIPTS guiscript;
	
	void Start(){
		player = GameObject.FindGameObjectWithTag("Player").transform;
		player_stats = player.GetComponent<Player_stats>();
		guiscript = GameObject.Find("GUIHANDLE").GetComponent<GUISCRIPTS>();
	}
	
	void OnTriggerEnter(Collider c){		
		if (c.transform.CompareTag("DestroyEnemy")){
			Destroy(gameObject);	
		}
		if (c.transform.CompareTag("Player")){
			player_stats.magnetOn = true;
			Transform clone = Instantiate(magnet, c.transform.position,  Quaternion.Euler(new Vector3(90,0,0)))as Transform;
			clone.GetComponent<MagnetScript>().target = player;
			guiscript.powerupAudio.Play();
			Destroy (gameObject);	
		}
	}
	
	void Update(){
			transform.Translate(-Vector3.forward*75*Time.deltaTime*speed);
			transform.Rotate(new Vector3(0,0,1)*Time.deltaTime*100);
	}
}
