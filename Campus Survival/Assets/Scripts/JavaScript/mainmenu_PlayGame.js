var isQuitButton = false;
var isMultiplayerButton = false;
var isPlayButton = false;

var GUI_Lobby : Transform;

function OnMouseEnter() {
	//Change the colour of the text
	renderer.material.color = Color.red;
}
function OnMouseExit() {
	//Change the colour of the text back
	renderer.material.color = Color.white;
}

function OnMouseUp() {
	// checks if it is Quit or Play button
	if(isQuitButton) {
		// quit the game
		Application.Quit();
	}
	if(isPlayButton) {
		// play the game
		// Numbers are stored in File -> Build Settings
		Application.LoadLevel(1);
	}
	if(isMultiplayerButton) {
		// Get rid of current game object
		Destroy(GameObject.Find("MainMenu"));
		 Instantiate(GUI_Lobby,transform.position,Quaternion.identity);
		 }
}