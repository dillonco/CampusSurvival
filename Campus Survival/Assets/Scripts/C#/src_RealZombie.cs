using UnityEngine;
using System.Collections;

public class src_RealZombie : MonoBehaviour {
	
	public int ChaseRange = 15;			// Range a zombie will start chasing a player
	public float runSpeed = 3f;		// Zombie run speed
	public float walkSpeed = 2f;		// Zombie walk speed
	public string tag_target = "Player";		// The tag of your target, because tags are cool
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
	private float x = 0.0f;
	private float y = 0.0f;
	private bool blue = false;
	private bool red = false;
	public int health = 3;
	public Transform KOZ;
	public Transform DZ1;
	public Transform DZ2;

 	
	
	void shot () {
		if (health == 1) {
			Destroy(gameObject);
			var newpos = transform.position;
			newpos.z -= 0.5f;
			Instantiate(KOZ, newpos, transform.rotation);
		}
		else health--;
	}
	
	void dead1 () {
			Destroy(gameObject);
			Instantiate(DZ1, transform.position, transform.rotation);
	}
	void dead2 () {
			Destroy(gameObject);
			Instantiate(DZ2, transform.position, transform.rotation);
		
	}
	
	
	void Start () {
			// Sets target as Player
			target = GameObject.FindWithTag(tag_target).transform;
			// Starts generating random directions for patrolling zombies
			StartCoroutine(ZombiePatrol());
	}
	
	// Treat this as a while loop, as the game runs
	void FixedUpdate () {
		if (chase == false){
			x = 0.0f;
			y = 0.0f;
		}	
		
//		Debug.LogError("x");
//		Debug.LogError(x);
//		Debug.LogError("y");
//		Debug.LogError(y);
		
		float player1Distance = (GameObject.Find("prf_Player1").transform.position - transform.position).sqrMagnitude;
		float player2Distance = (GameObject.Find("prf_Player2").transform.position - transform.position).sqrMagnitude;
		// Sets target as Player
		if (player1Distance < player2Distance) 
			target = GameObject.Find("prf_Player1").transform;
		else
			target = GameObject.Find("prf_Player2").transform;
		
		targetPosition = target.position; // Sets the constantly changing position
		distance = Vector3.Distance(transform.position, targetPosition);
		currentPosition = transform.position;
		
		
		AIFunctionality ();
		
		
		
		
		
		
		chase = false;
		
		// Target is within chase range
		if (distance < ChaseRange) {
			// Shoot out a magical invisible laser beam (your eyes)
			if (Physics.Linecast (transform.position, targetPosition, out hit)) {
				// You can see the player!!
				if (hit.collider.tag == tag_target) {
					// Chase that Mothe@#$%er down!
					chase = true;
				}
			}
		}
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
		transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
	
	}
	
	
	
	// Once you run into the Player, attack him!
	
	
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
		} else if(other.gameObject.tag == "Block"){
			randomDirection = new Vector3(Random.Range(-20.0f,20.0f),Random.Range(-20.0f,20.0f),0);
			
		} else if(other.gameObject.tag == "Material Placed" || other.gameObject.tag == "Material Placed2") {
			other.SendMessage("destroy");
			randomDirection = new Vector3(Random.Range(-20.0f,20.0f),Random.Range(-20.0f,20.0f),0);
		}  
			
		
    }

	
}

