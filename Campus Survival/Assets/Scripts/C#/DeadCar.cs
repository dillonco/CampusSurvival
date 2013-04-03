using UnityEngine;
using System.Collections;

public class DeadCar : MonoBehaviour {
	public AudioClip bulletSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void bullet () {
		audio.PlayOneShot(bulletSound);
	}
	
}
