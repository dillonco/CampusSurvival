using UnityEngine;
using System.Collections;


public class Spawn : MonoBehaviour {

	public Transform spawn;
	public int counter = 1;
	public int max = 30;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (counter == max) {
			Instantiate(spawn, transform.position, transform.rotation);
			counter = 1;
		}
		else counter++;
	}
}
