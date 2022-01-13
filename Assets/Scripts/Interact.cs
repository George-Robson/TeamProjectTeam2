using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {
  public float raycastDistance = 4F;
  public float inspectDistance = 1F;
  public float pickupDamping = 2;
	public float alignment = 0.95F;

  [HideInInspector]
  public enum PickupState { Picked, Picking, Placing, Placed, Combining, Observing }
  [HideInInspector]
  public PickupState objectPickupState = PickupState.Placed;

  RaycastHit hit;
  Transform pickedObjectL, pickedObjectR, pickedObjectM;
  Quaternion originalObjectLR, originalObjectRR, originalObjectMR; //rotation
  Vector3 originalObjectLP, originalObjectRP, originalObjectMP; //position
  Vector3 originalObjectLS, originalObjectRS, originalObjectMS; //scale
  float sensitivity;
  float amount = 0.5F;
  Transform gameObj;
  bool showText = false;
  
  private void Start() {
    sensitivity = GetComponentInParent<PlayerMovement>().MouseSensitivity;
  }

  void Update() {
    Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
    Ray rayL = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 3 * 2, Screen.height / 2, 0));
    Ray rayR = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 3, Screen.height / 2, 0));

    if (objectPickupState != PickupState.Placed && objectPickupState != PickupState.Combining) {
      if (Input.GetButtonDown("Interact")) {
        objectPickupState = PickupState.Placing;
      }
    }
    if (Physics.Raycast(ray, out hit, raycastDistance)) {
      if (hit.transform.parent && hit.transform.parent.tag == "Interactible" && hit.transform.tag != "Gem") {
        gameObj = hit.transform.parent.transform;

        if (Input.GetButtonDown("Interact")) {
          if (objectPickupState == PickupState.Placed) {
            objectPickupState = PickupState.Picking;

            originalObjectLP = gameObj.transform.GetChild(0).transform.position;
            originalObjectLR = gameObj.transform.GetChild(0).transform.rotation;
            originalObjectLS = gameObj.transform.GetChild(0).transform.localScale;
            pickedObjectL = gameObj.transform.GetChild(0).transform;

            originalObjectRP = gameObj.transform.GetChild(1).transform.position;
            originalObjectRR = gameObj.transform.GetChild(1).transform.rotation;
            originalObjectRS = gameObj.transform.GetChild(1).transform.localScale;
            pickedObjectR = gameObj.transform.GetChild(1).transform;

            originalObjectMP = gameObj.transform.GetChild(2).transform.position;
            originalObjectMR = gameObj.transform.GetChild(2).transform.rotation;
            originalObjectMS = gameObj.transform.GetChild(2).transform.localScale;
            pickedObjectM = gameObj.transform.GetChild(2).transform;
            pickedObjectM.GetComponent<MeshCollider>().enabled = false;
          }
        }
      }
    }

    switch (objectPickupState) {
      case PickupState.Picking: {
        //Move in front of camera
        Vector3 targetLocation = transform.position + ray.direction * inspectDistance;
        Vector3 targetLocationL = transform.position + rayL.direction * inspectDistance;
        Vector3 targetLocationR = transform.position + rayR.direction * inspectDistance;
        
        pickedObjectL.position = Vector3.MoveTowards(pickedObjectL.position, targetLocationL, Time.deltaTime * pickupDamping);
        pickedObjectR.position = Vector3.MoveTowards(pickedObjectR.position, targetLocationR, Time.deltaTime * pickupDamping);
        pickedObjectM.position = Vector3.MoveTowards(pickedObjectM.position, targetLocation, Time.deltaTime * pickupDamping);

        //Rotate to face the camera
        pickedObjectL.rotation = Quaternion.Slerp(pickedObjectL.rotation, Quaternion.LookRotation(transform.position - pickedObjectL.transform.position), Time.deltaTime * pickupDamping);
        pickedObjectR.rotation = Quaternion.Slerp(pickedObjectR.rotation, Quaternion.LookRotation(transform.position - pickedObjectR.transform.position), Time.deltaTime * pickupDamping);

        //Scale
        // float ratio = inspectDistance / raycastDistance;
        float ratio = 1F;
        pickedObjectL.localScale = Vector3.MoveTowards(pickedObjectL.localScale, new Vector3(originalObjectLS.x * ratio, originalObjectLS.y * ratio, originalObjectLS.z * ratio), Time.deltaTime * pickupDamping * 25);
        pickedObjectR.localScale = Vector3.MoveTowards(pickedObjectR.localScale, new Vector3(originalObjectRS.x * ratio, originalObjectRS.y * ratio, originalObjectRS.z * ratio), Time.deltaTime * pickupDamping * 25);

        if (pickedObjectL.position == targetLocationL &&
            pickedObjectR.position == targetLocationR &&
            pickedObjectL.rotation == Quaternion.LookRotation(transform.position - pickedObjectL.transform.position) &&
            pickedObjectR.rotation == Quaternion.LookRotation(transform.position - pickedObjectR.transform.position)
            ) {
          objectPickupState = PickupState.Picked;
        }

        break;
      }
      case PickupState.Placing: {
          pickedObjectL.position = Vector3.MoveTowards(pickedObjectL.position, originalObjectLP, Time.deltaTime * pickupDamping);
          pickedObjectR.position = Vector3.MoveTowards(pickedObjectR.position, originalObjectRP, Time.deltaTime * pickupDamping);
          pickedObjectM.position = Vector3.MoveTowards(pickedObjectM.position, originalObjectMP, Time.deltaTime * pickupDamping);

          pickedObjectL.rotation = Quaternion.Slerp(pickedObjectL.rotation, originalObjectLR, Time.deltaTime * pickupDamping);
          pickedObjectR.rotation = Quaternion.Slerp(pickedObjectR.rotation, originalObjectRR, Time.deltaTime * pickupDamping);
          pickedObjectM.rotation = Quaternion.Slerp(pickedObjectM.rotation, originalObjectMR, Time.deltaTime * pickupDamping);

          pickedObjectL.localScale = Vector3.Lerp(pickedObjectL.localScale, originalObjectLS, Time.deltaTime * pickupDamping * 10);
          pickedObjectR.localScale = Vector3.Lerp(pickedObjectR.localScale, originalObjectRS, Time.deltaTime * pickupDamping * 10);

          if (pickedObjectL.rotation == originalObjectLR && pickedObjectL.position == originalObjectLP &&
              pickedObjectR.rotation == originalObjectRR && pickedObjectR.position == originalObjectRP &&
              pickedObjectM.localScale == originalObjectMS) {
            objectPickupState = PickupState.Placed;
            pickedObjectM.GetComponent<MeshCollider>().enabled = true;
          }

          break;
        }
      case PickupState.Picked: {
        if (Input.GetKey(KeyCode.A)) {
          float rotationX = Input.GetAxis("Mouse X") * sensitivity;
          float rotationY = Input.GetAxis("Mouse Y") * sensitivity;

          pickedObjectR.transform.Rotate(new Vector3(-rotationY, -rotationX, 0), Space.Self);
        } else if (Input.GetKey(KeyCode.D)) {
          float rotationX = Input.GetAxis("Mouse X") * sensitivity;
          float rotationY = Input.GetAxis("Mouse Y") * sensitivity;

          pickedObjectL.transform.Rotate(new Vector3(-rotationY, -rotationX, 0), Space.Self);
        }

        if (Vector3.Dot(pickedObjectL.transform.forward, pickedObjectR.transform.forward) > alignment &&
            Vector3.Dot(pickedObjectL.transform.right, transform.forward) > alignment
        ) {
					objectPickupState = PickupState.Combining;
				}

        break;
      }
			case PickupState.Combining: {
          Vector3 targetLocation = transform.position + ray.direction * inspectDistance;
          pickedObjectL.position = Vector3.MoveTowards(pickedObjectL.position, targetLocation, Time.deltaTime * pickupDamping);
          pickedObjectR.position = Vector3.MoveTowards(pickedObjectR.position, targetLocation, Time.deltaTime * pickupDamping);

          pickedObjectL.localScale = Vector3.MoveTowards(pickedObjectL.localScale, Vector3.zero, Time.deltaTime * pickupDamping * 50);
          pickedObjectR.localScale = Vector3.MoveTowards(pickedObjectR.localScale, Vector3.zero, Time.deltaTime * pickupDamping * 50);
          pickedObjectM.localScale = Vector3.MoveTowards(pickedObjectM.localScale, originalObjectLS, Time.deltaTime * pickupDamping * 50);

				if(pickedObjectL.localScale == Vector3.zero && pickedObjectR.localScale == Vector3.zero){
          originalObjectMS = originalObjectLS;
          originalObjectLS = Vector3.zero;
          originalObjectRS = Vector3.zero;
          objectPickupState = PickupState.Observing;
        }
				break;
			}
      case PickupState.Observing: {

        if (Input.GetKeyDown(KeyCode.R)) {
          //Toggle text with R
          showText = !showText;
        }

        if(showText){
          //Text being shown
        } else {
          //No text, but there is rotation
          float rotationX = Input.GetAxis("Mouse X") * sensitivity;
          float rotationY = Input.GetAxis("Mouse Y") * sensitivity;

          pickedObjectM.transform.Rotate(new Vector3(-rotationY, -rotationX, 0), Space.Self);
        
          if (Input.GetButtonDown("Interact")) {
            //If you interact, you place it down
            objectPickupState = PickupState.Placing;
            
            print(gameObj.GetComponent<Condition>().condition);
            GameManager gm = GameObject.Find("Game Manager").transform.GetComponent<GameManager>();
            gm.SetGameState(gameObj.GetComponent<Condition>().condition, true);
          }
        }
        
        break;
      }
      default: break;
    }
  }
}
