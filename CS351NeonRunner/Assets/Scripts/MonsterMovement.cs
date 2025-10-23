using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckDistance = 0.2f;
    public Transform wallCheck;
    public float wallCheckDistance = 0.1f;

    private Rigidbody2D rb;
    private int dir = -1; // -1 = left, +1 = right

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (groundCheck == null || wallCheck == null)
        {
            Debug.LogError($"[{name}] Assign groundCheck and wallCheck!");
            enabled = false; return;
        }
    }

    void FixedUpdate()
    {
        bool groundAhead = Physics2D.Raycast(
            groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        bool wallAhead = Physics2D.Raycast(
            wallCheck.position, new Vector2(dir, 0f), wallCheckDistance, groundLayer);

        if (!groundAhead || wallAhead)
            Flip();

        rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);
    }

    private void Flip()
    {
        dir *= -1;
        var scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * dir;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(
                groundCheck.position,
                groundCheck.position + Vector3.down * groundCheckDistance);
        }
        if (wallCheck != null)
        {
            Gizmos.color = Color.red;
            var d = Application.isPlaying ? new Vector3(dir, 0, 0) : Vector3.right;
            Gizmos.DrawLine(
                wallCheck.position,
                wallCheck.position + d * wallCheckDistance);
        }
    }
}