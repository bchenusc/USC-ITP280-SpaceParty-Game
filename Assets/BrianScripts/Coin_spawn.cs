using UnityEngine;
using System.Collections;

public class Coin_spawn : MonoBehaviour {

	private float spawnTimer;
	public float resetSpawnTimer=10;
	
	Enemy_spawn enemySpawnScript;
	
	public Transform coinPrefab;
	
	public int screenwidth=0;
	
	void Start(){
		spawnTimer = 0;	
		enemySpawnScript = transform.GetComponent<Enemy_spawn>();
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTimer==0){
			//Spawn a coin
			Vector3 pos = new Vector3(Random.Range(-screenwidth/2, screenwidth/2),transform.position.y, transform.position.z);
			Transform clone = Instantiate(coinPrefab, pos, Quaternion.identity)as Transform;
			clone.GetComponent<Enemy_move>().speed = enemySpawnScript.max_speed;
			spawnTimer=resetSpawnTimer;
		}
		if (spawnTimer<0){
			spawnTimer=0;
		}
		if (spawnTimer>0){
			spawnTimer-=Time.deltaTime;	
		}
	}
}
