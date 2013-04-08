//Player Script
// Dillon: Jan 15
var GameOverScreen		: Transform;
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

var shotByBullet : boolean;


var firerate			: int = 15;
var counter				: int = 0;


var spacevalue 			: float = 1;
var materialStash		: int = 100;

var gun					: int = 1;


var Shot 				: AudioClip;

var inZone				: boolean = true;

var gold 				: Transform;



function Start()  {
	SP = GameObject.Find("prf_Player4");
		Physics.IgnoreCollision(SP.collider, collider);
}


function levelUp () {
	Instantiate(gold, transform.position, transform.rotation);
	speed = speed + 2;
	firerate = firerate - 1;
	counter = 0;
	
}


function left () {
	key = "left";
	moving2 = true;
	//transform.Translate(Vector3(0.3,0,0) * speed * Time.deltaTime);
	rigidbody.AddForce (250 * speed * boost * spacevalue, 0, 0);
	if (spacevalue >= 1){
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
	//transform.Translate(Vector3(-0.3,0,0) * speed * Time.deltaTime);
	rigidbody.AddForce (-250 * speed * boost * spacevalue, 0, 0);
	if (spacevalue >= 1){
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
	//transform.Translate(Vector3(0,0.3,0) * speed * Time.deltaTime);
	rigidbody.AddForce (0, 250 * speed * boost * spacevalue, 0);
	if (spacevalue >= 1){
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
	//transform.Translate(Vector3(0,-0.3,0) * speed * Time.deltaTime);
	rigidbody.AddForce (0, -250 * speed * boost * spacevalue, 0);
	if (spacevalue >= 1){
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
	//transform.position.x = Mathf.Clamp(transform.position.x, horizontalMin, horizontalMax);
	//transform.position.y = Mathf.Clamp(transform.position.y, verticalMin, verticalMax);


	
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
//////////////NOTE: key has to be set to direction of movement for collision detection, but causes the wrong socket to activate///////////	
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////





	if (counter == firerate) counter = 0;
	else if (counter > 0) counter++;
	
	if (spacevalue == 0.5) spacevalue = 2;
	
	if(Input.GetKey(KeyCode.LeftShift)) {
		
		spacevalue = 0.5;
		
		if (counter == 0){
			
			if (direc == "up") GameObject.Find("SocketUp2").SendMessage("fire");
			else if (direc == "down") GameObject.Find("SocketDown2").SendMessage("fire");
			else if (direc == "left") GameObject.Find("SocketLeft2").SendMessage("fire");
			else if (direc == "right") GameObject.Find("SocketRight2").SendMessage("fire");			
			audio.PlayOneShot(Shot);
			counter = 1;
		}
	}
	else if(Input.GetKey(KeyCode.Q) && materialStash > 0 && counter == 0) {
		if (materialStash >= 100 && noBase == true && inZone == true){
			materialStash = materialStash - 100;
			var currentPos = transform.position;
			spawnX = currentPos.x;
			spawnY = currentPos.y;
			SP = Instantiate(spawnPoint, transform.position, transform.rotation);
			SP.name = "prf_Player4";
			Physics.IgnoreCollision(SP.collider, collider);
			noBase = false;
			counter = 1;
		}
		else {
			spacevalue = 0.5;
				materialStash = materialStash - 1;
				if (direc == "up") GameObject.Find("SocketUp2").SendMessage("place");
				else if (direc == "down") GameObject.Find("SocketDown2").SendMessage("place");
				else if (direc == "left") GameObject.Find("SocketLeft2").SendMessage("place");
				else if (direc == "right") GameObject.Find("SocketRight2").SendMessage("place");
				counter = 1;
			
		}
	}
	//else spacevalue = 1;

	else if (Input.GetKey(KeyCode.Tab) && counter == 0) {
		if (spacevalue == 1) {
			spacevalue = 2;
			counter = 1;
		}
		else if (spacevalue == 2) {
			spacevalue = 1;
			counter = 1;
		}
	}
	
	
} // end update

// What happens when a zombie attacks you
function OnTriggerEnter (other: Collider) {
	if(other.gameObject.tag == "Material") {
		materialStash++;
	}
	else if(other.gameObject.name == "Zone2") {
		inZone = true;
	}
}

function OnTriggerExit (other: Collider) {
	if(other.gameObject.name == "Zone2") {
		inZone = false;
	}
}
public function Respawn() {
	if (respawning == false) {
		if (shotByBullet) {
			GameObject.Find("levelingSystem").SendMessage("AdjustOne", 3000);
		}
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
			Destroy(GameObject.Find("prf_Player1"));
			Instantiate(GameOverScreen, transform.position, transform.rotation);
		}
		GameObject.Find("prf_Player2").SendMessage("HealthRespawn");
		//transform.position = spawnPoint.position;
		speed = 20.0;
		counter = firerate;
		respawning = false;
	}
	//transform.position = spawnPoint.position;
}
public function NoBase() {
	noBase = true;
}

public function OnGUI () {
	if (respawning) {
		GUI.Label(Rect(Screen.width/4,Screen.height/2,100,50),"YOU HAVE DIED");
	}
	
	if (noBase) {
		GUI.Label(Rect(10,44,100,50),"Materials: " + materialStash.ToString() + "\nNo Base");
	}
	else GUI.Label(Rect(10,44,100,50),"Materials: " + materialStash.ToString());
}

public function shotbybullet () {
Debug.LogError("shotbybullet");
	shotByBullet = true;
}