#pragma strict


var deadZombie1				: Transform;
var deadZombie2				: Transform;
var liveZombie				: Transform;
var counter 				: int = 0;
var DZ1						: Transform;
var DZ2						: Transform;

function Start () {

}

function Update () {
	if (counter == 150) {
	
		var newpos = transform.position;
		newpos.z += 0.5f;
		Destroy(gameObject);
		Instantiate(liveZombie, newpos, transform.rotation);
	} else counter++;

}


function dead1() {
	Destroy(gameObject);
	DZ1 = Instantiate(deadZombie1, transform.position, transform.rotation);
	Physics.IgnoreCollision(DZ1.collider, GameObject.Find("prf_Player1").collider);
}

function dead2() {
	Destroy(gameObject);
	DZ2 = Instantiate(deadZombie2, transform.position, transform.rotation);
	Physics.IgnoreCollision(DZ2.collider, GameObject.Find("prf_Player2").collider);
}