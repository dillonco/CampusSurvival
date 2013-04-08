using UnityEngine;
using System.Collections;

public class ControlsGUI2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI(){
		
		GUI.Label(new Rect(15, Screen.height - 70, 300, 200), "Movement: WASD\nShoot: Shift\nMaterial/Melee/Activate: Q\nSprint Toggle: Tab");
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
