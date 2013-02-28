#pragma strict
// Dillon: Jan 15
// Manager file to control game objectives and other things
// We can keep track of zombie/player kills
// Maybe set a game time to see how long people last

var gameTime : float = 60;
static var score : int = 0;

function Start () {

}

function Update () {
	print("Score: " + score);
}

function AddScore () {
	score += 1;
}