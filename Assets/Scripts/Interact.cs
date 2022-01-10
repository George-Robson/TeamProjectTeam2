using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {
    public float raycastDistance = 10F;
    RaycastHit hit;
    void Update() {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        
        if (Physics.Raycast(ray, out hit, raycastDistance)) {
            if (hit.collider.tag == "Interactable" && Input.GetButton("Jump")){
                Transform objectHit = hit.transform;
                objectHit.position = transform.position + ray.direction * raycastDistance; 
            //TODO - add something
            }
        }
    }
}
