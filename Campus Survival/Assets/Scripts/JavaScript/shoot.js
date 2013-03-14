#pragma strict
var active				: boolean = true;
var projectile			: Transform;
var projectileLeft		: Transform;
var projectileRight		: Transform;
var spawn				: Transform;
var gun 				: int = 1;
var touchingZombie		: boolean = false;
var playerNumber		: String;
var touchedZombie 		: Collider;
var counter				: int = 0;
var player 				: Transform;

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
	if (touchingZombie){
	touchedZombie.SendMessage(playerNumber);
	}
	else {
		matPos = transform.position;
		matPos.x += Random.Range(-1.0f,1.0f);
		matPos.y += Random.Range(-1.0f,1.0f);
		var mat1 = Instantiate(spawn, matPos, transform.rotation) as Transform;
		matPos = transform.position;
		matPos.x += Random.Range(-1.0f,1.0f);
		matPos.y += Random.Range(-1.0f,1.0f);
		var mat2 = Instantiate(spawn, matPos, transform.rotation) as Transform;
		matPos = transform.position;
		matPos.x += Random.Range(-1.0f,1.0f);
		matPos.y += Random.Range(-1.0f,1.0f);
		var mat3 = Instantiate(spawn, matPos, transform.rotation) as Transform;
		Physics.IgnoreCollision(mat1.collider, player.collider);
		Physics.IgnoreCollision(mat2.collider, player.collider);
		Physics.IgnoreCollision(mat3.collider, player.collider);
	
	
	}
}	

function OnTriggerStay (other: Collider) {	
	if (other.tag == "Zombie") {
		touchingZombie = true;
		touchedZombie = other;
		//other.SendMessage(playerNumber);
	}	
	

}

function OnTriggerExit (other: Collider) {
	touchingZombie = false;
	}
	
	
function Update () {
	//Create a bullet
	
	
}