#pragma strict

function Start () {

}

function Update () {

}

function destroy () {
	Destroy(gameObject);
}

function OnTriggerEnter (other: Collider) {
	if(other.gameObject.tag == "Bullet2") {
		Destroy(gameObject);
	}
}