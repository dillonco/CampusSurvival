using UnityEngine;
using System.Collections;

public class Health2C : MonoBehaviour {

	public int curhealth = 10;
	public int maxhealth = 10; 
	public float healthBarLength;
	
	void Start () {
	healthBarLength = Screen.width / 2.07f;
		
	
	}
	
	void Update () {
	
	}
	
	void OnGUI(){
		GUI.Box(new Rect(10, 40, healthBarLength, 20),"Base: " + curhealth + "/" + maxhealth);
	}
	
	void shot () {
		if (curhealth == 1) {
				Debug.LogError("P2 base destroyed");
				GameObject.Find("prf_Player2").SendMessage("NoBase");
				Destroy(gameObject);
			}	
		else curhealth = curhealth - 1;
		
		healthBarLength = (Screen.width / 2 - 25) * (curhealth / (float)maxhealth);
		}
	
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Bullet1") {
			shot();
		}
	} 
	
}
