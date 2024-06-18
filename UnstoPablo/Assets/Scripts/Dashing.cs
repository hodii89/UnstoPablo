using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private playerMove pm;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    [Header("CameraEffects")]
    public PlayerCam cam;
    public float dashFov;

    [Header("Settings")]
    //public bool disableGravity = false;
    public bool resetVel = true;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.E;

    private AudioSource audioSource;

    // Zmienna do przechowywania klipu d�wi�kowego �mierci
    public AudioClip dashingSound;

    private void Start()
    {
        // Przypisz komponent AudioSource znajduj�cy si� na tym samym obiekcie
        audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();
        pm = GetComponent<playerMove>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey))
            Dash();

        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
    }

    private void Dash()
    {
        // Sprawd�, czy mamy komponent AudioSource na tym obiekcie
        if (audioSource != null && dashingSound != null)
        {
            // Odtw�rz d�wi�k �mierci przez audioSource
            audioSource.PlayOneShot(dashingSound);
        }

        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;

        pm.dashing = true;

        cam.DoFov(dashFov);

        Transform forwardT;

            forwardT = orientation; /// where you're looking
   


        Vector3 forceToApply = forwardT.forward * dashForce;
        
       // if (disableGravity)
          //  rb.useGravity = false;

        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private Vector3 delayedForceToApply;
    private void DelayedDashForce()
    {
        if (resetVel)
            rb.velocity = Vector3.zero;

        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    private void ResetDash()
    {
        pm.dashing = false;

       cam.DoFov(85f);

      //  if (disableGravity)
         //   rb.useGravity = true;
    }
}
