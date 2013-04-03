using UnityEngine;
using System.Collections;

public class OtherGuis : MonoBehaviour {
	public Texture black;
	
	void OnGUI () {
		GUI.Box(new Rect(Screen.width / 2 - 15, 0, 40, Screen.height), black);
	}
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
