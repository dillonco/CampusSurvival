using UnityEngine;
using System.Collections;

public class BlueLight : MonoBehaviour {
	public Transform activateObject;
	private int health = 10;
	public GameObject[] zombies;
	public AudioClip bulletSound;
	
	
	void activate () {
		if (GameObject.Find("prf_Player5") == null){
			Object BL = Instantiate(activateObject, transform.position, transform.rotation);
			BL.name = "prf_Player5"; 
		}
		else {
			Destroy(GameObject.Find("prf_Player5"));
			zombies = GameObject.FindGameObjectsWithTag("Zombie");
			foreach (GameObject zombie in zombies) {
				zombie.SendMessage("NoBlueLight");
			}
		}
		
		
	}
	
	void bullet () {
		audio.PlayOneShot(bulletSound);
		
	}
	
	void shot () {
		
		if (health == 1) {
						
			Destroy(gameObject);
			Destroy(GameObject.Find("prf_Player5"));
			zombies = GameObject.FindGameObjectsWithTag("Zombie");
			
			foreach (GameObject zombie in zombies) {
				zombie.SendMessage("NoBlueLight");
			}
		}
		else health--;	
	}
		
	
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
