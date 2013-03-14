
using UnityEngine;
using System.Collections;

public class scr_HealthSystem : MonoBehaviour {

// Health variables
public int maxHealth = 100;
public int curHealth = 100;
public Transform blood;

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
	GUI.Box(new Rect(Screen.width / 2 + 15, 10, healthBarLength, 20),"Health: " + curHealth + "/" + maxHealth);
}

// Method used to adjust health
public void AdjustCurrentHealth(int adj) {
	curHealth += adj;

	if(curHealth <= 0){
		GameObject.Find("prf_Player1").SendMessage("Respawn");
		curHealth = 0;
		}
	if (curHealth > maxHealth)
		curHealth = maxHealth;

	healthBarLength = (Screen.width / 2 - 25) * (curHealth / (float)maxHealth);
	}
	
public void shot() {
		Instantiate(blood, transform.position, transform.rotation);
		AdjustCurrentHealth(-10);	
	}
	
public void HealthRespawn() {
	curHealth = maxHealth;
	
	}	
}