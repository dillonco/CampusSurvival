
using UnityEngine;
using System.Collections;

public class scr_HealthSystem2 : MonoBehaviour {

// Health variables
public int maxHealth = 100;
public int curHealth = 100;

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
		GameObject.Find("prf_Player2").SendMessage("Respawn");
		curHealth = 100;
		}
	if (curHealth > maxHealth)
		maxHealth = 1;

	healthBarLength = (Screen.width / 2 - 25) * (curHealth / (float)maxHealth);
	}
	
public void shot() {
		AdjustCurrentHealth(-10);	
	}
}