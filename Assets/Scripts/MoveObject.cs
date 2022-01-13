using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {
  public float raycastDistance = 4F;
  RaycastHit hit;
  bool pickup = false;
  Transform obj;
    void Update(){
        Camera camera = GameObject.Find("Player").transform.GetChild(0).GetComponent<Camera>();
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                
        if (Physics.Raycast(ray, out hit, raycastDistance)) {
            if (Input.GetButtonDown("Interact")) {
                if (hit.transform.tag == "Gem") {
                    pickup = true;
                    obj = hit.transform;
                    obj.GetComponent<Rigidbody>().useGravity = false;
                    return;
                }
            }
        }

        if(pickup){
            obj.position = Vector3.MoveTowards(obj.position, camera.transform.position + ray.direction * raycastDistance, Time.deltaTime * 10);
        }

        if (Input.GetButtonDown("Interact")) {
            if(pickup){
                pickup = false;
                obj.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}
