// Zombie Stuff
// Dillon Jan 15

var target : Transform; 	//the zombie's target
var moveSpeed = 3;	 		//move speed
var rotationSpeed = 3; 		//turning speed
var attackThreshold = .1; 	//Distance which a zombie will start attacking
var chaseThreshold = 15; 	//Distance which a zombie will start chasing
var giveUpThreshold = 30; 	//Distance which a zombie will give up
var attackRepeatTime = 1; 	// delay between attacks

private var chasing = false;
private var attackTime : int;

var myTransform : Transform; 

function Awake()
{
    myTransform = transform; //cache transform data for easy access/preformance 
    }


function Start()
{
     target = GameObject.FindWithTag("Player").transform; //target the player
    }

function Update () {

    // check distance to target every frame:
    var distance = (target.position - myTransform.position).magnitude;

    if (chasing) {

        //rotate to look at the player
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
        Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed*Time.deltaTime);

        //move towards the player
        myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;

        // give up if too far away from target:
        if (distance > giveUpThreshold) {
            chasing = false;
        }

        // Attack commands
        if (distance < attackThreshold && Time.time > attackRepeatTime) {
					// doesnt work
					//Player.GetComponent("SCRIPT Player").health -=10;
            }
        if (distance < attackThreshold) {
       		moveSpeed=0;	//stop when close enough
    	}
		if (distance > attackThreshold) {
       		moveSpeed=3;	//start again if they run away
    	}
            attackTime = Time.time+ attackRepeatTime;
        }

      else {
        // start chasing if target comes close enough
        if (distance < chaseThreshold) {
            chasing = true;
        }
    }

}

function OnTriggerEnter (other: Collider) {
	// If you shoot a zombie, they will follow you
	// to the ends of the earth
	if (other.gameObject.CompareTag("Bullet")){
		chaseThreshold=100000;
	}
	// Reduces player health by 10 when they collide. 
	if(other.gameObject.tag == "Player") {
		other.GetComponent("scr_HealthSystem").AddjustCurrentHealth(-10);
	}
}

