using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Camera Cam;
    public float MouseSensitivity = 5F;
    public float MinimumY = -60F;
    public float MaximumY = 60F;
    public float MoveSpeed = 0.2F;
    public float CameraBobbingAmount = 1;
    public float CameraBobbingSpeed = 5;
    
    float rotationY = 0F;
    float Timer = 0F;
    float DefaultPosY;
    Rigidbody Body;

    void Start(){
        Body = GetComponent<Rigidbody>();
        DefaultPosY = Cam.transform.position.y;
    }

    void Update() {
        Timer += Time.fixedDeltaTime * CameraBobbingSpeed;

        Body.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * MoveSpeed) + (transform.right * Input.GetAxis("Horizontal") * MoveSpeed));

        //Body Horizontal Rotation
        Body.MoveRotation(Body.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * MouseSensitivity, 0)));

        //Camera Head Bobbing
        if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")){
            Cam.transform.position = new Vector3(Cam.transform.position.x, DefaultPosY + Mathf.Sin(Timer) * CameraBobbingAmount, Cam.transform.position.z);
        } else {
            Timer = 0F;
            Cam.transform.position = new Vector3(Cam.transform.position.x, Mathf.Lerp(Cam.transform.position.y, DefaultPosY, Time.fixedDeltaTime), Cam.transform.position.z);
        }

        //Camera Vertical Rotation
        rotationY += Input.GetAxis("Mouse Y") * MouseSensitivity;
        rotationY = Mathf.Clamp(rotationY, MinimumY, MaximumY);
        Cam.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
    }
}
