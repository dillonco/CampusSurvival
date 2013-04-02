using UnityEngine;
using System.Collections;

public class ZombieMovement : Photon.MonoBehaviour
{

    public PhotonPlayer owner;
	public src_RealZombie src_RealZombie;

    //Last input value, we're saving this to be able to save network messages/bandwidth.
    private Vector3 lastClientInput;

    //The input values the server will execute on this object
    private Vector3 serverCurrentInput;


    void Awake()
    {
        if (PhotonNetwork.isNonMasterClientInRoom)
        {
            // We are probably not the owner of this object: disable this script.
            enabled = false;	 // disable this script (this disables Update());	
        }
    }
    [RPC]
    void SetPlayer(PhotonPlayer player)
    {
        owner = player;
        if (PhotonNetwork.isMasterClient)
        {
            //This is the master client!
            enabled = true;
        }
    }

    void Update()
    {

        //Client code
        if (PhotonNetwork.player == owner)
        {
            //call the real zombie script
			Vector3 Input = src_RealZombie.getLocation();
			
            //Is our input different? Do we need to update the server?
            if (lastClientInput != Input)
            {
                lastClientInput = Input;
                
                //SendMovementInput(HInput, VInput); //Use this (and line 62) for simple "prediction"
                photonView.RPC("SendMovementInput", PhotonTargets.MasterClient, Input);
                
            }
        }

        //MasterCLient movement code
        //To also enable this on the client itself, use: " if (PhotonNetwork.isMasterClient || PhotonNetwork.player==owner){  "
        if (PhotonNetwork.isMasterClient)
        {            
            //Actually move the player using his/her input
            //Vector3 moveDirection = new Vector3(serverCurrentHInput, 0, serverCurrentVInput);
            //float speed = 5;
            transform.Translate(serverCurrentInput);
        }

        /*if (PhotonNetwork.isNonMasterClientInGame)
        {
            transform.position = Vector3.Lerp(transform.position, lastReceivedPosition, 0.75f); //"lerp" to the posReceive by 75%
        }*/

    }


    [RPC]
    void SendMovementInput(Vector3 Input)
    {
        //Called on the server
        serverCurrentInput = Input;
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
}