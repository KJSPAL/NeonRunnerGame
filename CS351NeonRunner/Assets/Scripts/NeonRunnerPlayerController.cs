using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonRunnerPlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    [Header("Sprint")]
    public float sprintMultiplier = 1.5f;     // how much faster when sprinting

    [Header("Feel / Control")]
    public float maxSpeed = 8f;               // absolute cap
    public float groundAcceleration = 60f;    // snappy on ground
    public float airAcceleration = 20f;       // gentle in air to preserve momentum
    public float groundLinearDrag = 8f;       // stop quickly on ground
    public float airLinearDrag = 1f;          // keep momentum in air

    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;

    //Set these references in the inspector
    public AudioClip jumpSound;
    public AudioSource playerAudio;


    void Start()
    {
        //Set reference to player audio source
        playerAudio = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();
        if (groundCheck == null)
            Debug.LogError("groundCheck not assigned to the player controller");

    }

    void Update()
    {
        // Horizontal input
        horizontalInput = Input.GetAxis("Horizontal");

        // Jump (preserves current X velocity){
        if (Input.GetButtonDown("Jump") && isGrounded) { 
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            playerAudio.PlayOneShot(jumpSound, 0.5f);
        }
    }

    void FixedUpdate()
    {
        // Ground check first
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Drag swap: high on ground (stops quickly), low in air (preserves momentum)
        if (isGrounded)
        {
            rb.drag = groundLinearDrag;
        }
        else
        {
            rb.drag = airLinearDrag;
        }

        // Target speed (includes sprint)
        float speedCap = moveSpeed; //Cap is just normailmovespeed unless shift is held, then the cap is the movespeed times the multiplier.
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded) speedCap *= sprintMultiplier;

        //
        float targetX = horizontalInput * Mathf.Min(maxSpeed, speedCap);



        if (isGrounded)
        {//Movement when on the ground
            // Strong control on ground: move quickly toward target
            float newX = Mathf.MoveTowards(rb.velocity.x, targetX, groundAcceleration * Time.fixedDeltaTime);
            rb.velocity = new Vector2(newX, rb.velocity.y);
        }
        else
        {//Movement when in the air
            // Air control: weaker nudge toward target to preserve momentum
            bool sameDir = Mathf.Sign(rb.velocity.x) == Mathf.Sign(targetX);
            if (sameDir && Mathf.Abs(rb.velocity.x) > Mathf.Abs(targetX))
            {
                // Keep current speed to preserve momentum; don't steer down to targetX
                targetX = rb.velocity.x;
            }
            float newX = Mathf.MoveTowards(rb.velocity.x, targetX, airAcceleration * Time.fixedDeltaTime);
            rb.velocity = new Vector2(newX, rb.velocity.y);
        }

        // Face movement direction
        if (horizontalInput > 0f) transform.localScale = new Vector3(1f, 1f, 1f);
        else if (horizontalInput < 0f) transform.localScale = new Vector3(-1f, 1f, 1f);
    }

}
