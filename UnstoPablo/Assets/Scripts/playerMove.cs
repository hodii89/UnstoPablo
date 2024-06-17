using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float ogMoveSpeed;
    public float sprintSpeed;
    public float slowSpeed;
    public float Stamina = 100;
    public float StamDrainRate;
    public bool dashing;

    public float groundDrag;
    public float airDrag;
    public float dashDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsground;
    public bool grounded;

    public Transform orientation;

    Vector3 moveDirection;

    Rigidbody rb;

    private MonoBehaviour activationScript; // Reference to the script to enable/disable

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Find the script with the specified name in the GameObject's components
        activationScript = GetComponentInChildren<UniversalAttack>();

        if (activationScript != null)
        {
            // Set initial state based on the 'dashing' variable
            activationScript.enabled = dashing;
        }
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsground);

        SpeedControl();
        if (grounded)
            rb.drag = groundDrag;
        else if (dashing)
        {
            rb.drag = dashDrag;
            if (activationScript != null)
                activationScript.enabled = true; // Enable script when dashing
        }
        else
        {
            rb.drag = airDrag;
            if (activationScript != null)
                activationScript.enabled = false; // Disable script when not dashing
        }
    }
    private void FixedUpdate()
    {
        if(grounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (Stamina > 0f)
                    Stamina -= Time.deltaTime * StamDrainRate;
                if (Stamina > 0f)
                    Sprint();
                else
                    MovePlayer();

            }
            else if (Input.GetKey(KeyCode.S))
            {

                SlowDown();
            }
            else
            {


                if (Stamina < 100f)
                {
                    Stamina += Time.deltaTime * StamDrainRate;
                }

                MovePlayer();

            }
        }

    }

    private void MovePlayer()
    {
        moveSpeed = ogMoveSpeed;
        moveDirection = orientation.forward;
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void Sprint()
    {
        moveSpeed = sprintSpeed;
        moveDirection = orientation.forward;
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }
    private void SlowDown()
    {
        moveSpeed = slowSpeed;
        moveDirection = orientation.forward;
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }
}

