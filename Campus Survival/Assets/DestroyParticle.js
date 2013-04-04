#pragma strict

var counter 	: int = 0;

function Start () {

}

function Update () {
	counter++;
	if (counter == 150) Destroy(gameObject);
}