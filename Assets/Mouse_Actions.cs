using UnityEngine;
using System.Collections;

public class Mouse_Actions : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//Vector3 pos = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x,Input.mousePosition.y, 10) );
		//transform.position = pos;
		
		/*if(Input.GetMouseButtonDown(0)) { // Left rotation
			leftRotate();
		} else if(Input.GetMouseButtonDown(1)) { // Right rotation
			rightRotate();
		}*/
	}
	
	void leftRotate() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray from the camera to the mouse position
		RaycastHit hit; // Raycast Hit information
		if( Physics.Raycast(ray, out hit) ) { // If the raycast hits something
			if( hit.transform.gameObject.tag == "rightRotate" ) { // If the object is rotating right
				hit.transform.gameObject.GetComponent<Rotate>().zVelocity -= 10;
				if(hit.transform.gameObject.GetComponent<Rotate>().zVelocity <= 1) {
					hit.transform.gameObject.GetComponent<Rotate>().dropping = true;
				}
			} else { // If the object is rotating left
				if(hit.transform.gameObject.GetComponent<Rotate>().zVelocity != null) {
					hit.transform.gameObject.GetComponent<Rotate>().zVelocity += 10; // Speed up the object
				}
			}
		}
	}
	
	void rightRotate() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//ray.origin = new Vector3(ray.origin.x, ray.origin.y, 0.01f);
		RaycastHit hit;
		if( Physics.Raycast(ray, out hit) ) {
			if( hit.transform.gameObject.tag == "leftRotate" ) {
				hit.transform.gameObject.GetComponent<Rotate>().zVelocity -= 10;
				if(hit.transform.gameObject.GetComponent<Rotate>().zVelocity <= 1) {
					hit.transform.gameObject.GetComponent<Rotate>().dropping = true;
				}
			} else {
				if(hit.transform.gameObject.GetComponent<Rotate>().zVelocity != null) {
					hit.transform.gameObject.GetComponent<Rotate>().zVelocity += 10; // Speed up the object
				}
			}
		}
	}
}