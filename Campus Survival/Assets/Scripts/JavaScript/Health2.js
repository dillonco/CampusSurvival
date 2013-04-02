#pragma strict

var health	: int = 10; 


function Start () {

}

function Update () {

}

function shot () {
	if (health == 1) {
			Debug.LogError("P2 base destroyed");
			GameObject.Find("prf_Player2").SendMessage("NoBase");
			Destroy(gameObject);
		}	
		else health = health - 1;
}

function OnTriggerEnter (other: Collider) {
	if (other.gameObject.tag == "Bullet") {
		shot();
	}
}