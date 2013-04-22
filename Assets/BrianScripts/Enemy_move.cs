using UnityEngine;
using System.Collections;

public class Enemy_move : MonoBehaviour {
	
	public float speed = 1.0f;
	
	public Vector3 direction;
	
	// Update is called once per frame
	void Update () {
		transform.Translate(direction*speed*75*Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider c){
		if (c.transform.CompareTag("DestroyEnemy")){
			Destroy(gameObject);	
		}
	}
}
