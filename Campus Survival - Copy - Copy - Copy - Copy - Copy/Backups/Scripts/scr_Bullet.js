#pragma strict
var bulletSpeed : float = 15.0;	//Speed of a bullet
var mapLimit	: float = 10.0; // Destroy bullet at the end of the map
var sceneManager : GameObject;	// To get the score to showup on screen
function Start () {

}

function Update () {
	transform.Translate(0, bulletSpeed * Time.deltaTime, 0);
	
	if(transform.position.y >= mapLimit) {
		Destroy(gameObject);
		}
}

function OnTriggerEnter (other : Collider) {

	if(other.gameObject.tag == "Zombie")
	{
		//couldnt get score to pop up
		//sceneManager.transform.GetComponent("scrptManager").AddScore();
		Destroy(other.gameObject);
		}		
	
}