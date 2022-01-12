using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {
  public float raycastDistance = 4F;
  public float inspectDistance = 1F;
  public float pickupDamping = 2;

  [HideInInspector]
  public enum PickupState { Picked, Picking, Placing, Placed }
  [HideInInspector]
  public PickupState objectPickupState = PickupState.Placed;

  RaycastHit hit;
  Transform pickedObjectL;
  Transform pickedObjectR;
  Vector3 originalObjectLP;
  Quaternion originalObjectLR;
  Vector3 originalObjectLS;
  Vector3 originalObjectRP;
  Quaternion originalObjectRR;
  Vector3 originalObjectRS;
  float sensitivity;

  private void Start() {
    sensitivity = GetComponentInParent<PlayerMovement>().MouseSensitivity;
  }

  void Update() {
    Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

    if (objectPickupState != PickupState.Placed) {
      if (Input.GetButtonDown("Fire1")) {
        objectPickupState = PickupState.Placing;
      }
    }
    if (Physics.Raycast(ray, out hit, raycastDistance)) {
      if (hit.transform.parent && hit.transform.parent.tag == "Interactible") {
        Transform gameObj = hit.transform.parent.transform;

        if (Input.GetButtonDown("Fire1")) {
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
          }
        }
      }
    }

    switch (objectPickupState) {
      case PickupState.Picking: {
          //Move in front of camera
          float amount = 0.5F;
          Vector3 targetLocation = transform.position + ray.direction * inspectDistance;
          pickedObjectL.position = Vector3.MoveTowards(pickedObjectL.position, targetLocation - pickedObjectL.right * amount, Time.deltaTime * pickupDamping);
          pickedObjectR.position = Vector3.MoveTowards(pickedObjectR.position, targetLocation + pickedObjectR.right * amount, Time.deltaTime * pickupDamping);

          //Rotate to face the camera
          pickedObjectL.rotation = Quaternion.Slerp(pickedObjectL.rotation, Quaternion.LookRotation(transform.position - pickedObjectL.transform.position), Time.deltaTime * pickupDamping);
          pickedObjectR.rotation = Quaternion.Slerp(pickedObjectR.rotation, Quaternion.LookRotation(transform.position - pickedObjectR.transform.position), Time.deltaTime * pickupDamping);

          //Scale
          float ratio = inspectDistance / raycastDistance;
          pickedObjectL.localScale = Vector3.MoveTowards(pickedObjectL.localScale, new Vector3(ratio, ratio, ratio), Time.deltaTime * pickupDamping);
          pickedObjectR.localScale = Vector3.MoveTowards(pickedObjectR.localScale, new Vector3(ratio, ratio, ratio), Time.deltaTime * pickupDamping);

          if (pickedObjectL.position == targetLocation - pickedObjectL.right * amount &&
              pickedObjectR.position == targetLocation + pickedObjectR.right * amount &&
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
          pickedObjectL.rotation = Quaternion.Slerp(pickedObjectL.rotation, originalObjectLR, Time.deltaTime * pickupDamping);
          pickedObjectR.rotation = Quaternion.Slerp(pickedObjectR.rotation, originalObjectRR, Time.deltaTime * pickupDamping);
          pickedObjectL.localScale = Vector3.Lerp(pickedObjectL.localScale, originalObjectLS, Time.deltaTime * pickupDamping);
          pickedObjectR.localScale = Vector3.Lerp(pickedObjectR.localScale, originalObjectRS, Time.deltaTime * pickupDamping);

          if (pickedObjectL.rotation == originalObjectLR && pickedObjectL.position == originalObjectLP &&
              pickedObjectR.rotation == originalObjectRR && pickedObjectR.position == originalObjectRP) {
            objectPickupState = PickupState.Placed;
          }

          break;
        }
      case PickupState.Picked: {
          if (Input.GetKey(KeyCode.A)) {
            float rotationX = Input.GetAxis("Mouse X") * sensitivity;
            float rotationY = Input.GetAxis("Mouse Y") * sensitivity;

            pickedObjectR.transform.Rotate(new Vector3(-rotationY, -rotationX, 0), Space.Self);
          }
          else if (Input.GetKey(KeyCode.D)) {
            float rotationX = Input.GetAxis("Mouse X") * sensitivity;
            float rotationY = Input.GetAxis("Mouse Y") * sensitivity;

            pickedObjectL.transform.Rotate(new Vector3(-rotationY, -rotationX, 0), Space.Self);
          }

          //TODO
          if (Vector3.Dot(pickedObjectL.transform.forward, pickedObjectR.transform.forward) > 0.9) {

					}

          break;
        }
      default: break;
    }
  }
}
