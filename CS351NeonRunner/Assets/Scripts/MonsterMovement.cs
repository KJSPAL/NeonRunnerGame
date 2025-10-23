using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public LayerMask groundLayer;      // include your ground/tilemap layers
    public float groundCheckDown = 0.5f;   // how far to look down (make ≥ tile height * 0.6)
    public float frontEpsilon = 0.05f;     // a hair in front of the feet

    Rigidbody2D rb;
    Collider2D col;
    int dir = -1; // -1 = left, +1 = right

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        // move horizontally
        rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);

        // compute a point at the "front foot" from collider bounds
        var b = col.bounds;
        Vector2 frontFoot = new Vector2(
            dir < 0 ? b.min.x - frontEpsilon : b.max.x + frontEpsilon,
            b.min.y + 0.02f
        );

        // look straight down for ground ahead
        bool groundAhead = Physics2D.Raycast(frontFoot, Vector2.down, groundCheckDown, groundLayer);

        if (!groundAhead) Flip();
    }

    void Flip()
    {
        dir = -dir;
        var s = transform.localScale;
        s.x = Mathf.Abs(s.x) * dir;
        transform.localScale = s;
    }
}

