using UnityEngine;
using System.Collections;

public class Shield_script : MonoBehaviour {
	
	public Transform target;
	public Transform explosion;
	
	public int hit = 0;
	
	void Start(){
		Destroy(gameObject, 10);	
	}

	void OnTriggerEnter(Collider c){
		if (c.transform.CompareTag("Enemy"))
		{
			hit++;
			Instantiate(explosion, c.transform.position, Quaternion.Euler(-90,0,0));
			Destroy(c.gameObject);
		}
	}
	
	void Update(){
		transform.position = target.position;
		if(hit>=3){
			Destroy(gameObject);	
		}
	}
}
