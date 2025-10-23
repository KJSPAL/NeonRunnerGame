using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymove : MonoBehaviour

{
    public Transform player;
    public float speed = 2f;
    public float detectionRange = 5f; // how close the player must be to start chasing
    public float stopRange = 8f;      // how far before enemy stops chasing again (hysteresis)

    private bool isChasing = false;

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Start chasing when player enters detection range
        if (!isChasing && distanceToPlayer <= detectionRange)
        {
            isChasing = true;
        }
        // Stop chasing when player leaves stop range (to prevent jitter)
        else if (isChasing && distanceToPlayer > stopRange)
        {
            isChasing = false;
        }

        if (isChasing)
        {
            float step = speed * Time.deltaTime;
            Vector2 targetPos = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
        }
    }

    // Optional: visualize detection zones in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopRange);
    }
}
