/*
using UnityEngine;
using System.Collections;
using Leap;

public class BrianScript : MonoBehaviour {
	Controller controller;
	// Use this for initialization
	void Start () {
	controller = new Controller();
	}
	
	// Update is called once per frame
	void Update () {
		//Rotation of the object.
		//Quaternion
		Vector3 palmXNormal = lH.PalmNormal.ToUnity();
		//palmXNormal.z =0;
		//Quaternion lookRotation = Quaternion.FromToRotation(-Vector3.up, palmXNormal);
		//transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation,Time.deltaTime*rotationSpeed);
		
		Vector3 moveDir = new Vector3(0,0,0);
		
		if (palmXNormal.x >0.2){
			if(transform.position.x>-boundaryWidth/2){
			moveDir += new Vector3(-1,0,0);
			}
		}
		if (palmXNormal.x <-0.2){
			if(transform.position.x<boundaryWidth/2){
				moveDir += new Vector3(1,0,0);	
			}
		}
		if (palmXNormal.z<-0.2 && playerStatsScript.move2D == true){
			if (transform.position.z< boundaryHeight/2){
			//Go forwards
			moveDir += new Vector3(0,0,1);
			}
		}
		if (palmXNormal.z>0.2 && playerStatsScript.move2D ==true){
			if (transform.position.z>-boundaryHeight/2){
			//Go backwards
			moveDir+=new Vector3(0,0,-1);
			}
		}
		
		//moveDir = (transform.position-lH.Finger(0).TipPosition
		
		transform.Translate(moveDir*speed);
	}
}
*/