using UnityEngine;
using System.Collections;

public class Connect : Photon.MonoBehaviour
{

    /*
     * We want this script to automatically connect to Photon and to enter a room.
     * In Awake we connect to the Photon server(/cloud).
     * Via OnConnectedToPhoton(); we will either join an existing room (if any), otherwise create one. 
     */

    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings("1.0");
    }


    void OnGUI()
    {
        //Check connection state..
        if (PhotonNetwork.connectionState == ConnectionState.Disconnected)
        {
            //We are currently disconnected
            GUILayout.Label("Connection status: Disconnected");

            GUILayout.BeginVertical();
            if (GUILayout.Button("Connect"))
            {
                PhotonNetwork.ConnectUsingSettings("1.0");
            }
            GUILayout.EndVertical();
        }
        else
        {
            //We're connected!
            if (PhotonNetwork.connectionState == ConnectionState.Connected)
            {
                GUILayout.Label("Connection status: Connected");
                if (PhotonNetwork.room != null)
                {
                    GUILayout.Label("Room: " + PhotonNetwork.room.name);
                    GUILayout.Label("Players: " + PhotonNetwork.room.playerCount + "/" + PhotonNetwork.room.maxPlayers);

                }
                else
                {
                    GUILayout.Label("Not inside any room");
                }

                GUILayout.Label("Ping to server: " + PhotonNetwork.GetPing());
            }
            else
            {
                //Connecting...
                GUILayout.Label("Connection status: " + PhotonNetwork.connectionState);
            }
        }
    }

    private bool receivedRoomList = false;

    void OnConnectedToPhoton()
    {
        StartCoroutine(JoinOrCreateRoom());
    }

    void OnDisconnectedFromPhoton()
    {
        receivedRoomList = false;
    }

   



    
    /// <summary>
    /// Helper function to speed up our testing: 
    /// - after connecting to Photon, check for active rooms and join the first if possible
    /// - if no roomlist was found within 2 seconds: Create a room
    /// </summary>
    /// <returns></returns>
    IEnumerator JoinOrCreateRoom()
    {
        float timeOut = Time.time + 2;
        while (Time.time < timeOut && !receivedRoomList)
        {
            yield return 0;
        }
        //We still didn't join any room: create one
        if (PhotonNetwork.room == null){
            string roomName = "TestRoom"+Application.loadedLevelName;
            PhotonNetwork.CreateRoom(roomName, true, true, 4);
        }
    }

    
    /// <summary>
    /// This is called when we are connect to Photon in the lobby state, upon receiving a new roomlist.
    /// </summary>
    void OnReceivedRoomList()
    {
        string wantedRoomName = "TestRoom" + Application.loadedLevelName;
        foreach(Room room in PhotonNetwork.GetRoomList()){
            if (room.name == wantedRoomName)
            {
                PhotonNetwork.JoinRoom(room.name);
                break;
            }
        }
        receivedRoomList = true;
    }

    /// <summary>
    /// Not used in this script, just to show how list updates are handled.
    /// </summary>
    void OnReceivedRoomListUpdate()
    {
        Debug.Log("We received a room list update, total rooms now: " + PhotonNetwork.GetRoomList().Length);
    }
}
