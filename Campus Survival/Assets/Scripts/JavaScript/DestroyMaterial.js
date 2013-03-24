#pragma strict


function Start () {
	Physics.IgnoreCollision(transform.collider, GameObject.Find("prf_Player1").collider);
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