#pragma strict

function Start () {
	Physics.IgnoreCollision(transform.collider, GameObject.Find("prf_Player2").collider);
}

function Update () {

}

function destroy () {
	Destroy(gameObject);
}

function OnTriggerEnter (other: Collider) {
	if(other.gameObject.tag == "Bullet") {
		Destroy(gameObject);
	}
}