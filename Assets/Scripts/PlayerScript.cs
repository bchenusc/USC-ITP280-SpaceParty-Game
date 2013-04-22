using UnityEngine;
using System.Collections;
using Leap;

public class PlayerScript : MonoBehaviour {
	Controller controller;
	Vector palm_vector;
	float xVector;
	float yVector;
	float zVector;
	
	public GameObject my_camera;
	
	// Use this for initialization
	void Start () {
		controller = new Controller();
	}
	
	// Update is called once per frame
	void Update () {
		Frame frame = controller.Frame();
		Hand hand = frame.Hands[0];
		palm_vector = hand.PalmNormal;
		//Debug.Log(palm_vector);
		
		xVector = palm_vector.x;
		//yVector = palm_vector.y;
		zVector = palm_vector.z;
		
		if (this != null) 
			{pushPlayer(xVector, zVector);}	
	}
	
	void pushPlayer(float xDirection, float zDirection) {
		transform.Translate(xDirection*Time.deltaTime*-10, 0.0f, zDirection*Time.deltaTime*10, Space.World);
	}
	
	void OnDestroy() {
		if (!my_camera.Equals(null)) {
			my_camera.SendMessage("toggleGamePause");
			my_camera.SendMessage("toggleGameOver");
		}
	}
}
