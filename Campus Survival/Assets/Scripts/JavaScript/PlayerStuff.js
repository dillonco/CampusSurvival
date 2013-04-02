//Player Script
// Dillon: Jan 15
var spawnPoint			: Transform;
var spawnX				: float;
var spawnY				: float;
var noBase				: boolean = true;
var materials 			: Transform;
var respawning			: boolean = false;
var boost				: int = 1;
// Players speed
var speed		: float = 20.0;
//Health 
var health				: int = 100; 
 //Map limits
var horizontalMin 		: int = -15;
var horizontalMax 		: int = 15;
var verticalMin   		: int = -15;
var verticalMax   		: int = 15;

// Declaring some stuff
var projectile			: Transform;
//var socketProjectile	: Transform;
var blood				: Transform;
var camPos				: Vector3;
var key 				: String = "up";
var direc				: String = "up";
var moving				: boolean = false;


var upSocket 	: GameObject;
var downSocket 	: GameObject;
var leftSocket 	: GameObject;
var rightSocket : GameObject;

var leftTexture : Texture2D;
var rightTexture : Texture2D;
var upTexture : Texture2D;
var downTexture : Texture2D;


var firerate			: int = 15;
var counter				: int = 0;

var Shot 				: AudioClip;


var spacevalue			: float = 1;
var materialStash		: int = 100;

var gun					: int = 1;
var inZone				: boolean = true;


function Update () {
	//if (moving) Debug.LogError("moving");
	//else Debug.LogError("not moving");
	
	
	// Setting the range of the map
	//transform.position.x = Mathf.Clamp(transform.position.x, horizontalMin, horizontalMax);
	//transform.position.y = Mathf.Clamp(transform.position.y, verticalMin, verticalMax);


	
	if (counter == firerate) counter = 0;
	else if (counter > 0) counter++;
	
	if (spacevalue == 0.5) spacevalue = 1;
	
	
	if(Input.GetKey("space")) {
		
		spacevalue = 0.5;
		
		if (counter == 0){
			
			if (direc == "up") GameObject.Find("SocketUp").SendMessage("fire");
			else if (direc == "down") GameObject.Find("SocketDown").SendMessage("fire");
			else if (direc == "left") GameObject.Find("SocketLeft").SendMessage("fire");
			else if (direc == "right") GameObject.Find("SocketRight").SendMessage("fire");		
			audio.PlayOneShot(Shot);
			counter = 1;
		}
	}
	else if(Input.GetKey(KeyCode.RightShift) && materialStash > 0) {
		if (materialStash >= 100 && noBase == true && inZone == true){
		materialStash = materialStash - 100;
		var currentPos = transform.position;
		spawnX = currentPos.x;
		spawnY = currentPos.y;
		SP = Instantiate(spawnPoint, transform.position, transform.rotation);
		SP.name = "prf_Player3";
		Physics.IgnoreCollision(SP.collider, collider);
		noBase = false;
		counter = 1;
		}
		else {
			spacevalue = 0.5;
			if (counter == 0) {
				materialStash = materialStash - 1;
				if (direc == "up") GameObject.Find("SocketUp").SendMessage("place");
				else if (direc == "down") GameObject.Find("SocketDown").SendMessage("place");
				else if (direc == "left") GameObject.Find("SocketLeft").SendMessage("place");
				else if (direc == "right") GameObject.Find("SocketRight").SendMessage("place");
				counter = 1;
		}
		
		}
	}
	//else spacevalue = 1;

	else if (Input.GetKey(KeyCode.RightControl) && spacevalue == 1 && counter == 0) {
		spacevalue = 2;
		counter = 1;
	}
	else if (Input.GetKey(KeyCode.RightControl) && spacevalue == 2 && counter == 0) {
		spacevalue = 1;
		counter = 1;
	}
	
	/*if (Input.GetKey(KeyCode.RightControl) && counter == 0) {
		if (gun == 3) {
		gun = 1;
		counter = 1;
		if (direc == "up") GameObject.Find("SocketUp").SendMessage("gun3");
		else if (direc == "down") GameObject.Find("SocketDown").SendMessage("gun3");
		else if (direc == "left") GameObject.Find("SocketLeft").SendMessage("gun3");
		else if (direc == "right") GameObject.Find("SocketRight").SendMessage("gun3");
		}
		else if (gun == 2) {
		gun = 3;
		counter = 1;
		if (direc == "up") GameObject.Find("SocketUp").SendMessage("gun2");
		else if (direc == "down") GameObject.Find("SocketDown").SendMessage("gun2");
		else if (direc == "left") GameObject.Find("SocketLeft").SendMessage("gun2");
		else if (direc == "right") GameObject.Find("SocketRight").SendMessage("gun2");
		}
		else if (gun == 1) {
		gun = 2;
		counter = 1;
		GameObject.Find("SocketUp").SendMessage("gun1");
		GameObject.Find("SocketDown").SendMessage("gun1");
		GameObject.Find("SocketLeft").SendMessage("gun1");
		GameObject.Find("SocketRight").SendMessage("gun1");
		}
	}*/

	
} // end update

// What happens when a zombie attacks you
function OnTriggerEnter (other: Collider) {
	if(other.gameObject.tag == "Material") {
		materialStash++;
	}
	else if(other.gameObject.name == "Zone1") {
		inZone = true;
	}
}

function OnTriggerExit (other: Collider) {
	if(other.gameObject.name == "Zone1") {
		inZone = false;
	}
}

public function Respawn() {
	if (respawning == false) {
		respawning = true;
		counter = firerate + 1;
		speed = 0.0;
		rigidbody.constraints = RigidbodyConstraints.FreezePosition| RigidbodyConstraints.FreezeRotation;
		yield WaitForSeconds(2);
		
		rigidbody.constraints = RigidbodyConstraints.FreezePositionZ| RigidbodyConstraints.FreezeRotation;
		for(i=0;i<(materialStash/5);i++){
			Instantiate(materials, transform.position, transform.rotation);
		}
		materialStash = 0;
		var newPos = transform.position;
		newPos.x = spawnX;
		newPos.y = spawnY;
		transform.position = newPos;
		if (noBase){
			Destroy(gameObject);
			Debug.LogError("P1 loses!");
			Debug.Break();
		}
		GameObject.Find("prf_Player1").SendMessage("HealthRespawn");
		//transform.position = spawnPoint.position;
		counter = firerate;
		speed = 20.0;
		respawning = false;
	}
	
	//transform.position = spawnPoint.position;
}

public function NoBase() {
	noBase = true;
}

public function OnGUI () {
	GUI.Label(Rect(Screen.width / 2 + 15,60,100,50),"Materials: " + materialStash.ToString());
}