using UnityEngine;
using System.Collections;

public class DestroyMaterial : MonoBehaviour {
	public string plrNum;

	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision(transform.collider, GameObject.Find("prf_Player" + plrNum).collider);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void destroy () {
		Destroy(gameObject);
	}
	
	void OnTriggerEnter (Collider other) {
		if(other.gameObject.name == "Bullet" && other.gameObject.tag != "Bullet" + plrNum) {
			Destroy(gameObject);
		}
	}
	
}