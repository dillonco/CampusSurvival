var isQuitButton = false;

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
	else {
		// play the game
		// Numbers are stored in File -> Build Settings
		Application.LoadLevel(1);
	}
}