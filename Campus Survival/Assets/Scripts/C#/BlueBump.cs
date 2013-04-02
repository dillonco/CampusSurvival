using UnityEngine;
using System.Collections;


public class BlueBump : MonoBehaviour {
	
	public GameObject parent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void bumped () {
		parent.SendMessage("blueSet");
	}	
	
	
}
