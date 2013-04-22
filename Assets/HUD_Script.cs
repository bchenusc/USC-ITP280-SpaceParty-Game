using UnityEngine;
using System.Collections;

public class HUD_Script : MonoBehaviour {
	public Texture zeroHUD;
	public Texture oneHUD;
	public Texture twoHUD;
	public Texture threeHUD;
	public int textureNumber = 0;
	public GameObject my_camera;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void changeTexture() {
		switch(textureNumber) {
		case 1:
			guiTexture.texture = oneHUD;
			break;
		case 2:
			guiTexture.texture = twoHUD;
			break;
		case 3:
			guiTexture.texture = threeHUD;
			my_camera.SendMessage("endGame");
			break;
		}
	}
}
