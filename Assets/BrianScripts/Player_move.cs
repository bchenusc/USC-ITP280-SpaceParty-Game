using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class Player_move : MonoBehaviour 
{	
	Controller controller;
	
	Frame curFrame;
	
	public int boundaryWidth = 0;
	public int boundaryHeight= 0;
	
	private Hand lH;
	private Hand rH;
	private float prevRYVelocity = 0;
	
	private bool canPush;
	private float curTimer;
	private float resetTimer;
	
	public bool playerDead=false;
	
	public Transform shield;
	
	public Transform explosion;

	public float speed=5.0f;
	
	Player_stats playerStatsScript;
	
	void Start(){
		controller = new Leap.Controller();
		playerStatsScript = transform.GetComponent<Player_stats>();

		curFrame = controller.Frame ();
		canPush = false;
		curTimer = 0;
		resetTimer = 1;
	}
	
	void Update(){
		if (Input.GetKeyDown(KeyCode.G)){
			Transform myshield = Instantiate(shield,transform.position, Quaternion.Euler(90,0,0)) as Transform;
			myshield.GetComponent<Shield_script>().target = transform;
		}	
	}
	
	void FixedUpdate(){
		curFrame = controller.Frame();
		#region Timer for hand push
		//TIMER FOR HAND PUSH
			if(curTimer==0){
				canPush = true;
			}
			if (curTimer>0){
				curTimer-=Time.deltaTime;
			}
			if (curTimer<0){
				curTimer=0;
			}
		#endregion
		
		//If there is 2 or more hands.
		if(curFrame.Hands.Count>1&&curFrame.Hands.Count<3){
			//Rotation of palm determines rotation of player.
			if (curFrame.Hands[0].PalmPosition.x<curFrame.Hands[1].PalmPosition.x){
				lH= curFrame.Hands[0];
				rH = curFrame.Hands[1];
				
			}else{
				lH = curFrame.Hands[1];
				rH = curFrame.Hands[0];
			}
			
			prevRYVelocity =  rH.PalmVelocity.y;
		//If there is only one hand.
		}else if (curFrame.Hands.Count ==1){
			//Debug.Log (curFrame.Hands.Count);
			lH = curFrame.Hands[0];
			if ((lH.PalmVelocity.y<-100) && prevRYVelocity<-100 && canPush){
				canPush = false;
				curTimer = resetTimer;
			}
			
			prevRYVelocity =  lH.PalmVelocity.y;
		}else{
			#region more than 2 hands.
			//MORE THAN 2 HANDS. THIS ACCOUNTS FOR HEAD>
			if (curFrame.Hands.Count>2){
				int handIndex = 0;
				int handIndex1 =0;
				int handIndex2 = 0;
				float lowestHand1=1000;
				float lowestHand2=1000;
				foreach (Hand hand in curFrame.Hands){
					//Find the lowest 2 hand positions
					if (hand.PalmPosition.y<lowestHand1){
						lowestHand1=hand.PalmPosition.y;
						handIndex1 = handIndex;
					}
					else if (hand.PalmPosition.y<lowestHand2){
						lowestHand2=hand.PalmPosition.y;	
						handIndex2 = handIndex;
					}
					handIndex++;
				}
				//Debug.Log (lowestHand1+" , "+lowestHand2);
				if (curFrame.Hands[handIndex1].PalmPosition.x<curFrame.Hands[handIndex2].PalmPosition.x){
					lH= curFrame.Hands[handIndex1];
					rH = curFrame.Hands[handIndex2];
					
				}else{
					lH = curFrame.Hands[handIndex2];
					rH = curFrame.Hands[handIndex1];
				}
			}
			#endregion
		}
		
		if (curFrame.Hands.Count>0){
			//Rotation of the object.
			//Quaternion
			Vector3 palmXNormal = lH.PalmNormal.ToUnity();
			//palmXNormal.z =0;
			//Quaternion lookRotation = Quaternion.FromToRotation(-Vector3.up, palmXNormal);

				//transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation,Time.deltaTime*speed);
			
			Vector3 rotateDir = Vector3.up;
			
			Vector3 moveDir = new Vector3(0,0,0);
			
			if (palmXNormal.x >0.2){
				//move left
				if(transform.position.x>-boundaryWidth/2){
					moveDir += new Vector3(-1,0,0);
					rotateDir += new Vector3(-0.5f, 1,0);
				}
			}
			if (palmXNormal.x <-0.2){
				//move right 
				if(transform.position.x<boundaryWidth/2){
					moveDir += new Vector3(1,0,0);
					rotateDir += new Vector3(0.5f,1,0);
				}
			}
			if (palmXNormal.z<-0.2 && playerStatsScript.move2D == true){
				if (transform.position.z< boundaryHeight/2){
					//Go forwards
					moveDir += new Vector3(0,0,1);
					rotateDir += new Vector3(0,1,0.5f);
				}
			}
			if (palmXNormal.z>0.2 && playerStatsScript.move2D ==true){
				if (transform.position.z>-boundaryHeight/2){
					//Go backwards
					moveDir+=new Vector3(0,0,-1);
					rotateDir+= new Vector3(0,1,-0.5f);
				}
			}
			
			///----------------------------------------
			//moveDir = (lH.Finger(0).TipPosition.ToUnity()-transform.position).normalized;

			//----------------------------------
			Quaternion spaceshipRotation = Quaternion.FromToRotation(Vector3.up, rotateDir);
			
			transform.rotation = spaceshipRotation;
			
			transform.Translate(moveDir*speed,Space.World);
				
		}
		
		//-----------------------------------------------------------------------------------------
		else{
			
			Vector3 rotateDir = Vector3.up;
			Vector3 moveDir = new Vector3(0,0,0);
			if (Input.GetKey(KeyCode.LeftArrow)){
				//move left
				if(transform.position.x>-boundaryWidth/2){
					moveDir += new Vector3(-1,0,0);
					rotateDir += new Vector3(-0.5f, 1,0);
				}
			}
			if (Input.GetKey(KeyCode.RightArrow)){
				//move right
				if(transform.position.x<boundaryWidth/2){
					moveDir += new Vector3(1,0,0);
					rotateDir += new Vector3(0.5f, 1,0);
				}
				
			}
			if (Input.GetKey(KeyCode.UpArrow) && playerStatsScript.move2D == true){
				if (transform.position.z< boundaryHeight/2){
				//Go forwards
					moveDir += new Vector3(0,0,1);
					rotateDir += new Vector3(0, 1,0.7f);
				}
			}
			if (Input.GetKey(KeyCode.DownArrow) && playerStatsScript.move2D == true){
				if (transform.position.z>-boundaryHeight/2){
				//Go backwards
					moveDir+=new Vector3(0,0,-1);
					rotateDir += new Vector3(0, 1,-0.7f);
				}
			}
			
			Quaternion spaceshipRotation = Quaternion.FromToRotation(Vector3.up, rotateDir);
			
			transform.rotation = spaceshipRotation;
		
			transform.Translate(moveDir*speed,Space.World);
		//-----------------------------------------------------------------------------
		}
	}
	
	void OnTriggerEnter(Collider c){
		if (c.transform.CompareTag("Enemy")){
			playerDead=true;
			
			Instantiate (explosion,transform.position,Quaternion.Euler(-90,0,0));
			
			gameObject.collider.enabled = false;
			
			foreach(Transform child in transform){
				child.gameObject.SetActive(false);
			}
		}
	}
}



