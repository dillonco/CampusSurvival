using UnityEngine;
using System.Collections;

public class scr_Bullet : MonoBehaviour {
	public string plrNum;
	public string player;
	public float bulletSpeed = 30.0f;
	//public GameObject sceneManager;
	public Transform blood;
	
	//var mapLimit	: float = 10.0; // Destroy bullet at the end of the map
	
	
	
	
	void setPlr (Transform plr) {
		player = plr.name;
	}
	void setNum (string num) {
		plrNum = num;
	}
	
	
	void OnTriggerEnter (Collider other) {

	if(other.gameObject.tag == "Zombie" || other.gameObject.tag == "Car" || other.gameObject.tag == "BlueLight")
	{
		//couldnt get score to pop up
		//sceneManager.transform.GetComponent("scrptManager").AddScore();
		
		//Destroy(other.gameObject);
		if (other.gameObject.tag == "Car") {
			other.SendMessage("bullet");
		}
		else other.SendMessage("shot");
		Destroy(gameObject);
		}
	else if(other.gameObject.tag == "Block" || other.gameObject.name == "Material Placed" && other.gameObject.tag != "Material Placed" + plrNum || other.gameObject.tag == "Spawn2") {
//Debug.LogError(other.gameObject.name);
//Debug.LogError(other.gameObject.tag);
		Destroy(gameObject);	
	}	
	
	else if(other.gameObject.tag == "Player" && other.gameObject.name != player) {
		other.SendMessage("shot");
		Instantiate(blood, transform.position, transform.rotation);
		//GameObject.other.GetComponent("scr_HealthSystem").AddjustCurrentHealth(-10);
		Destroy(gameObject);	
	}
}

	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, bulletSpeed * Time.deltaTime, 0);
	}
}