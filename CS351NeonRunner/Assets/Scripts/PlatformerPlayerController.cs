using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayerController : MonoBehaviour
{
    //Player movement speed
    public float moveSpeed = 5f;

    //Force applied when jumping
    public float jumpForce = 10f;

    //Layer mask for detecting ground
    public LayerMask groundLayer;

    //Transform representing the position to check for ground
    public Transform groundCheck;

    //Radius for ground check
    public float groundCheckRadius = 0.2f;

    //Reference to the Rigidbody2D component
    private Rigidbody2D rb;

    //Boolean to check is we are on the ground
    private bool isGrounded;

    // A variable to hold horizontal input
    private float horizontalInput;


    // Start is called before the first frame update
    void Start()
    {
        //Get the Rigidbody2D component attached to the gameObject
        rb = GetComponent<Rigidbody2D>();

        //Ensure the groundCheck variable is assigned
        if (groundCheck == null)
        {
            Debug.LogError("groundCheck not assigned to the player controller");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Get input values for horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");

        //Check for jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //Apply a upward force for jumping
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        //Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);


        //TODO: Optionally we can add animations here later

        //Ensure the placer is facing the direction of the movement

        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if(horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

    }

}
