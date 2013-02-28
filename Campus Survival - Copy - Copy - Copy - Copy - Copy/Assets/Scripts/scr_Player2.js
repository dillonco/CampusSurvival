//Player Script
// Dillon: Jan 15
var spawnPoint			: Transform;
var noBase				: boolean = false;

// Players speed
var speed		: float = 10.0;
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
var newPos				: Vector3;
var camPos				: Vector3;
var key 				: String = "up";
var direc				: String = "up";
var moving2				: boolean = false;


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


var spacevalue : float = 1;
var materialStash		: int = 0;




function left () {
	key = "left";
	moving2 = true;
	newPos = transform.position;
	newPos.x += speed * Time.deltaTime * spacevalue;
	transform.position = newPos;
	if (spacevalue == 1){
		direc = "left";
		upSocket.active = false;
		downSocket.active = false;
		leftSocket.active = true;
		rightSocket.active = false;
		renderer.material.mainTexture = leftTexture;
		GameObject.Find("Main Camera2").SendMessage("left");
	}
}
function right () {
	key = "right";
	moving2 = true;
	newPos = transform.position;
	newPos.x -= speed * Time.deltaTime * spacevalue;
	transform.position = newPos;
	if (spacevalue == 1){
		direc = "right";
		upSocket.active = false;
		downSocket.active = false;
		leftSocket.active = false;
		rightSocket.active = true;
		renderer.material.mainTexture = rightTexture;
		GameObject.Find("Main Camera2").SendMessage("right");
	}
}
function up () {
	key = "up";
	moving2 = true;
	newPos = transform.position;
	newPos.y += speed * Time.deltaTime * spacevalue;
	transform.position = newPos;
	if (spacevalue == 1){
		direc = "up";
		upSocket.active = true;
		downSocket.active = false;
		leftSocket.active = false;
		rightSocket.active = false;
		renderer.material.mainTexture = upTexture;
		GameObject.Find("Main Camera2").SendMessage("up");
	}
}
function down () {
	key = "down";
	moving2 = true;
	newPos = transform.position;
	newPos.y -= speed * Time.deltaTime * spacevalue;
	transform.position = newPos;
	if (spacevalue == 1){
		direc = "down";
		upSocket.active = false;
		downSocket.active = true;
		leftSocket.active = false;
		rightSocket.active = false;
		renderer.material.mainTexture = downTexture;
		GameObject.Find("Main Camera2").SendMessage("down");
	}
}

function Update () {

	
	//if (moving2) Debug.LogError("moving2");
	//else Debug.LogError("not moving2");

	
	//Players movement
	if(Input.GetKey (KeyCode.A)) {
			left();
		}	else if (Input.GetKey (KeyCode.D)) {
			right();	
		}	else if (Input.GetKey (KeyCode.W)) {
			up();	
		}	else if (Input.GetKey (KeyCode.S)) {
			down();	
		} else moving2 = false;
	//	Debug.LogError(key);
	
	// Setting the range of the map
	transform.position.x = Mathf.Clamp(transform.position.x, horizontalMin, horizontalMax);
	transform.position.y = Mathf.Clamp(transform.position.y, verticalMin, verticalMax);


	
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
//////////////NOTE: key has to be set to direction of movement for collision detection, but causes the wrong socket to activate///////////	
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////





	if (counter == firerate) counter = 0;
	else if (counter > 0) counter++;
	
	if(Input.GetKey(KeyCode.LeftShift)) {
		
		spacevalue = 0.5;
		
		if (counter == 0){
			
			if (direc == "up") GameObject.Find("SocketUp2").SendMessage("fire");
			else if (direc == "down") GameObject.Find("SocketDown2").SendMessage("fire");
			else if (direc == "left") GameObject.Find("SocketLeft2").SendMessage("fire");
			else if (direc == "right") GameObject.Find("SocketRight2").SendMessage("fire");		
			counter = 1;
		}
	}
	else if(Input.GetKey(KeyCode.Q) && materialStash > 0) {
		spacevalue = 0.5;
		if (counter == 0) {
			materialStash = materialStash - 1;
			if (direc == "up") GameObject.Find("SocketUp2").SendMessage("place");
			else if (direc == "down") GameObject.Find("SocketDown2").SendMessage("place");
			else if (direc == "left") GameObject.Find("SocketLeft2").SendMessage("place");
			else if (direc == "right") GameObject.Find("SocketRight2").SendMessage("place");
			counter = 1;
		}
	}
	else spacevalue = 1;

	
} // end update

// What happens when a zombie attacks you
function OnTriggerEnter (other: Collider) {
	if(other.gameObject.name == "SpawnZombie(Clone)") {
		// Epic splatter effects
		if (blood) {
			Instantiate(blood, transform.position, transform.rotation);
			}
	}
	else if(other.gameObject.tag == "Block" || other.gameObject.tag == "Material Placed" || (other.gameObject.tag == "Player" && moving2))
	{
	if (key == "right") transform.Translate(0.5,0,0);
	if (key == "left")	transform.Translate(-0.5,0,0);
	if (key == "up")	transform.Translate(0,-0.5,0);
	if (key == "down")	transform.Translate(0,0.5,0);
	}
	else if(other.gameObject.tag == "Material") {
		materialStash++;
	}
}
public function Respawn() {
	if (noBase){
		Destroy(gameObject);
		Debug.LogError("P2 loses!");
		Debug.Break();
	}
	transform.position = spawnPoint.position;
}
public function NoBase() {
	noBase = true;
}