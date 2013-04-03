#pragma strict
var bulletSpeed : float = 30.0;	//Speed of a bullet
var mapLimit	: float = 10.0; // Destroy bullet at the end of the map
var sceneManager : GameObject;	// To get the score to showup on screen
var blood				: Transform;
function Start () {

}

function Update () {
	transform.Translate(0, bulletSpeed * Time.deltaTime, 0);
	
	//if(transform.position.y >= mapLimit || transform.position.y <= -mapLimit || transform.position.x >= mapLimit || transform.position.x <= -mapLimit) {
	//	Destroy(gameObject);
	//	}
}

function OnTriggerEnter (other : Collider) {

//Debug.LogError(other);

	if(other.gameObject.tag == "Zombie" || other.gameObject.tag == "Car" || other.gameObject.tag == "BlueLight") {
		//couldnt get score to pop up
		//sceneManager.transform.GetComponent("scrptManager").AddScore();
		
		//Destroy(other.gameObject);
		
		if (other.gameObject.tag == "Car" || other.gameObject.tag == "BlueLight") {
			other.SendMessage("bullet");
		}
		else {
			other.SendMessage("shot");
		}
		Destroy(gameObject);
	}
	else if(other.gameObject.tag == "Block" || other.gameObject.tag == "Material Placed2" || other.gameObject.tag == "Spawn2")
	{
		Destroy(gameObject);	
	}	
	
	else if(other.gameObject.name == "prf_Player2") {
		other.SendMessage("shot");
		Instantiate(blood, transform.position, transform.rotation);
		//GameObject.other.GetComponent("scr_HealthSystem").AddjustCurrentHealth(-10);
		Destroy(gameObject);	
	}
}
