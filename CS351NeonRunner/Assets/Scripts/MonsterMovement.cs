using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public LayerMask groundLayer;

    // Put this a little in front of the monster's feet.
    public Transform probe;
    public float groundCheckDistance = 0.25f;
    public float wallCheckDistance = 0.1f;

    Rigidbody2D rb;
    int dir = -1; // -1 = left, +1 = right

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void FixedUpdate()
    {
        // Move
        rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);

        // Turn if there is no ground ahead or a wall ahead
        bool groundAhead = Physics2D.Raycast(probe.position, Vector2.down, groundCheckDistance, groundLayer);
        bool wallAhead = Physics2D.Raycast(probe.position, new Vector2(dir, 0f), wallCheckDistance, groundLayer);

        if (!groundAhead || wallAhead) Flip();
    }

    void Flip()
    {
        dir *= -1;
        var s = transform.localScale;
        s.x = Mathf.Abs(s.x) * dir;
        transform.localScale = s;
    }
}
