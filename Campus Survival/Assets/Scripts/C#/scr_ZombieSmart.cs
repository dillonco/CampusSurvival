using UnityEngine;

using System.Collections;

 

public class scr_ZombieSmart : MonoBehaviour {

 

    public float speed;
    public float speedModifier = 1;
    public float rotateSpeed;
    public int rotateSpeedModifier;

 

    private RaycastHit fHit;
    private RaycastHit rHit;
    private RaycastHit riHit;
    private RaycastHit lHit;
    private RaycastHit leHit;


    private Transform student;

 
    void Awake () {
        student = transform;

    }

 

    void Update () {

        if(Physics.Raycast(student.position, student.forward, out fHit, 1000)){

            Vector3 rightDir = student.forward + student.right;

            Vector3 leftDir = student.forward - student.right;

            Physics.Raycast(student.position, rightDir, out rHit, 1000);

            Physics.Raycast(student.position, leftDir, out lHit, 1000);

            Physics.Raycast(student.position, student.right, out riHit, 1000);

            Physics.Raycast(student.position, -student.right, out leHit, 1000);

            rotateSpeed = rotateSpeedModifier/fHit.distance;

            speed = speedModifier*fHit.distance;

            if(((lHit.distance+leHit.distance)/2) > ((rHit.distance+riHit.distance)/2)){

                student.Rotate(Vector3.up, -rotateSpeed);

            }else if(((lHit.distance+leHit.distance)/2) < ((rHit.distance+riHit.distance)/2)){

                student.Rotate(Vector3.up, rotateSpeed);

            }else if((Mathf.Approximately(((rHit.distance+riHit.distance)/2), ((lHit.distance+leHit.distance)/2)))){

                if(Random.value > 0.5f){

                    student.Rotate(Vector3.up, -rotateSpeed);

                }else{

                    student.Rotate(Vector3.up, rotateSpeed);

                }

            }

            student.Translate(Vector3.forward * speed * Time.deltaTime);

        }

    }

}