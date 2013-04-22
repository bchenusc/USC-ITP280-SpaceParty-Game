using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn_Populate : MonoBehaviour {
	
	Enemy_spawn spawnScript;
	
	List<int> wave = new List<int>{0,1,2,3,5,6,7,8,15,9,14,10,11,12,13};
	
	// Use this for initialization
	void Start () {
		spawnScript = transform.GetComponent<Enemy_spawn>();
		for (int i=0; i<wave.Count; i++){
			spawnScript.AddWave(wave[i]);
		}
		
		for(int i=0; i<100; i++){
			spawnScript.AddWave(Random.Range(0,22));	
		}
		spawnScript.AddWave (23);
		
	}
}
