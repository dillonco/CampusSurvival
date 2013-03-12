using UnityEngine;
using System.Collections;

// Players Base Health Script

public class Health1C : MonoBehaviour {

	public int curhealth = 10;
	public int maxhealth = 10; 
	public float healthBarLength;
	public Transform player;
	
	void Start () {
		healthBarLength = Screen.width / 2.07f;
	}
	
	void Update () {
	}
	
	void OnGUI(){
		GUI.Box(new Rect(815, 40, healthBarLength, 20),"Base: " + curhealth + "/" + maxhealth);
	}
	
	void shot () {
		if (curhealth == 1) {
				Debug.LogError("P1 base destroyed");
				GameObject.Find("prf_Player1").SendMessage("NoBase");
				Destroy(gameObject);
			}	
		else curhealth = curhealth - 1;
		
		healthBarLength = (Screen.width / 2 - 25) * (curhealth / (float)maxhealth);
		}
	
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Bullet2") {
			shot();
		}
	} 
	
}
