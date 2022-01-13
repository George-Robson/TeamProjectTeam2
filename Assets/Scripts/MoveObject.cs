using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {
  public float raycastDistance = 4F;
  RaycastHit hit;
  [HideInInspector]
  public bool pickup = false;
  [HideInInspector]
  public Transform obj;
    void Update(){
        Camera camera = transform.GetChild(0).GetComponent<Camera>();
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                
        if (Physics.Raycast(ray, out hit, raycastDistance) && !pickup) {
            if (Input.GetButtonDown("Interact")) {
                if (hit.transform.tag == "Gem") {
                    pickup = true;
                    obj = hit.transform;
                }
            }
        } else {
            if (Input.GetButtonDown("Interact")) {
                pickup = false;
            }
        }

        if(pickup){
            obj.position = Vector3.MoveTowards(obj.position, camera.transform.position + ray.direction * raycastDistance, Time.deltaTime * 10);
        }
    }
}
