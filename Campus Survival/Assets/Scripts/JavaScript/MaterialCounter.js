#pragma strict

var counter			: int = 0;
var spawn			: Transform;

function Start () {

}

function Update () {

}



function OnTriggerEnter (other: Collider) {
	if(other.gameObject.tag == "Bullet1" || other.gameObject.tag == "Bullet2") {
		if (counter == 0) {
			Instantiate(spawn, other.transform.position, other.transform.rotation);
			counter = 0;
		}
		else counter++;
	} else if (other.gameObject.name == "Red Bumper" || other.gameObject.name == "Blue Bumper") other.SendMessage("bumped");
}
