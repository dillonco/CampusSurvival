#pragma strict

var target : Transform;
var smooth = 5.0;



var Camupsocket 	: GameObject;
var Camdownsocket 	: GameObject;
var Camleftsocket 	: GameObject;
var Camrightsocket	: GameObject;




function left () {
	//transform.localPosition = Vector3(5,0,15);
	target.position = Camleftsocket.transform.position;
}

function right () {
	//transform.localPosition = Vector3(-5,0,15);
	target.position = Camrightsocket.transform.position;
}

function up () {
	//transform.localPosition = Vector3(0,5,15);
	target.position = Camupsocket.transform.position;
}

function down () {
	//transform.localPosition = Vector3(0,-5,15);
	target.position = Camdownsocket.transform.position;
}




function Start () {

}



function Update () {
    transform.position = Vector3.Lerp (
        transform.position, target.position,
        Time.deltaTime * smooth);
}