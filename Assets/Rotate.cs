using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	
	private float rotate_Direction = 1; // 1 to rotate left, -1 to rotate right
	public float zVelocity; // Speed the object comes at you
	public GameObject explosion;
	public bool dropping = false;
	private float dropVelocity = 3;
	public GameObject my_camera;
	
	// Use this for initialization
	void Start () {
		float setRotate = Random.Range(-1, 1); // Get a random number betwen -1 and 1
		if(setRotate >= 0) { // If the random number is positive
			rotate_Direction = 1; // Set the rotation to left
			tag = "leftRotate"; // Set the tag
		} else { // If the random number is negative
			rotate_Direction = -1; // Set the rotation to right
			tag = "rightRotate"; // Set the tag
		}
		transform.position = new Vector3(Random.Range(-8, 8), Random.Range (-6, 6), 60); // Create this at a random x, y position betwen -10 and 10
		zVelocity = 10;
		
		my_camera = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(dropping == false) {
			transform.Rotate( new Vector3(0, 0, rotate_Direction) * 150 * Time.deltaTime); // Rotate this object
			if(transform.position.z >= 0) { // If the object is inside the view
				transform.position = transform.position - new Vector3(0, 0, zVelocity * Time.deltaTime); // Move towards the camera
			} else { // If the object is outside the view
				GameObject tempTexture = GameObject.Find("HUD");
				tempTexture.GetComponent<HUD_Script>().textureNumber++;
				tempTexture.SendMessage("changeTexture");
				Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
				Destroy(gameObject); // Destroy
			}
		} else {
			if(transform.position.y < -50) {
				Destroy(gameObject);
			}
			transform.position = transform.position - new Vector3(0, dropVelocity * Time.deltaTime, 0);
			dropVelocity += 1;
		}
	}
	
	void OnDestroy() {
		my_camera.SendMessage("incrementScore");
		//my_camera.SendMessage("endGame");
	}
}
