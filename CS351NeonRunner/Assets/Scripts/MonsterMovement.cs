using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterMovement : MonoBehaviour
{
    public float speed = 2f;
    public LayerMask groundLayer;      // your ground/tilemap layers
    public float checkDown = 0.6f;     // how far to look for ground below the front foot
    public float frontOffset = 0.05f;  // tiny offset in front of the collider

    Rigidbody2D rb;
    Collider2D col;
    int dir = -1; // 1 = right, -1 = left

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        // 1) Move horizontally
        rb.velocity = new Vector2(dir * speed, rb.velocity.y);

        // 2) Find a point at the front foot
        var b = col.bounds;
        Vector2 frontFoot = new Vector2(
            dir < 0 ? b.min.x - frontOffset : b.max.x + frontOffset,
            b.min.y + 0.02f
        );

        // 3) Raycast straight down to see if there’s ground ahead
        bool groundAhead = Physics2D.Raycast(frontFoot, Vector2.down, checkDown, groundLayer);

        // 4) If no ground ahead, flip direction
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
