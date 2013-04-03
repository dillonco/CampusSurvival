#pragma strict

function Start () {

}

function Update () {

}

function OnMouseEnter() {
	//Change the colour of the text
	renderer.material.color = Color.red;
}
function OnMouseExit() {
	//Change the colour of the text back
	renderer.material.color = Color.white;
}


function OnMouseUp() {
	Application.LoadLevel(0);
}