#pragma strict

var health	: int = 10; 


function Start () {

}

function Update () {

}

function OnTriggerEnter (other: Collider) {
	if (other.gameObject.tag == "Bullet2") {
		if (health == 1) {
			Debug.LogError("P1 base destroyed");
			GameObject.Find("prf_Player1").SendMessage("NoBase");
			Destroy(gameObject);
		}	
		else health = health - 1;
	}
}