using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;
    public float dashCooldown;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.E;

    private float dashCooldownTimer = 0;
    public bool isDashing = false;

    private bool canDashAgain=false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(dashKey) && dashCooldownTimer <= 0 && !isDashing)
            StartCoroutine(PerformDash());
    }

    private IEnumerator PerformDash()
    {
        isDashing = true;
        pm.enabled = false;  // Disable other movement scripts during dash
        pm.IsDashing = true;
        
        Vector3 dashDirection = orientation.forward * dashForce + orientation.up * dashUpwardForce;
        rb.AddForce(dashDirection, ForceMode.Impulse);

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        pm.IsDashing = false;
        pm.enabled = true;  // Re-enable other movement scripts
        dashCooldownTimer = dashCooldown;
        if (canDashAgain) dashCooldownTimer = 0;
        canDashAgain = false;
    }
    public void ResetDashCooldown()
    {
        canDashAgain = true;
    }
    
    

}