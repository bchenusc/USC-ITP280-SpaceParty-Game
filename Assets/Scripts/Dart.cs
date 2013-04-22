using UnityEngine;
using System.Collections;
using Leap;

public class Dart : MonoBehaviour {
	public Transform particle_rocket;
	Transform rocketParticles;
	public Transform hitCube;
	Transform hitPoint;
	public Transform powerBar;
	Transform bar;
	Transform gamePlayController;
	public Transform particle_hit;
	Vector3 screenPos;
	private DART_STATE dartState;
	Vector3 moveDirection = Vector3.zero;
	const float MAXPOWER = 80f;
	float power = 0f;
	public GUIStyle powerBarStyle;
	bool increasing = true;
	Vector3 gravity = new Vector3(0, -10.0f, 0);
	
	// Leap
	Controller controller;
	Frame curFrame;
	Hand leftHand;
	Hand rightHand;
	int handCount;
	float gestureTimer = 0.5f;
	float resetTimer = 0.5f;
	bool canGesture;
	
	
	public enum DART_STATE : int {
		WAITING = 0,
		AIMING = 1,
		POWER = 2,
		CALC_FLIGHT_PATH = 3,
		FLYING = 4,
		HIT = 5
	};
	
    void Awake() {
		dartState = DART_STATE.WAITING;
		bar = Instantiate(powerBar) as Transform;
		hitPoint = Instantiate(hitCube) as Transform;
		hitPoint.renderer.enabled = false;
		bar.transform.localScale = new Vector3(0, bar.transform.localScale.y, bar.transform.localScale.z);
	}
	
	void Start() {
		transform.FindChild("dart 1").FindChild("dart").transform.renderer.enabled = false;
		transform.FindChild("dart 1").FindChild("booster").transform.renderer.enabled = false;
		gamePlayController = GameObject.Find("GameController").transform;
		transform.position = new Vector3(0, Camera.mainCamera.transform.position.y - 0.5f, Camera.mainCamera.transform.position.z + 4);
		controller = new Controller();
		controller.EnableGesture(Gesture.GestureType.TYPEKEYTAP);
		rocketParticles = Instantiate(particle_rocket, transform.position - new Vector3(0, 0, 1f), Quaternion.Euler(new Vector3(0, 180, 0))) as Transform;
		rocketParticles.parent = transform;
		rocketParticles.renderer.enabled = false;
	}
	
    void Update() {
		if (!canGesture) {
			gestureTimer -= Time.deltaTime;
			if (gestureTimer <= 0) {
				gestureTimer = resetTimer;
				canGesture = true;
			}
		}
		
		
		curFrame = controller.Frame();
		handCount = curFrame.Hands.Count;
		if (handCount == 0) {
			//Debug.Log("No Hands");
			leftHand = null;
			rightHand = null;
		} else if (handCount == 1) {
			//Debug.Log("One Hand");
			leftHand = curFrame.Hands[0];
			rightHand = null;
		} else if (handCount == 2) {
			//Debug.Log("Two Hands");
			if ((curFrame.Hands[0].PalmPosition.y - curFrame.Hands[1].PalmPosition.y) > 200) {
				leftHand = curFrame.Hands[1];
				rightHand = null;
				handCount = 1;
			} else if ((curFrame.Hands[0].PalmPosition.y - curFrame.Hands[1].PalmPosition.y) < -200) {
				leftHand = curFrame.Hands[0];
				rightHand = null;
				handCount = 1;
			} else {
				if (curFrame.Hands[0].PalmPosition.x < curFrame.Hands[1].PalmPosition.x) {
					leftHand = curFrame.Hands[0];
					rightHand = curFrame.Hands[1];
				} else {
					leftHand = curFrame.Hands[1];
					rightHand = curFrame.Hands[0];
				}
			}
		} else if (handCount >= 3) {
			//Debug.Log("Three or More Hands");
			float lowestY1 = 10000;
			int index1 = -1, index2 = -1;
			for (int i = 0; i < curFrame.Hands.Count; i++) {
				if (curFrame.Hands[i].PalmPosition.y < lowestY1) {
					index2 = index1;
					lowestY1 = curFrame.Hands[i].PalmPosition.y;
					index1 = i;
				} else {
					index2 = i;
				}
			}
			if (curFrame.Hands[index1].PalmPosition.x < curFrame.Hands[index2].PalmPosition.x) {
				leftHand = curFrame.Hands[index1];
				rightHand = curFrame.Hands[index2];
			} else {
				leftHand = curFrame.Hands[index2];
				rightHand = curFrame.Hands[index1];
			}
			
			handCount = 2;
		}
		//Debug.Log("Hands: " + handCount);
		
		
		
		switch (dartState) {
			case DART_STATE.AIMING: {
				transform.FindChild("dart 1").FindChild("dart").transform.renderer.enabled = true;
				transform.FindChild("dart 1").FindChild("booster").transform.renderer.enabled = true;
				
				hitPoint.renderer.enabled = true;
				Vector3 rayCast = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2f);
				Ray ray = new Ray(rayCast, transform.forward);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit)) {
					hitPoint.transform.position = new Vector3(hit.point.x, hit.point.y, -7.2f);
				}
			
