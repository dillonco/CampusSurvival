#pragma strict
var active				: boolean = true;
var projectile			: Transform;
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
}
	
	
	
function Update () {
	//Create a bullet
	
	
}