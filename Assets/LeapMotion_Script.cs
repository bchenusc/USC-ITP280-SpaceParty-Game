using UnityEngine;
using System.Collections;
using Leap;

public class LeapMotion_Script : MonoBehaviour {
	Controller controller;
	Frame curFrame;
	Vector prevFingerPos;
	// Use this for initialization
	void Start () {
		controller = new Controller();
		controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
		prevFingerPos = new Vector(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		curFrame = controller.Frame();
		
		Finger curFinger = curFrame.Fingers[0];
		Vector curFingerPos = curFinger.TipPosition;
		
		bool canMove = true;
		if( Mathf.Abs(curFingerPos.x - prevFingerPos.x) > 15 ) {
			canMove = false;
		}
		if( Mathf.Abs(curFingerPos.y - prevFingerPos.y) > 15 ) {
			canMove = false;
		}
		
		//Debug.Log (pointerFingerPosition.x + " " + prevPointerPosition.x + " " + canMove);
		
		if(canMove == true) {
			Vector3 pos = Camera.main.ScreenToWorldPoint( new Vector3( (curFingerPos.x + 35) * 10, (curFingerPos.y - 40) * 7, 10) );
			transform.position = pos;
		}
		if(curFrame.Gestures().Count > 0) {
			CircleGesture circleItem = new CircleGesture(curFrame.Gestures()[0]);
			Vector circlePos = new Vector( (circleItem.Center.x + 35) * 10, (circleItem.Center.y - 40) * 7, 10);
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(circlePos.x, circlePos.y, 0)); // Ray from the camera to the mouse position
			Debug.DrawRay(Camera.main.ScreenToWorldPoint(new Vector3(circlePos.x, circlePos.y, 0)), Vector3.forward);
			RaycastHit hit; // Raycast Hit information
			if( Physics.Raycast(ray, out hit) ) { // If the raycast hits something
				hit.transform.gameObject.GetComponent<Rotate>().dropping = true;
			}
		}
		prevFingerPos = curFingerPos;
	}
}
