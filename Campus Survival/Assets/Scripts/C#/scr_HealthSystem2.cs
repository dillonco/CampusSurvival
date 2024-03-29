
using UnityEngine;
using System.Collections;

public class scr_HealthSystem2 : MonoBehaviour {

// Health variables
public int maxHealth = 100;
public int curHealth = 100;
public Transform blood;
public bool shotByBullet;

public float healthBarLength;

	
// initialize
void Start () {
	healthBarLength = Screen.width / 2;
}

// Update the health every second
void Update () {
	AdjustCurrentHealth (0);
}
// Create a healthbar	
void OnGUI(){
	GUI.Box(new Rect(10, 10, healthBarLength, 20),"Health: " +  curHealth + "/" + maxHealth);
}

// Method used to adjust health
public void AdjustCurrentHealth(int adj) {
	curHealth += adj;

	if(curHealth <= 0){
		if (shotByBullet) {
			GameObject.Find("prf_Player2").SendMessage("shotbybullet");
		}
		GameObject.Find("prf_Player2").SendMessage("Respawn");
		curHealth = 0;
		}
	if (curHealth > maxHealth)
		curHealth = maxHealth;

	healthBarLength = (Screen.width / 2 - 25) * (curHealth / (float)maxHealth);
	}
	
public void shot1() {
	AdjustCurrentHealth(-10);
	shotByBullet = true;
}
	
public void shot() {
		Instantiate(blood, transform.position, transform.rotation);
		AdjustCurrentHealth(-10);
		shotByBullet = false;	
	}
	

public void HealthRespawn() {
	curHealth = maxHealth;
	}
}