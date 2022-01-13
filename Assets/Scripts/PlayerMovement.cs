using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Camera Cam;
    public float MouseSensitivity = 5F;
    public float MinimumY = -60F;
    public float MaximumY = 60F;
    public float MoveSpeed = 0.2F;
    public float BobbingWalkingAmount = 1;
    public float BobbingWalkingSpeed = 1;
    public float BobbingStandingAmount = 1;
    public float BobbingStandingSpeed = 1;
    
    float rotationY = 0F;
    float Timer = 0F;
    float DefaultPosY;
    Rigidbody Body;

    void Start(){
        Body = GetComponent<Rigidbody>();
        DefaultPosY = Cam.transform.position.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate() {
        if(Cam.GetComponent<Interact>().objectPickupState == Interact.PickupState.Picked) return;
        if(Cam.GetComponent<Interact>().objectPickupState == Interact.PickupState.Picking ) return;
        if(Cam.GetComponent<Interact>().objectPickupState == Interact.PickupState.Combining ) return;
        if(Cam.GetComponent<Interact>().objectPickupState == Interact.PickupState.Observing ) return;

        Body.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * MoveSpeed) + (transform.right * Input.GetAxis("Horizontal") * MoveSpeed));
        
        //Camera Head Bobbing
        if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")){
            Timer += Time.fixedDeltaTime * BobbingWalkingSpeed;
            Cam.transform.position = new Vector3(Cam.transform.position.x, transform.position.y + 1.5f + Mathf.Sin(Timer) * BobbingWalkingAmount, Cam.transform.position.z);
        } else {
            Timer += Time.fixedDeltaTime * BobbingStandingSpeed;
            Cam.transform.position = new Vector3(Cam.transform.position.x, transform.position.y + 1.5f + Mathf.Sin(Timer) * BobbingStandingAmount, Cam.transform.position.z);
        }
    }

    void Update(){
        if(Cam.GetComponent<Interact>().objectPickupState == Interact.PickupState.Picked ) return;
        if(Cam.GetComponent<Interact>().objectPickupState == Interact.PickupState.Picking ) return;
        if(Cam.GetComponent<Interact>().objectPickupState == Interact.PickupState.Combining ) return;
        if(Cam.GetComponent<Interact>().objectPickupState == Interact.PickupState.Observing ) return;

        //Camera Vertical Rotation
        rotationY += Input.GetAxis("Mouse Y") * MouseSensitivity;
        rotationY = Mathf.Clamp(rotationY, MinimumY, MaximumY);
        Cam.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);

        //Body Horizontal Rotation
        Body.MoveRotation(Body.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * MouseSensitivity, 0)));
    }
}
