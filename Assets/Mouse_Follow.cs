using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x,Input.mousePosition.y, 10) );
		transform.position = pos;
		Debug.Log (pos);
	}
}
