#pragma strict


var deadZombie1				: Transform;
var deadZombie2				: Transform;
var liveZombie				: Transform;
var counter 				: int = 0;

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
		Instantiate(deadZombie1, transform.position, transform.rotation);
}

function dead2() {
		Destroy(gameObject);
		Instantiate(deadZombie2, transform.position, transform.rotation);
}