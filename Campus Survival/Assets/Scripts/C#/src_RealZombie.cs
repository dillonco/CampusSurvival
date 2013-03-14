using UnityEngine;
using System.Collections;

public class src_RealZombie : MonoBehaviour {
	
	public int zChaseRange = 50;			// Range a zombie will start chasing a player
	public float runSpeed = 3f;		// Zombie run speed
	public float walkSpeed = 2f;		// Zombie walk speed
	public string tag_target = "Player";// The tag of your target, because tags are cool
	public string tag_target1 = "Spawn1"; //The tag of the alternate targets
	public string tag_target2 = "Spawn2"; //The tag of the alternate targets
	public int attackDamage = 10;		// Amount of damage caused by a Zombie running into you
	public int attackThreshold = 1;		// Range which zombies can attack
	public int attackSpeed = 2;			// Attack every X seconds
	
	private Transform target;			// Who should the zombies target? Set in Start()
	private RaycastHit hit;				// Raycast used for Zombies sight
	private Vector3 randomDirection; 	// Random movement for bored Zombies
	private bool chase = false;			// Whether or not the zombie is chasing someone
	private Vector3 targetPosition;		// Current position of the target, Zombies have super smell, and your abercrombie cologne is pretty strong brah
	private Vector3 lastKnownPosition;	// Last position a zombie saw you at
	private float distance;				// Distance between zombie and target
	private Vector3 currentPosition;
		
	
	// Raycasting against walls
	public float speed;
    public float rotateSpeed;
    public int rotateSpeedModifier;

    private RaycastHit fHit;
    private RaycastHit rHit;
    private RaycastHit riHit;
    private RaycastHit lHit;
    private RaycastHit leHit;
	
	public int health = 3;
	
	public Transform KOZ; // knocked out zombie
	public Transform DZ1; // dead zombie killed by player 1
	public Transform DZ2; // dead zombie killed by player 2
	public Transform dz1;
	public Transform dz2;
	
	// distances to potential targets
	public float player1Distance;
	public float player2Distance;
	public float player1BaseDistance;
	public float player2BaseDistance;
	
	public float sec = 0;
	
	// for the stupid sort array;
	public int min = 501;
	public int temp;
	public int minpos;
	public AudioClip attack;
	public AudioClip zhit;
	
 	
	// replaces zombie with knocked out zombie when shot enough times
	void shot () {
		audio.PlayOneShot(zhit);
		if (health == 1) {
			Destroy(gameObject);
			var newpos = transform.position;
			newpos.z -= 0.5f; // places knocked out zombie on the ground, so live zombies can see over it
			Instantiate(KOZ, newpos, transform.rotation);
		}
		else health--;
	}
	
	// replaces zombie with dead zombie if hit with a droped material
	void dead1 () {
			Destroy(gameObject);
			dz1 = Instantiate(DZ1, transform.position, transform.rotation) as Transform;
			Physics.IgnoreCollision(dz1.collider, GameObject.Find("prf_Player1").collider);
	}
	void dead2 () {
			Destroy(gameObject);
			dz2 = Instantiate(DZ2, transform.position, transform.rotation) as Transform;
			Physics.IgnoreCollision(dz2.collider, GameObject.Find("prf_Player2").collider);
	}
	
	
	void Start () {
			// Starts generating random directions for patrolling zombies
			StartCoroutine(ZombiePatrol());
	}
	
	//Known bug: if player is standing still before zombie gets in range, then 
	// the zombie won't see them
	// To fix this, set kinematic to yes in rigidbody of the player
	// this also breaks the current player movement
	bool IsVisible(string targetName){ // checks to see if given object is visible and is a target
		targetPosition = GameObject.Find(targetName).transform.position;
		if (Physics.Linecast (transform.position, targetPosition, out hit)) {
				// You can see the object
				// the object is something we want to attack, aka not a wall.
			if (hit.collider.gameObject == GameObject.Find(targetName)) {
				//Debug.LogError("Player is seen");
				return true;
			}
		}
		//Debug.LogError("Player is invisible");
		return false;	
	}
	
	// Returns the distance between the zombie and a target name
	int getDistance(string name) {
		int theDistance;
		if(GameObject.Find(name) != null) {
			theDistance = (int)(GameObject.Find(name).transform.position - transform.position).sqrMagnitude;
			return theDistance;
		}
		else return 1000; //in case the player doesn't exist, just return a large range
	}
	
