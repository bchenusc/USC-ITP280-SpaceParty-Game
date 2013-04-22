using UnityEngine;
using System.Collections;

public class Scroll_bg : MonoBehaviour {
	
	public Transform enemySpawn;
	Enemy_spawn enemySpawnScript;
	float offset = 0;
	
	// Use this for initialization
	void Start () {
		if (enemySpawn)
		enemySpawnScript = enemySpawn.GetComponent<Enemy_spawn>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!enemySpawnScript){
			if (enemySpawn){
				enemySpawnScript = enemySpawn.GetComponent<Enemy_spawn>();
			}
		}
		
		if (enemySpawnScript){
			offset -= enemySpawnScript.max_speed/60*Time.deltaTime+(enemySpawnScript.max_speed-2)*3;
		}
		else{
			offset -= 4.0f/60*Time.deltaTime;	
		}
		renderer.material.SetTextureOffset("_MainTex", new Vector2(0,offset));
	}
	

}
