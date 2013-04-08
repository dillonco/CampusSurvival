using UnityEngine;
using System.Collections;

public class ControlsGui1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI(){
		
		GUI.Label(new Rect(Screen.width - 200, Screen.height - 70, 300, 200), "Movement: Arrows\nShoot: Slash\nMaterial/Melee/Activate: Period\nSlow Toggle: Comma");
		
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
