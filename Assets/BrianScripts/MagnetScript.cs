using UnityEngine;
using System.Collections;

public class MagnetScript : MonoBehaviour {

	public Transform target;
	
	private Player_stats playerstatsScript;
	
	private int initSpawnTime;
	
	void Start(){
		initSpawnTime = (int)Time.timeSinceLevelLoad;
		playerstatsScript=target.GetComponent<Player_stats>();
		Destroy(gameObject, 20);
	}

	void OnTriggerEnter(Collider c){
		if (c.transform.CompareTag("Enemy"))
		{
			Destroy(c.gameObject);
		}
	}
	
	void Update(){
		transform.position = target.position;	
		if (((int)Time.timeSinceLevelLoad-initSpawnTime+1)%20 == 0){
			playerstatsScript.magnetOn = false;
		}
	}
}
