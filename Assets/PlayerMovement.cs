using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float groundDrag;
    public float walkSpeed;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Debug")]
    public float updateInterval = 2f;
    private float lastIntervalTime;
    private Vector3 lastPosition;

    public Transform orientation;
    Rigidbody rb;
    
    public bool IsDashing { get; set; }



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        ResetInterval();
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        MyInput();
        SpeedControl();
        HandleDrag();
        DebugSpeed();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        Vector3 moveDirection = orientation.forward * Input.GetAxisRaw("Vertical") + orientation.right * Input.GetAxisRaw("Horizontal");

        if (grounded)
            rb.AddForce(moveDirection.normalized * walkSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection.normalized * walkSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > walkSpeed)
            rb.velocity = new Vector3(flatVel.normalized.x * walkSpeed, rb.velocity.y, flatVel.normalized.z * walkSpeed);
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void HandleDrag()
    {
        rb.drag = grounded ? groundDrag : 0;
    }

    private void DebugSpeed()
    {
        if (Time.time - lastIntervalTime >= updateInterval)
        {
            float deltaTime = Time.time - lastIntervalTime;
            float speed = (transform.position - lastPosition).magnitude / deltaTime;
//            Debug.Log("Speed: " + speed);
            ResetInterval();
        }
    }

    private void ResetInterval()
    {
        lastIntervalTime = Time.time;
        lastPosition = transform.position;
    }
}
