using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonRunnerPlayerController : MonoBehaviour
{
    [Header("Input (per player)")]
    public KeyCode leftKey = KeyCode.A;           // P1: A,   P2: LeftArrow
    public KeyCode rightKey = KeyCode.D;           // P1: D,   P2: RightArrow
    public KeyCode jumpKey = KeyCode.W;           // P1: W or Space, P2: UpArrow
    public KeyCode sprintKey = KeyCode.LeftShift;   // P1: LeftShift,  P2: RightShift or RightControl

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    [Header("Sprint")]
    public float sprintMultiplier = 1.5f;     // how much faster when sprinting (ground only)

    [Header("Feel / Control")]
    public float maxSpeed = 8f;               // absolute cap
    public float groundAcceleration = 60f;    // snappy on ground
    public float airAcceleration = 20f;       // gentle in air to preserve momentum
    public float groundLinearDrag = 8f;       // stop quickly on ground
    public float airLinearDrag = 1f;          // keep momentum in air

    // Audio
    public AudioClip jumpSound;
    public AudioSource playerAudio;

    // Internals
    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput; // -1, 0, +1

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!playerAudio) playerAudio = GetComponent<AudioSource>();
        if (!groundCheck) Debug.LogError("groundCheck not assigned to the player controller on " + name);
        if (!rb) Debug.LogError("Rigidbody2D missing on " + name);
    }

    void Update()
    {
        // --- Ground check first for responsive jump ---
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // --- Read per-player keys and build horizontal input (-1, 0, +1) ---
        int dir = 0;
        if (Input.GetKey(leftKey)) dir -= 1;
        if (Input.GetKey(rightKey)) dir += 1;
        horizontalInput = dir;

        // --- Jump (preserve X velocity) ---
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            if (playerAudio && jumpSound) playerAudio.PlayOneShot(jumpSound, 0.5f);
        }
    }

    void FixedUpdate()
    {
        // --- Drag swap: high on ground (stops quickly), low in air (preserves momentum) ---
        rb.drag = isGrounded ? groundLinearDrag : airLinearDrag;

        // --- Target speed (sprint only when grounded) ---
        float speedCap = moveSpeed;
        if (isGrounded && Input.GetKey(sprintKey))
            speedCap *= sprintMultiplier;

        float targetX = horizontalInput * Mathf.Min(maxSpeed, speedCap);

        // --- Horizontal movement ---
        if (isGrounded)
        {
            // Strong control on ground: accelerate quickly toward target
            float newX = Mathf.MoveTowards(rb.velocity.x, targetX, groundAcceleration * Time.fixedDeltaTime);
            rb.velocity = new Vector2(newX, rb.velocity.y);
        }
        else
        {
            // Air control: weaker nudge; don't brake if already faster in same direction
            bool sameDir = Mathf.Sign(rb.velocity.x) == Mathf.Sign(targetX);
            if (sameDir && Mathf.Abs(rb.velocity.x) > Mathf.Abs(targetX))
            {
                targetX = rb.velocity.x; // keep momentum; don't steer down to a smaller target
            }

            float newX = Mathf.MoveTowards(rb.velocity.x, targetX, airAcceleration * Time.fixedDeltaTime);
            rb.velocity = new Vector2(newX, rb.velocity.y);
        }

        // --- Face movement direction ---
        if (horizontalInput > 0f) transform.localScale = new Vector3(1f, 1f, 1f);
        else if (horizontalInput < 0f) transform.localScale = new Vector3(-1f, 1f, 1f);
    }
}
