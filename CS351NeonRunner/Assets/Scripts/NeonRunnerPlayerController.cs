using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonRunnerPlayerController : MonoBehaviour
{
    [Header("Input (per player)")] //This is a header that shows better orginization in the inspector
    //This is all assisgnable per player in the inspector
    //These are the default values for Player 1, This needs to  be changed for Player 2.
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode jumpKey = KeyCode.W;
    public KeyCode sprintKey = KeyCode.LeftShift;

    //Basic movement parameters that can be adjusted in the inspector
    [Header("Movement")]
    public float moveSpeed = 5f; //base move speed
    public float jumpForce = 10f; //how high the player jumps
    public LayerMask groundLayer; //what is considered ground
    public Transform groundCheck; //where to check for ground
    public float groundCheckRadius = 0.2f; //how big of a circle to check for ground

    [Header("Sprint")]
    public float sprintMultiplier = 1.5f;     //how much faster when sprinting (ground only)

    [Header("Feel / Control")]
    public float maxSpeed = 8f;               //The highest possible speed
    //notice that the ground has more acceleration than air for better control   
    public float groundAcceleration = 60f;    //how fast the player accelerates on the ground
    public float airAcceleration = 20f;       //how fast the player accelerates in the air


    public float groundLinearDrag = 8f;       // stop quickly on ground
    public float airLinearDrag = 1f;          // keep momentum in air

    //Audio
    public AudioClip jumpSound;
    public AudioSource playerAudio;

    //Internals
    private Rigidbody2D rb; 
    private bool isGrounded; //Checks if the ground is touched, true if touching false otherwise
    private float horizontalInput; // -1, 0, +1

    private bool isSlowed = false;

    //Animations
    [Header("Animations")]
    private Animator Animator1;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();//initialize the rb variable
        if (!playerAudio) playerAudio = GetComponent<AudioSource>(); //try to get the audio source if not assigned
        if (!groundCheck) Debug.LogError("groundCheck not assigned to the player controller on " + name); //output to the console if not assigned
        if (!rb) Debug.LogError("Rigidbody2D missing on " + name); //output to the console if not assigned
    }

    void Update()
    {
        //Ground check first for responsive jump
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //Read per-player keys and build horizontal input (-1, 0, +1)
        int dir = 0;
        if (Input.GetKey(leftKey)) dir -= 1;
        if (Input.GetKey(rightKey)) dir += 1;
        horizontalInput = dir;

        //Jump (preserve X velocity)
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
            if (!isSlowed && sameDir && Mathf.Abs(rb.velocity.x) > Mathf.Abs(targetX))
            {
                targetX = rb.velocity.x; // keep momentum; don't steer down to a smaller target
            }

            float newX = Mathf.MoveTowards(rb.velocity.x, targetX, airAcceleration * Time.fixedDeltaTime);
            rb.velocity = new Vector2(newX, rb.velocity.y);
        }

        // --- Face movement direction ---
        if (horizontalInput > 0f) transform.localScale = new Vector3(1f, 1f, 1f);
        else if (horizontalInput < 0f) transform.localScale = new Vector3(-1f, 1f, 1f);

        if (horizontalInput != 0)
        {
            Animator1.SetBool("IsRunning", true);
        }
        else
        {
            Animator1.SetBool("IsRunning", false);
        }
    }


    //Abilitie Functino Definitions

    //Apply a slow effect that halves the move speed for a duration (default 5 seconds)
    public void ApplySlow()
    {
        StartCoroutine(SlowCoroutine(3f));
    }
    private IEnumerator SlowCoroutine(float duration)
    {
        isSlowed = true;
        moveSpeed *= 0.5f; //halve the move speed
        yield return new WaitForSeconds(duration);
        moveSpeed *= 2f; //restore the move speed
        isSlowed = false;

    }

    //Speed Boost that doubles the move speed for a duration (default 5 seconds)
    public void ApplyBoost()
    {
        StartCoroutine(BoostCoroutine(2f));
    }
    private IEnumerator BoostCoroutine(float duration)
    {
        moveSpeed *= 2f; //double the move speed
        yield return new WaitForSeconds(duration);
        moveSpeed *= 0.5f; //restore the move speed
    }
}
