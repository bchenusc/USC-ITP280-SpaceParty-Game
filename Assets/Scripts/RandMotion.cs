using UnityEngine;
using System.Collections;

public class RandMotion : MonoBehaviour {
	
	private Vector3 direction_;
	public float speed_multiplier;
	public float repeat_rate;
	public GameObject player;
	float rand1, rand2, rand3;
	
	// Use this for initialization
	void Start () {
		speed_multiplier = 90f;
		repeat_rate = 3f;
		InvokeRepeating("updateDir",1, repeat_rate);
	}
	
	// Update is called once per frame
	void Update () {
		rotateDir(direction_);
	}
	
	void updateDir() {
		direction_ = randDir();
		speed_multiplier += 7.5f;
		repeat_rate -= 0.05f;
	}
	
	Vector3 randDir () {
		Vector3 direction;
		rand1 = Random.Range(-1.0f, 1.0f);	
		rand2 = Random.Range(-1.0f, 1.0f);	
		rand3 = Random.Range(-1.0f, 1.0f);
		
		direction = new Vector3(rand1, rand2, rand3);
		return direction;
	}
	
	void rotateDir(Vector3 newDir) {
		transform.Rotate(newDir*speed_multiplier*Time.deltaTime,Space.World);
	}
	
	//Ensures that the player does not sit still on the platform
	void pushObject(Vector3 newDir) { 
		player.rigidbody.AddForce(rand1*-5, 0, rand3*-5);
	}
	
	void OnTriggerStay(Collider other) {
		pushObject(direction_);
	}
}
