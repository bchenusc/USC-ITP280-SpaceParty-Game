using UnityEngine;
using System.Collections;

public class LightFlashing : MonoBehaviour {
	public bool flashing = true;
	public Texture2D offTexture;
	public Texture2D onTexture;
	float resetTimer = 0.5f;
	float timer = 0.5f;
	bool on = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (flashing) {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				timer = resetTimer;
				if (on) {
					transform.renderer.material.mainTexture = offTexture;
					on = false;
				} else {
					transform.renderer.material.mainTexture = onTexture;
					on = true;
				}
			}
		}
	}
}