	// Dillon's Motherfucking Magical Sort Array
	int[,] sortArray(int[,] array) {
		//yes we need 2 for-loops
		for (int i=0; i < 3; i++) {
			//find the min
			for (int x=0; x < 3; x++) {
				if(min > array[x, 0]) {
					min = array[x, 0];	//Max value in array
					minpos = x;			//Position of max value
					temp = array[i, 0];
					// Sets the new values ordered properly
					array[i, 0] = min;
					array[minpos, 0] = temp;	
				}
			
			}
		}
		return array;
	}
	// Treat this as a while loop, as the game runs
	void Update () {
		
		//Creates the array 
		int[,] priority = new int[,] {{getDistance("prf_Player1"),1}, {getDistance("prf_Player2"),2}, 
			{getDistance("prf_Player3"),3}, {getDistance("prf_Player4)"),4}};
		//
		chase = false;
		//sort array
		sortArray(priority);
		// goes through to make sure they can see the player
		// if not, zombie will check the next closest person if he can see them
		for (int i=0; i < 4; i++) {
			if(GameObject.Find("prf_Player" + priority[i, 1]) != null &&
				zChaseRange > priority[i, 0] &&
				IsVisible("prf_Player" + priority[i, 1])) {
					//Debug.LogError("Player distance:" + priority[i, 0]);
					//Debug.LogError("Player" + priority[i, 1] + " is within chaserange");
					//Passed 3 checks
					target = GameObject.Find("prf_Player" + priority[i, 1]).transform;
					targetPosition = target.position; // Sets the constantly changing position
					distance = Vector3.Distance(transform.position, targetPosition);
					currentPosition = transform.position;	
					chase = true;
					break;
			}	
		}
		// magic
		AIFunctionality ();
		
		
		// This stops the zombie from going into the third dimension
		// They're a bunch of cheaters
		Vector3 pos = transform.position;
     	pos.z = 0;
    	transform.position = pos;
		
		//----------------------Don't mind this, Josh is playing around with sprites.
		
		//transform.rotation *= 
		//transform.eulerAngles *= new Vector3(1,-1,1);
		//if (transform.rotation.z != 270) {
			//transform.rotation = Quaternion.AngleAxis(270, Vector3.up);
			//transform.rotation = Quaternion.Euler (180, 0, 0);
		//}
		//print (transform.rotation);
		

	}
	
	// Method to generate random walking directions
	IEnumerator ZombiePatrol() {
		while (true) {
			randomDirection = new Vector3(Random.Range(-90.0f,90.0f),Random.Range(-90.0f,90.0f),0);
			yield return new WaitForSeconds(5);		// changes every 5 seconds
		}
	}
	
	// The heart of Zombie...
	void AIFunctionality () {
		if (chase == true) {
			MoveTowards (targetPosition);
			lastKnownPosition = targetPosition;  //Constantly setting this just incase you lose him
		}
		if (chase == false) { 
			// check to see if you have a last known position
			if (lastKnownPosition != Vector3.zero) {
				MoveTowards (lastKnownPosition);
				// Once it gets close to the last known position, and hasn't seen anything yet, zombie patrols again
				if ((lastKnownPosition - transform.position).sqrMagnitude < .5) lastKnownPosition = Vector3.zero;
			}
			// Haven't seen the player yet
			else {
				Patrol(randomDirection);
			}
		}
	}
	// Walks the Zombie towards a position
	// Tricky names, eh?
    void MoveTowards (Vector3 direction) {

		transform.LookAt(direction);
		transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
		//transform.Translate(Vector3.fix_this * runSpeed * Time.deltaTime);
		// Note by Josh, for Josh: this is where you left off
    }
	
	// Patrol movement for the Zombie
	void Patrol(Vector3 direction) {
		
		transform.LookAt(direction);
		transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
	
	}
	
	
	IEnumerator OnCollisionStay(Collision other) {
		// Once a Zombie hits a material, it stops, destroys it, and continues on
		if(other.gameObject.tag == "Material Placed" || other.gameObject.tag == "Material Placed2"){
			runSpeed = 0f;
			walkSpeed = 0f;
			yield return new WaitForSeconds(0.5f);
			other.gameObject.SendMessage("destroy");
			runSpeed = 3f;
			walkSpeed = 2f;
		}
		// Once you run into the Player, attack him!
		else if(other.gameObject.tag == "Player" || other.gameObject.tag == "Spawn1" || other.gameObject.tag == "Spawn2") {
			sec = sec + 0.01f;
			float waiting = (sec % 1);
			
			// First attack at .5 seconds of collision, all other attacks are 1 second apart
			if(waiting <= 0.01 || sec == .2){
        		//HealthSystem.AdjustCurrentHealth(-attackDamage);
				other.gameObject.SendMessage("shot");
				audio.PlayOneShot(attack);
			}
		}
		// prevents zombies from trying to walk through walls
		//else if(other.gameObject.tag == "Block" || other.gameObject.tag == "Zombie"){
		//	randomDirection = new Vector3(Random.Range(-20.0f,20.0f),Random.Range(-20.0f,20.0f),0);
		//}
	}
	
}

