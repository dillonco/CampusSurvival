#pragma strict

public var curhealth		: int = 10;
public var maxhealth		: int = 10; 
public var healthBarLength	: float;

function Start () {

}

function Update () {

}

function OnGUI(){
	GUI.Box(Rect(815, 20, healthBarLength, 20), curhealth + "/" + maxhealth);
}

function shot () {
	if (curhealth == 1) {
			Debug.LogError("P1 base destroyed");
			GameObject.Find("prf_Player1").SendMessage("NoBase");
			Destroy(gameObject);
		}	
	else curhealth = curhealth - 1;
	
	healthBarLength = (Screen.width / 2 - 25) * (curhealth / maxhealth);
	}


function OnTriggerEnter (other: Collider) {
	if (other.gameObject.tag == "Bullet2") {
		shot();
	}
}