using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;
    //    public float multiplier;

    public Transform orientation;
    //    public Transform camHolder;

    float xRotation;
    float yRotation;

    //  [Header("Fov")]
    //   public bool useFluentFov;
    //   public PlayerMovement pm;
    //    public Rigidbody rb;
    //public Camera cam;
    //public float minMovementSpeed;
    //public float maxMovementSpeed;
    //public float minFov;
    //public float maxFov;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
        
        

