#pragma strict

var timer		:int = 0;

function Start () {

}

function Update () {
	if (timer == 60) Destroy(gameObject);
	else timer++;

}