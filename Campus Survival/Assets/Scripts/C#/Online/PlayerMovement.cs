using UnityEngine;
using System.Collections;

public class FPSWalker4 : MonoBehaviour
{
    public float speed = 2.0f;

    private Vector3 moveDirection = Vector3.zero;
	string key = "up";
	string direc = "up";
	bool moving	 = false;
	float spacevalue = 1;
	
	private Vector3 movement;
	private Vector3 currentPosition;
	


    void FixedUpdate()
    {
            if (!FPSChat4.usingChat)
            {
//            	if(Input.GetKey (KeyCode.LeftArrow)) {
//					left();
//				}	else if (Input.GetKey (KeyCode.RightArrow)) {
//					right();	
//				}	else if (Input.GetKey (KeyCode.UpArrow)) {
//					up();	
//				}	else if (Input.GetKey (KeyCode.DownArrow)) {
//					down();	
//				} else moving = false;
				//currentPosition = transform.position;    
			
			// We are grounded, so recalculate movedirection directly from axes
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;

              
            }
            else
            {
                moveDirection = Vector3.zero;
            }

        // Move the controller
       CharacterController controller = GetComponent<CharacterController>() as CharacterController;
       CollisionFlags flags = controller.Move(moveDirection);
       //grounded = (flags & CollisionFlags.CollidedBelow) != 0;
    }

	void left () {
	key = "left";
	moving = true;
		
	rigidbody.AddForce (250 * speed * spacevalue, 0,0);
	//transform.Translate(Vector3(0.3,0,0) * speed * Time.deltaTime);
	//movement = new Vector3(250 * speed * spacevalue, 0, 0);
	if (spacevalue == 1){
		direc = "left";
		GameObject.Find("Main Camera").SendMessage("left");
	} 
}
	void right () {
	key = "right";
	moving = true;
	rigidbody.AddForce (-250 * speed * spacevalue, 0, 0);
	//movement = new Vector3(-250 * speed * spacevalue, 0, 0);
	if (spacevalue == 1){
		direc = "right";
		GameObject.Find("Main Camera").SendMessage("right");
	} 
}
	void up () {
	key = "up";
	moving = true;
	rigidbody.AddForce (0, 250 * speed * spacevalue, 0);
	//movement = new Vector3(0, 250 * speed * spacevalue, 0);
	if (spacevalue == 1){
		direc = "up";
		GameObject.Find("Main Camera").SendMessage("up");
	} 
}
	void down () {
	key = "down";
	moving = true;
	rigidbody.AddForce (0, -250 * speed * spacevalue, 0);
	//movement = new Vector3(0, -250 * speed * spacevalue, 0);
	if (spacevalue == 1){
		direc = "down";
		GameObject.Find("Main Camera").SendMessage("down");
	} 
	}

}