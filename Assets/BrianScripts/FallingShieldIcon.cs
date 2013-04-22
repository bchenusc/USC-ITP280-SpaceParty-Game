using UnityEngine;
using System.Collections;

public class FallingShieldIcon : MonoBehaviour {

	Transform player;
	public float speed=1;
	Player_stats player_stats;
	public Transform shield;
	GUISCRIPTS guiscript;
	
	void Start(){
		guiscript = GameObject.Find("GUIHANDLE").GetComponent<GUISCRIPTS>();
	}
	
	void OnTriggerEnter(Collider c){		
		if (c.transform.CompareTag("DestroyEnemy")){
			Destroy(gameObject);	
		}
		if (c.transform.CompareTag("Player")){
			Transform clone = Instantiate(shield, c.transform.position, Quaternion.Euler(new Vector3(90,0,0))) as Transform;
			clone.GetComponent<Shield_script>().target = c.transform;
			guiscript.powerupAudio.Play();
			Destroy (gameObject);
		}
	}
	
	void Update(){
			transform.Translate(-Vector3.forward*75*Time.deltaTime*speed);
			transform.Rotate(new Vector3(0,0,1)*Time.deltaTime*100);
	}
}
