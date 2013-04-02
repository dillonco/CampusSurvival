using UnityEngine;
using System.Collections;

public class Playerscript : Photon.MonoBehaviour
{

    public PhotonPlayer owner;

    //Last input value, we're saving this to be able to save network messages/bandwidth.
    private float lastClientHInput = 0;
    private float lastClientVInput = 0;

    //The input values the server will execute on this object
    //Movement variables
	private float serverCurrentHInput = 0;
    private float serverCurrentVInput = 0;
	
	private Vector3 movement;
	private Vector3 serverMovement;
	private Vector3 currentPosition;
	
	string key = "up";
	string direc = "up";
	bool moving	 = false;
	float spacevalue = 1;
	
	// Players speed
	float speed = 20.0f;
	
	GameObject upSocket;
	GameObject leftSocket;
	GameObject rightSocket;
	GameObject downSocket;

	Texture2D upTexture;
	Texture2D leftTexture;
	Texture2D rightTexture;
	Texture2D downTexture;
	

    void Awake()
    {
        if (PhotonNetwork.isNonMasterClientInRoom)
        {
            // We are probably not the owner of this object: disable this script.
            // RPC's and OnPhotonSerializeView will STILL get trough!
            // The server ALWAYS run this script though
            enabled = false;	 // disable this script (this disables Update());	
        }
    }
	
    [RPC]
    void SetPlayer(PhotonPlayer player)
    {
        owner = player;
        if (player == PhotonNetwork.player)
        {
            //We can control this player: enable this script (this enables Update());
            enabled = true;
        }
    }

    void Update()
    {

        //Client code
        if (PhotonNetwork.player == owner)
        {
            //Only the client that owns this object executes this code
			if(Input.GetKey (KeyCode.LeftArrow)) {
				left();
			}	else if (Input.GetKey (KeyCode.RightArrow)) {
				right();	
			}	else if (Input.GetKey (KeyCode.UpArrow)) {
				up();	
			}	else if (Input.GetKey (KeyCode.DownArrow)) {
				down();	
			} else moving = false;
			currentPosition = transform.position;
            //Is our input different? Do we need to update the server?
            //if (lastClientHInput != HInput || lastClientVInput != VInput)
            //{
             //   lastClientHInput = HInput;
              //  lastClientVInput = VInput;
           
	        //Debug.LogError(movement);

                //SendMovementInput(HInput, VInput); //Use this (and line 62) for simple "prediction"
          	photonView.RPC("SendMovementInput", PhotonTargets.MasterClient, currentPosition);

         //   }
        }

        //MasterCLient movement code
        //To also enable this on the client itself, use: " if (PhotonNetwork.isMasterClient || PhotonNetwork.player==owner){  "
        if (PhotonNetwork.isMasterClient || PhotonNetwork.player==owner)
        {            
            //Actually move the player using his/her input
            //Vector3 moveDirection = new Vector3(serverCurrentHInput, 0, serverCurrentVInput);
            transform.position = serverMovement; //* Time.deltaTime
			//Players movement
			//Debug.LogError(serverMovement);

        }

        /*if (PhotonNetwork.isNonMasterClientInGame)
        {
            transform.position = Vector3.Lerp(transform.position, lastReceivedPosition, 0.75f); //"lerp" to the posReceive by 75%
        }*/

    }


    [RPC]
    void SendMovementInput(string Movement)
    {
        //Called on the server
        serverMovement = movement;
    }


    Vector3 lastReceivedPosition;

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //This is executed on the owner of the PhotonView
            //The owner sends it's position over the network
                  
            stream.SendNext(transform.position);//"Encode" it, and send it

        }
        else
        {
            //Executed on all non-owners
            //receive a position and set the object to it

            Vector3 lastReceivedPosition = (Vector3)stream.ReceiveNext();
            
            //We've just recieved the current servers position of this object in 'posReceive'.

            transform.position = lastReceivedPosition;
            //To reduce laggy movement a bit you could comment the line above and use position lerping below instead:	
            //It would be even better to save the last received server position and lerp to it in Update because it is executed more often than OnPhotonSerializeView

        }
    }
	void left () {
	key = "left";
	moving = true;
		
	rigidbody.AddForce (250 * speed * spacevalue, 0,0);
	//transform.Translate(Vector3(0.3,0,0) * speed * Time.deltaTime);
	movement = new Vector3(250 * speed * spacevalue, 0, 0);
	if (spacevalue == 1){
		direc = "left";
		upSocket.active = false;
		downSocket.active = false;
		leftSocket.active = true;
		rightSocket.active = false;
		renderer.material.mainTexture = leftTexture;
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
		upSocket.active = false;
		downSocket.active = false;
		leftSocket.active = false;
		rightSocket.active = true;
		renderer.material.mainTexture = rightTexture;
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
		upSocket.active = true;
		downSocket.active = false;
		leftSocket.active = false;
		rightSocket.active = false;
		renderer.material.mainTexture = upTexture;
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
		upSocket.active = false;
		downSocket.active = true;
		leftSocket.active = false;
		rightSocket.active = false;
		renderer.material.mainTexture = downTexture;
		GameObject.Find("Main Camera").SendMessage("down");
	} 
	}
}