using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceInTrigger : MonoBehaviour {
    public float xSpeed = 1;
    public float ySpeed = 1;
    public float zSpeed = 1;
    public float offset = 1;
    bool spinning = false;
    Transform obj;
    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Gem"){
            print("u mom gae");
            other.transform.tag = "Untagged";
            GameObject.Find("Player").GetComponent<MoveObject>().pickup = false;
            obj = GameObject.Find("Player").GetComponent<MoveObject>().obj;
            if(obj){
                obj.transform.position = transform.position + Vector3.up * offset;
                spinning = true;
            }
        }
    }
    
    private void FixedUpdate() {
        if(spinning && obj){
            obj.Rotate(Vector3.right * (Time.fixedDeltaTime * xSpeed));
            obj.Rotate(Vector3.up * (Time.fixedDeltaTime * ySpeed));
            obj.Rotate(Vector3.forward * (Time.fixedDeltaTime * zSpeed));
        }
    }
}
