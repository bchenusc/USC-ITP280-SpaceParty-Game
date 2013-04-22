using UnityEngine;
using System.Collections;

public class Explosion_Script : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
		GameObject.Find("GUIHANDLE").GetComponent<GUISCRIPTS>().explosionAudio.Play();
		Destroy (gameObject,0.6f);
	}
}
