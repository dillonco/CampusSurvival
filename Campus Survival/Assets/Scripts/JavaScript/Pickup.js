#pragma strict

function Start () {

}

function Update () {

}

function OnTriggerEnter (other: Collider) {
	if(other.gameObject.tag == "Socket") {
		Destroy(gameObject);
	}
}