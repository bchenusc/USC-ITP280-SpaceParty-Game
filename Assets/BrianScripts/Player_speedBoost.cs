using UnityEngine;
using System.Collections;

public class Player_speedBoost : MonoBehaviour {

	void OnTriggerEnter(Collider c){		
		if (c.transform.CompareTag("Player")){
			c.transform.GetComponent<Player_move>().speed ++;
			Destroy(gameObject);
		}

		if (c.transform.CompareTag("DestroyEnemy")){
			Destroy(gameObject);	
		}
	
	}
}
