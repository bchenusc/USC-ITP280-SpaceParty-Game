using UnityEngine;
using System.Collections;

public class Move2D_script : MonoBehaviour {
	
	void OnTriggerEnter(Collider c){
		if (c.transform.CompareTag("Player")){
			c.transform.GetComponent<Player_stats>().move2D = true;
			Destroy(gameObject);
		}
		
		if (c.transform.CompareTag("DestroyEnemy")){
			Destroy(gameObject);	
		}

	}
}
