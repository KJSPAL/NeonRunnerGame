using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{
    //Adjust this value in the inspector to change the speed of the player
    public float moveSpeed = 5f;

    private Rigidbody2D rb;

    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        //Get the RidigBody2d component attached to the game object
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get input values for horizontal and vertical movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

    }
    private void FixedUpdate()
    {
        //Move the player using RigidBody2D in FixedUpdate
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
        
    }
}
