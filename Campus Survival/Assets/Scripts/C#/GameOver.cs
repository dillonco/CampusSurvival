using UnityEngine;
using System.Collections;

//Game Over Script, this will print the winner.
// Winner needs to be passed as a variable in replace of "Player 1"

// We can also add multiple boxes to print number of zombies killed 
// and elapsed time
// Dillon, March 11

public class GameOver : MonoBehaviour {
    void OnGUI() {
        GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-25, 100, 50), "Player 1");
    }
}