				if (handCount == 1) {
					Vector3 finger = new Vector3(leftHand.Fingers[0].Direction.x, leftHand.Fingers[0].Direction.y, -leftHand.Fingers[0].Direction.z);
					transform.rotation = Quaternion.FromToRotation(Vector3.forward, finger);
				} else if (handCount >= 2) {
					Vector3 finger = new Vector3(leftHand.Fingers[0].Direction.x, leftHand.Fingers[0].Direction.y, -leftHand.Fingers[0].Direction.z);
					transform.rotation = Quaternion.FromToRotation(Vector3.forward, finger);
					if (curFrame.Gestures().Count > 0) {
						for (int i = 0; i < curFrame.Gestures().Count; i++) {
							for (int j = 0; j < curFrame.Gestures()[i].Hands.Count; j++) {
								if (curFrame.Gestures()[i].Hands[j].Equals(rightHand)) {
									if (canGesture) {
										canGesture = false;
										dartState++;
									}
									break;
								}
							}
						}
					}
				}
				
				break;
			}
			case DART_STATE.POWER: {	
				//Debug.Log(power);
				
				if (curFrame.Gestures().Count > 0) {
					for (int i = 0; i < curFrame.Gestures().Count; i++) {
						for (int j = 0; j < curFrame.Gestures()[i].Hands.Count; j++) {
							if (canGesture) {
								canGesture = false;
								dartState++;
								break;
							}	
						}
					}
				}
			
				
				if (power >= MAXPOWER) {
					increasing = false;
				} else if (power <= 0) {
					increasing = true;
				}
				if (increasing) {
					power += Time.deltaTime * 200;
				} else {
					power -= Time.deltaTime * 200;
				}
			
			
				bar.transform.localScale = new Vector3((power/MAXPOWER)*-27.99f, bar.transform.localScale.y, bar.transform.localScale.z);
			
				break;
			}
			case DART_STATE.CALC_FLIGHT_PATH: {
				moveDirection = transform.forward;
				moveDirection *= power;
				dartState++;
				Destroy(hitPoint.gameObject);
				rocketParticles.renderer.enabled = true;
				audio.Play();
				break;
			}
			case DART_STATE.FLYING: {
				moveDirection += gravity * Time.deltaTime;
				transform.Translate(moveDirection * Time.deltaTime, Space.World);
				transform.rotation = Quaternion.FromToRotation(Vector3.forward, moveDirection);
				break;
			}
			case DART_STATE.HIT: {
				break;
			}
			default: break;
		}
    }
	
	void OnCollisionEnter(Collision other) {
		if (dartState < DART_STATE.HIT) {
			dartState = DART_STATE.HIT;
			if (other.transform.CompareTag("Board")) {
				int score = 0;
				//Debug.Log("Nice shot!");
				float dist = Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(other.transform.position.x, other.transform.position.y, 0));
				//Debug.Log("Distance: " + dist);
				transform.position = new Vector3(transform.position.x, transform.position.y, -5.9f);
				if (dist <= 0.101576215f) {
					score = 100;
				} else if (dist <= 0.26667565f) {
					score = 50;
				} else if (dist <= 1.61090750f) {
					score = 15;
				} else if (dist <= 1.88879300f) {
					score = 35;
				} else if (dist <= 2.66064550f) {
					score = 15;
				} else if (dist <= 2.94844200f) {
					score = 25;
				} else if (dist <= 4.09927215f) {
					score = 5;
				}
				gamePlayController.GetComponent<GamePlayController>().increaseScore(score);
				gamePlayController.GetComponent<GamePlayController>().canSpawnDart(true);
				transformToEmpty();
			} else if (other.transform.CompareTag("Wall")) {
				transform.position = new Vector3(transform.position.x, transform.position.y, -5.8f);
				//Debug.Log("You hit the wall...");
				gamePlayController.GetComponent<GamePlayController>().canSpawnDart(true);
				transformToEmpty();
			} else if (other.transform.CompareTag("KillBox")) {
				gamePlayController.GetComponent<GamePlayController>().canSpawnDart(true);
				transformToEmpty();
				Destroy(gameObject);
				//Debug.Log("You completely missed...");
			}
		}
	}
	
	void transformToEmpty() {
		Destroy(rocketParticles.gameObject);
		Destroy (bar.gameObject);
		Destroy (rigidbody);	
		collider.enabled = false;
		Destroy (gameObject.GetComponent<DontGoThroughThings>());
		Instantiate(particle_hit, transform.position, Quaternion.identity);
		Destroy (this);
	}
	
	public DART_STATE getState() {
		return dartState;	
	}
	
	public void setState(DART_STATE newState) {
		dartState = newState;
	}
	
	public void setGravity(Vector3 newGrav) {
		gravity = newGrav;	
	}
	
	public void setPower(float newPower) {
		power = newPower;	
	}
}