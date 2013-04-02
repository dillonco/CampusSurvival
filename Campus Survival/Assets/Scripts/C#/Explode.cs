using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {
	
	public int health = 10;
	public GameObject[] zombies;
	public Transform brokenCar;
	public AudioClip bulletSound;
	
	public src_RealZombie zombieScript;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void bullet () {
		audio.PlayOneShot(bulletSound);
		shot();
	}
	
	void shot () {
		if (health == 1) {
			Vector3 myPos = transform.position;
			
			zombies = GameObject.FindGameObjectsWithTag("Zombie");
			
			foreach (GameObject zombie in zombies) {
				zombie.SendMessage("boom", myPos);
			}
			
			Destroy(gameObject);
			Instantiate(brokenCar, transform.position, transform.rotation);
			
			
		}
		else health--;	
	}
	
}
