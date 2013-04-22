using UnityEngine;
using System.Collections;

public class Dart_Generator : MonoBehaviour {
	
	public GameObject asteroidObject1;
	public GameObject asteroidObject2;
	public GameObject asteroidObject3;
	public float generateInterval;
	private float generateTimer = 0;
	
	// Use this for initialization
	void Start () {
		generateInterval = Mathf.Abs (generateInterval); // Set interval to positive
	}
	
	// Update is called once per frame
	void Update () {
		if(generateTimer >= 0) { // If the generate timer is not triggered
			generateTimer -=  Time.deltaTime; // Count down
		} else { // If the generate timer is triggered
			generateInterval -= .01f;
			generateTimer = generateInterval; // Reset the timer
			int randomInt = Mathf.FloorToInt(Random.Range(0, 2.99f));
			if( randomInt == 0 ) {
				Instantiate(asteroidObject1); // Create a dart
			} else if( randomInt == 1 ) {
				Instantiate(asteroidObject2);
			} else if( randomInt == 2 ) {
				Instantiate(asteroidObject3);
			}
		}
	}
}
