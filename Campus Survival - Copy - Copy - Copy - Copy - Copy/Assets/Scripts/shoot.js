#pragma strict
var active				: boolean = true;
var projectile			: Transform;
var projectileLeft		: Transform;
var projectileRight		: Transform;
var spawn				: Transform;
var gun 				: int = 1;
var touchingZombie		: boolean = false;
var playerNumber		: String = "dead1";
var touchedZombie 		: Collider;



var matPos				: Vector3;


function gun1 () {
	gun = 1;
}
function gun2 () {
	gun = 2;
}
function gun3 () {
	gun = 3;
}







function Start () {

}
function activate() {
	gameObject.active = true;
	}
function deactivate() {
	gameObject.active = false;
	}
	
function fire() {
	 
	Instantiate(projectile, transform.position, transform.rotation);
	//if (gun == 2) {
		//Instantiate(projectileLeft, transform.position, transform.rotation);
		//Instantiate(projectileRight, transform.position, transform.rotation);
	//}
}

function place() {
	if (touchingZombie) touchedZombie.SendMessage(playerNumber);
	else {
		matPos = transform.position;
		matPos.x += Random.Range(-1.0f,1.0f);
		matPos.y += Random.Range(-1.0f,1.0f);
		Instantiate(spawn, matPos, transform.rotation);
		matPos = transform.position;
		matPos.x += Random.Range(-1.0f,1.0f);
		matPos.y += Random.Range(-1.0f,1.0f);
		Instantiate(spawn, matPos, transform.rotation);
		matPos = transform.position;
		matPos.x += Random.Range(-1.0f,1.0f);
		matPos.y += Random.Range(-1.0f,1.0f);
		Instantiate(spawn, matPos, transform.rotation);
	}
}	
	
function OnTriggerStay (other: Collider) {	
	if (other.tag == "Zombie") {
		touchingZombie = true;
		touchedZombie = other;
		//other.SendMessage(playerNumber);
	}	
	

}
	
	
function Update () {
	//Create a bullet
	touchingZombie = false;
	
}