//Player Script
// Dillon: Jan 15
var spawnPoint			: Transform;
var spawnX				: float;
var spawnY				: float;
var noBase				: boolean = true;

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


var spacevalue			: float = 1;
var materialStash		: int = 100;

var gun					: int = 1;






function left () {
	key = "left";
	moving = true;
	rigidbody.AddForce (5000 * spacevalue, 0, 0);
	if (spacevalue == 1){
		direc = "left";
		upSocket.active = false;
		downSocket.active = false;
		leftSocket.active = true;
		rightSocket.active = false;
		renderer.material.mainTexture = leftTexture;
		GameObject.Find("Main Camera").SendMessage("left");
	} /*else {
		if (gun == 1) GameObject.Find("SocketLeft").SendMessage("gun1");
		else if (gun == 2) GameObject.Find("SocketLeft").SendMessage("gun2");
		else if (gun == 3) GameObject.Find("SocketLeft").SendMessage("gun3");
	}*/
}
function right () {
	key = "right";
	moving = true;
	rigidbody.AddForce (-5000 * spacevalue, 0, 0);
	if (spacevalue == 1){
		direc = "right";
		upSocket.active = false;
		downSocket.active = false;
		leftSocket.active = false;
		rightSocket.active = true;
		renderer.material.mainTexture = rightTexture;
		GameObject.Find("Main Camera").SendMessage("right");
	} /*else {
	if (gun == 1) GameObject.Find("SocketRight").SendMessage("gun1");
	else if (gun == 2) GameObject.Find("SocketRight").SendMessage("gun2");
	else if (gun == 3) GameObject.Find("SocketRight").SendMessage("gun3");
	}*/
}
function up () {
	key = "up";
	moving = true;
	rigidbody.AddForce (0, 5000 * spacevalue, 0);
	if (spacevalue == 1){
		direc = "up";
		upSocket.active = true;
		downSocket.active = false;
		leftSocket.active = false;
		rightSocket.active = false;
		renderer.material.mainTexture = upTexture;
		GameObject.Find("Main Camera").SendMessage("up");
	} /*else {
	if (gun == 1) GameObject.Find("SocketUp").SendMessage("gun1");
	else if (gun == 2) GameObject.Find("SocketUp").SendMessage("gun2");
	else if (gun == 3) GameObject.Find("SocketUp").SendMessage("gun3");
	}*/
}
function down () {
	key = "down";
	moving = true;
	rigidbody.AddForce (0, -5000 * spacevalue, 0);
	if (spacevalue == 1){
		direc = "down";
		upSocket.active = false;
		downSocket.active = true;
		leftSocket.active = false;
		rightSocket.active = false;
		renderer.material.mainTexture = downTexture;
		GameObject.Find("Main Camera").SendMessage("down");
	} /*else {
	if (gun == 1) GameObject.Find("SocketDown").SendMessage("gun1");
	else if (gun == 2) GameObject.Find("SocketDown").SendMessage("gun2");
	else if (gun == 3) GameObject.Find("SocketDown").SendMessage("gun3");
	}*/
}








function Update () {
	//if (moving) Debug.LogError("moving");
	//else Debug.LogError("not moving");
	
	//Players movement
	if(Input.GetKey (KeyCode.LeftArrow)) {
			left();
		}	else if (Input.GetKey (KeyCode.RightArrow)) {
			right();	
		}	else if (Input.GetKey (KeyCode.UpArrow)) {
			up();	
		}	else if (Input.GetKey (KeyCode.DownArrow)) {
			down();	
		} else moving = false;
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
	
	if(Input.GetKey("space")) {
		
		spacevalue = 0.5;
		
		if (counter == 0){
			
			if (direc == "up") GameObject.Find("SocketUp").SendMessage("fire");
			else if (direc == "down") GameObject.Find("SocketDown").SendMessage("fire");
			else if (direc == "left") GameObject.Find("SocketLeft").SendMessage("fire");
			else if (direc == "right") GameObject.Find("SocketRight").SendMessage("fire");		
			counter = 1;
		}
	}
	else if(Input.GetKey(KeyCode.RightShift) && materialStash > 0) {
		if (materialStash >= 100 && noBase == true){
		materialStash = materialStash - 100;
		var currentPos = transform.position;
		spawnX = currentPos.x;
		spawnY = currentPos.y;
		SP = Instantiate(spawnPoint, transform.position, transform.rotation);
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
	else spacevalue = 1;
	
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
	if(other.gameObject.tag == "Zombie") {
		// Epic splatter effects
		if (blood) {
			Instantiate(blood, transform.position, transform.rotation);
			}
	}/*
	else if(other.gameObject.tag == "Block" || other.gameObject.tag == "Material Placed2" || (other.gameObject.tag == "Player" && moving))
	{
	if (key == "right") transform.Translate(0.5,0,0);
	if (key == "left")	transform.Translate(-0.5,0,0);
	if (key == "up")	transform.Translate(0,-0.5,0);
	if (key == "down")	transform.Translate(0,0.5,0);
	}*/
	else if(other.gameObject.tag == "Material") {
		materialStash++;
	}
}

public function Respawn() {
	if (noBase){
		Destroy(gameObject);
		Debug.LogError("P1 loses!");
		Debug.Break();
	}
	materialStash = 0;
	var newPos = transform.position;
	newPos.x = spawnX;
	newPos.y = spawnY;
	transform.position = newPos;
	
	//transform.position = spawnPoint.position;
}

public function NoBase() {
	noBase = true;
}

public function OnGUI () {
	GUI.Label(Rect(815,60,100,50),"Materials: " + materialStash.ToString());
}