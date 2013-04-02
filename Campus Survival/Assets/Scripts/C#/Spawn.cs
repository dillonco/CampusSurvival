using UnityEngine;
using System.Collections;


public class Spawn : MonoBehaviour {

	public Transform spawn;
	public int counter = 1;
	public int max = 1200;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.isMasterClient) {
			if (counter == 50) {
				PhotonNetwork.Instantiate("SpawnZombie", transform.position, transform.rotation, 0);
				//counter = 1;
			}
			else counter++;
		}
	}
}
