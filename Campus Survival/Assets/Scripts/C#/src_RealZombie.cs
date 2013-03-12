using UnityEngine;
using System.Collections;

public class src_RealZombie : MonoBehaviour {
	
	public int ChaseRange = 15;			// Range a zombie will start chasing a player
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
	
	// distances to potential targets
	public float player1Distance;
	public float player2Distance;
	public float player1BaseDistance;
	public float player2BaseDistance;
	
	// for the stupid sort array;
	public int max = 0;
	public int temp;
	public int maxpos;
 	
	// replaces zombie with knocked out zombie when shot enough times
	void shot () {
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
			Instantiate(DZ1, transform.position, transform.rotation);
	}
	void dead2 () {
			Destroy(gameObject);
			Instantiate(DZ2, transform.position, transform.rotation);
	}
	
	
	void Start () {
			// Starts generating random directions for patrolling zombies
			StartCoroutine(ZombiePatrol());
	}
	
	
	bool IsVisible(Vector3 targetPosition){ // checks to see if given object is visible and is a target
		if (Physics.Linecast (transform.position, targetPosition, out hit)) {
			// You can see the object
			if (hit.collider.tag == tag_target || hit.collider.tag == tag_target1 || hit.collider.tag == tag_target2) {
				// the object is a target
				return true;
			}
		}
		return false;	
	}
	
	int getDistance(string name) {
		if(GameObject.Find(name).transform.position != null) {
			distance = (GameObject.Find(name).transform.position - transform.position).sqrMagnitude;
			int intDistance = (int)distance;
			return intDistance;
		}
		else return 500;
	}
	
	// Dillon's Motherfucking Magical Sort Array
	int[,] sortArray(int[,] array) {
		//yes we need 2 for loops
		for (int i=0; i < array.Length; i++) {
			//find the max
			for (int x=0; i < array.Length; i++) {
				if(max < array[x, 0]) {
					max = array[x, 0];	//Max value in array
					maxpos = x;			//Position of max value
				}
			temp = array[i, 0];
			// Sets the new values ordered properly
			array[i, 0] = max;
			array[maxpos, 0] = temp;	
			}
		}
		return array;
	}
	// Treat this as a while loop, as the game runs
	void Update () {
		
		//Creates the array 
		int[,] priority = new int[,] {{getDistance("prf_Player1"),1}, {getDistance("prf_Player2"),2}}; 
		//{getDistance("Feb 28 P1Spawn(Clone)"),3}, {getDistance("Feb 28 P2Spawn(Clone)"),4}};
	
		//sort array
		sortArray(priority);
		// goes through to make sure they can see the player
		// if not, zombie will check the next closest person if he can see them
		for (int i=0; i < 2; i++) {
			if(IsVisible(GameObject.Find("prf_Player" + priority[i, 1]).transform.position)){
				target = GameObject.Find("prf_Player" + priority[i, 1]).transform;
				Debug.LogError("Target is prf_Player" + priority[i, 1]);
				chase = true;
			}
			else chase = false;
		}
		
		// actually sets the target and correct position
		if (target != null) {
			targetPosition = target.position; // Sets the constantly changing position
			distance = Vector3.Distance(transform.position, targetPosition);
			currentPosition = transform.position;
		}
		
		// magic
		AIFunctionality ();
		
		
		// This stops the zombie from going into the third dimension
		// They're a bunch of cheaters
		Vector3 pos = transform.position;
     	pos.z = 0;
    	transform.position = pos;
		

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
    }
	
	// Patrol movement for the Zombie
	void Patrol(Vector3 direction) {
		
		transform.LookAt(direction);
		transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
	
	}
	
	void OnTriggerStay(Collider other) {
		
		scr_HealthSystem HealthSystem;
        HealthSystem = other.GetComponent("scr_HealthSystem2") as scr_HealthSystem;
		// Obviously never attack fellow zombies or walls
		if(other.gameObject.tag == "Player") {
			float sec = Time.time;
			float waiting = (sec % 1);
			
			// First attack at .5 seconds of collision, all other attacks are 1 second apart
			if(waiting == 0 || sec == .2){
        		//HealthSystem.AdjustCurrentHealth(-attackDamage);
				other.SendMessage("shot");
			}
			
		} else if(other.gameObject.tag == "Material Placed" || other.gameObject.tag == "Material Placed2") {
			other.SendMessage("destroy");
			randomDirection = new Vector3(Random.Range(-20.0f,20.0f),Random.Range(-20.0f,20.0f),0);
		}  
			
		
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
			float sec = Time.time;
			float waiting = (sec % 1);
			
			// First attack at .5 seconds of collision, all other attacks are 1 second apart
			if(waiting == 0 || sec == .2){
        		//HealthSystem.AdjustCurrentHealth(-attackDamage);
				other.gameObject.SendMessage("shot");
			}
		}
		// prevents zombies from trying to walk through walls
		else if(other.gameObject.tag == "Block" || other.gameObject.tag == "Zombie"){
			randomDirection = new Vector3(Random.Range(-20.0f,20.0f),Random.Range(-20.0f,20.0f),0);
		}
	}
	
}

