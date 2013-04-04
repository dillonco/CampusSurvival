using UnityEngine;
using System.Collections;

// Players Base Health Script

public class Health1C : MonoBehaviour {

	public int curhealth = 10;
	public int maxhealth = 10; 
	public bool attacked = false;
	public bool blinkon = true;
	public int counter = 0;
	
	void Start () {
	}
	
	void Update () {
		if (counter == 120) {
			counter = 0;
			attacked = false;
		}
		else if (counter >= 1) {
			counter++;
			if (counter%10 <= 3) {
				blinkon = false;	
			}
			else blinkon = true;
		}
	}
	
	void OnGUI(){
		if (attacked && blinkon) {
			GUI.Label(new Rect(Screen.width - 200, 59, 200, 20), "Base under attack!");
		}
	}
	
	void shot () {
		attacked = true;
		counter = 1;
		if (curhealth == 1) {
				Debug.LogError("P1 base destroyed");
				GameObject.Find("prf_Player1").SendMessage("NoBase");
				Destroy(gameObject);
			}	
		else curhealth = curhealth - 1;
		}
	
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Bullet2") {
			shot();
		}
	} 
	
}
