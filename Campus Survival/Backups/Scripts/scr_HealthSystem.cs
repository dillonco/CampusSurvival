using UnityEngine;
using System.Collections;

public class scr_HealthSystem : MonoBehaviour {

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
	AddjustCurrentHealth (0);
}
// Create a healthbar	
void OnGUI(){
	GUI.Box(new Rect(10, 10, healthBarLength, 20), curHealth + "/" + maxHealth);
}

// Method used to adjust health
public void AddjustCurrentHealth(int adj) {
	curHealth += adj;

	if(curHealth < 0)
		curHealth = 0;

	if (curHealth > maxHealth)
		maxHealth = 1;

	healthBarLength = (Screen.width / 2) * (curHealth / (float)maxHealth);
	}
}