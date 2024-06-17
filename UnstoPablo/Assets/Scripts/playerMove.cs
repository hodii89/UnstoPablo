using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float sprintSpeed;
    public float Stamina = 100;
    public float StamDrainRate;

    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsground;
    public bool grounded;

    public Transform orientation;

    Vector3 moveDirection;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsground);

        SpeedControl();
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0f;
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Stamina > 0f)
                Stamina -= Time.deltaTime * StamDrainRate;
            if (Stamina > 0f)
             Sprint();
            else
             MovePlayer();
           
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

   private void MovePlayer()
    {

        moveDirection = orientation.forward;
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void Sprint()
    {
        moveDirection = orientation.forward;
        rb.AddForce(moveDirection.normalized * sprintSpeed, ForceMode.Force);
    }
}
