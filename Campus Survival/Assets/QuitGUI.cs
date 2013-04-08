using UnityEngine;
using System.Collections;

public class QuitGUI : MonoBehaviour {
	
	public int counter = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (counter == 90) Application.Quit();
		
		if(Input.GetKey(KeyCode.Escape)) {
		counter++;	
		}
		else counter = 0;
	}
	
	void OnGUI(){
		
		GUI.Label(new Rect(Screen.width/2 - 80, 40, 300, 200), "Hold Esc for 3 seconds to quit");
		
	}
	
}
